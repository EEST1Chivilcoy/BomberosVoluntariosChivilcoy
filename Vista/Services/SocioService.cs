using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Socios;
using Vista.Data.Models.Socios.Componentes;
using Vista.Helpers;

namespace Vista.Services
{
    public interface ISocioService
    {
        Task CrearSocioAsync(Socio socio);
        Task<List<Socio>?> ObtenerSociosAsync();
        Task<Socio?> ObtenerSocioPorIdAsync(int socioId, bool asNoTracking = true);
        Task EditarSocioAsync(Socio socio);
        Task<int> ObtenerProximoNroSocioAsync();
    }

    public class SocioService : ISocioService
    {
        private readonly BomberosDbContext _context;
        private readonly IHistorialSocioService _historialSocioService;

        public SocioService(BomberosDbContext context, IHistorialSocioService historialSocioService)
        {
            _context = context;
            _historialSocioService = historialSocioService;
        }

        public async Task CrearSocioAsync(Socio socio)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (socio == null)
            {
                throw new ArgumentNullException(nameof(socio), "El socio no puede ser nulo.");
            }

            ValidationHelper.Validar(socio);

            // --- Paso B: Validaciones "Caras" (contra la BD) ---
            // (Se hacen antes de iniciar la transacción para no abrirla innecesariamente)

            if (await _context.Socios.AnyAsync(s => s.NroSocio == socio.NroSocio))
            {
                throw new InvalidOperationException($"Ya existe un socio con el Nro de Socio '{socio.NroSocio}'.");
            }

            if (await _context.Socios.AnyAsync(s => s.DocumentoOCUIT == socio.DocumentoOCUIT))
            {
                throw new InvalidOperationException($"Ya existe un socio con el Documento o CUIT '{socio.DocumentoOCUIT}'.");
            }

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso A: Guardar el Socio ---
                _context.Socios.Add(socio);

                // Guardamos en la BD para obtener el Id del socio
                await _context.SaveChangesAsync();

                // --- Paso B: Crear Movimientos Iniciales ---

                // Movimiento inicial de cuota
                var movimientoCuota = new MovimientoCambioCuota
                {
                    FechaDesde = socio.FechaIngresoSistemaNuevo,
                    FechaHasta = null, // Vigente
                    Monto = socio.MontoCuota,
                    FormaDePago = socio.FormaPago ?? Data.Enums.Socios.FormaDePago.Efectivo,
                    FrecuenciaDePago = socio.FrecuenciaDePago ?? Data.Enums.Socios.FrecuenciaPago.Mensual,
                    SocioId = socio.Id
                };

                _context.HistorialSocios.Add(movimientoCuota);

                // Movimiento inicial de estado
                var movimientoEstado = new MovimientoCambioEstado
                {
                    FechaDesde = socio.FechaIngresoSistemaNuevo,
                    FechaHasta = null, // Vigente
                    Estado = socio.EstadoSocio ?? Data.Enums.Socios.TipoEstadoSocio.Activo,
                    Motivo = "Alta de socio en el sistema",
                    SocioId = socio.Id
                };

                _context.HistorialSocios.Add(movimientoEstado);

                // Guardamos los movimientos
                await _context.SaveChangesAsync();

                // --- Paso C: Confirmamos la transacción ---
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // --- Manejo de Error ---
                await transaction.RollbackAsync();

                // Limpiar el contexto para evitar conflictos futuros
                _context.ChangeTracker.Clear();

                if (ex is DbUpdateException)
                {
                    throw new Exception("Error al guardar en la base de datos. Verifique datos duplicados.", ex);
                }

                throw;
            }
        }

        public async Task<List<Socio>?> ObtenerSociosAsync()
        {
            return await _context.Socios
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Socio?> ObtenerSocioPorIdAsync(int socioId, bool asNoTracking = true)
        {
            IQueryable<Socio> query = _context.Socios;

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(s => s.Id == socioId);
        }

        public async Task EditarSocioAsync(Socio socio)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---

            if (socio == null)
            {
                throw new ArgumentNullException(nameof(socio), "El socio no puede ser nulo.");
            }

            ValidationHelper.Validar(socio);

            // --- Paso B: Validaciones "Caras" (contra la BD) ---

            Socio SocioAEditar = await ObtenerSocioPorIdAsync(socio.Id, asNoTracking: false)
                ?? throw new KeyNotFoundException($"No se encontró un socio con Id '{socio.Id}'.");

            // Validar que no exista el mismo NroSocio para otro Socio (si se cambió)
            if (SocioAEditar.NroSocio != socio.NroSocio)
            {
                bool NroSocioExistente = await _context.Socios
                    .AnyAsync(s => s.NroSocio == socio.NroSocio && s.Id != socio.Id);

                if (NroSocioExistente)
                {
                    throw new InvalidOperationException("Número de socio ya existente para otro socio.");
                }
            }

            // Validar que no exista otro Socio with el mismo documento o CUIT (si se cambió)
            if (SocioAEditar.DocumentoOCUIT != socio.DocumentoOCUIT)
            {
                bool documentoExistente = await _context.Socios
                    .AnyAsync(s => s.DocumentoOCUIT == socio.DocumentoOCUIT && s.Id != socio.Id);

                if (documentoExistente)
                {
                    throw new InvalidOperationException("Número de documento ya existente para otro socio.");
                }
            }

            // --- 2. Inicio de la Transacción ---
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso C: Crear Movimientos en el Historial ---

                // Detectar cambios en la cuota
                if (SocioAEditar.MontoCuota != socio.MontoCuota ||
                    SocioAEditar.FrecuenciaDePago != socio.FrecuenciaDePago ||
                    SocioAEditar.FormaPago != socio.FormaPago)
                {
                    // El movimiento registra los nuevos valores de la cuota
                    // El servicio de historial se encargará de cerrar el movimiento anterior
                    var movimientoCuota = new MovimientoCambioCuota
                    {
                        Monto = socio.MontoCuota,
                        FormaDePago = socio.FormaPago ?? Data.Enums.Socios.FormaDePago.Efectivo,
                        FrecuenciaDePago = socio.FrecuenciaDePago ?? Data.Enums.Socios.FrecuenciaPago.Mensual,
                    };

                    await _historialSocioService.CrearMovimientoSocio(socioId: socio.Id, movimientoCuota);
                }

                // Detectar cambios en el estado
                if (SocioAEditar.EstadoSocio != socio.EstadoSocio)
                {
                    // El movimiento registra el nuevo estado
                    // El servicio de historial se encargará de cerrar el movimiento anterior
                    var movimientoEstado = new MovimientoCambioEstado
                    {
                        Estado = socio.EstadoSocio ?? Data.Enums.Socios.TipoEstadoSocio.Activo,
                        Motivo = $"Cambio de estado de {SocioAEditar.EstadoSocio} a {socio.EstadoSocio}",
                    };

                    await _historialSocioService.CrearMovimientoSocio(socioId: socio.Id, movimientoEstado);
                }

                // --- Paso D: Actualizar los campos del Socio ---
                SocioAEditar.NroSocio = socio.NroSocio;
                SocioAEditar.Nombre = socio.Nombre;
                SocioAEditar.Apellido = socio.Apellido;
                SocioAEditar.DocumentoOCUIT = socio.DocumentoOCUIT;
                SocioAEditar.Direccion = socio.Direccion;
                SocioAEditar.Latitud = socio.Latitud;
                SocioAEditar.Longitud = socio.Longitud;
                SocioAEditar.Zona = socio.Zona;
                SocioAEditar.Ocupacion = socio.Ocupacion;
                SocioAEditar.Telefono = socio.Telefono;
                SocioAEditar.Email = socio.Email;
                SocioAEditar.Tipo = socio.Tipo;
                SocioAEditar.FechaIngreso = socio.FechaIngreso;
                SocioAEditar.EstadoSocio = socio.EstadoSocio;
                SocioAEditar.MontoCuota = socio.MontoCuota;
                SocioAEditar.FrecuenciaDePago = socio.FrecuenciaDePago;
                SocioAEditar.FormaPago = socio.FormaPago;

                // --- Paso E: Guardar los cambios ---
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // --- Manejo de Error ---
                await transaction.RollbackAsync();

                // Limpiar el contexto para evitar conflictos futuros
                _context.ChangeTracker.Clear();

                if (ex is DbUpdateException)
                {
                    throw new Exception("Error al guardar en la base de datos. Verifique datos duplicados.", ex);
                }

                throw;
            }
        }

        public async Task<int> ObtenerProximoNroSocioAsync()
        {
            // Obtiene el máximo NroSocio actual, si no hay socios retorna 0
            int maxNroSocio = await _context.Socios
                .MaxAsync(s => (int?)s.NroSocio) ?? 0;

            return maxNroSocio + 1;
        }
    }
}

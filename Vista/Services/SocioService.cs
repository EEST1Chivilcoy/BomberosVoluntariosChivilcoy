using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Socios;
using Vista.Helpers;

namespace Vista.Services
{
    public interface ISocioService
    {
        Task CrearSocioAsync(Socio socio);
        Task<List<Socio>?> ObtenerSociosAsync();
        Task<Socio?> ObtenerSocioPorIdAsync(int socioId, bool asNoTracking = true);
        Task EditarSocioAsync(Socio socio);
    }

    public class SocioService : ISocioService
    {
        private readonly BomberosDbContext _context;

        public SocioService(BomberosDbContext context)
        {
            _context = context;
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

                // Guardamos en la BD
                await _context.SaveChangesAsync();

                // --- Paso B: Confirmamos la transacción ---
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // --- Manejo de Error ---
                // Si CUALQUIER operación falla (el primer SaveChanges,
                // la lógica del service, o el segundo SaveChanges dentro del service),
                // revertimos TODA la operación.
                await transaction.RollbackAsync();

                // Limpiar el contexto para evitar conflictos futuros
                _context.ChangeTracker.Clear();

                // Lanza una excepción genérica o la 'ex' original
                // para que la capa superior sepa que algo falló.
                if (ex is DbUpdateException)
                {
                    throw new Exception("Error al guardar en la base de datos. Verifique datos duplicados.", ex);
                }

                // Re-lanza la excepción (ej. la ValidationException del service)
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

            try
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

                // Validar que no exista otro Socio con el mismo documento o CUIT (si se cambió)
                if (SocioAEditar.DocumentoOCUIT != socio.DocumentoOCUIT)
                {
                    bool documentoExistente = await _context.Socios
                        .AnyAsync(s => s.DocumentoOCUIT == socio.DocumentoOCUIT && s.Id != socio.Id);

                    if (documentoExistente)
                    {
                        throw new InvalidOperationException("Número de documento ya existente para otro bombero.");
                    }
                }

                // --- Paso C: Actualizar los campos del Socio ---
                SocioAEditar.Id = socio.Id;
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

                // --- Paso D: Crear Movimientos en el Historial ---

                // Pendiente de implementar

                // --- Paso E: Inicio de la Transacción ---
                // Esta será la transacción "principal" que controlará todo.
                using var transaction = await _context.Database.BeginTransactionAsync();

                // Guardar los cambios
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // --- Manejo de Error ---
                // Si CUALQUIER operación falla (el primer SaveChanges,
                // la lógica del service, o el segundo SaveChanges dentro del service),
                // revertimos TODA la operación.
                //await transaction.RollbackAsync();

                // Limpiar el contexto para evitar conflictos futuros
                _context.ChangeTracker.Clear();

                // Lanza una excepción genérica o la 'ex' original
                // para que la capa superior sepa que algo falló.
                if (ex is DbUpdateException)
                {
                    throw new Exception("Error al guardar en la base de datos. Verifique datos duplicados.", ex);
                }

                // Re-lanza la excepción (ej. la ValidationException del service)
                throw;
            }
        }
    }
}

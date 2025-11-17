using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Enums.Personal.ComisionDirectiva;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Helpers;

namespace Vista.Services
{
    public interface IComisionDirectivaService
    {
        Task CrearComisionDirectivaAsync(ComisionDirectiva comisionDirectiva, Imagen? imagen = null);
        Task EditarComisionDirectivaAsync(ComisionDirectiva comisionDirectiva);
        Task<List<ComisionDirectiva>> ObtenerTodosLosMiembrosDeComisionDirectivaAsync(bool ConImagenes = false);
        Task<ComisionDirectiva> ObtenerMiembroComisionDirectivaPorIdAsync(int id, bool asnotracking = false, bool conRelaciones = false);
        Task<bool> CambiarEstado(int id, EstadoComisionDirectiva estado);
    }

    public class ComisionDirectivaService : IComisionDirectivaService
    {
        private readonly BomberosDbContext _context;
        private readonly IImagenService _imagenService;

        public ComisionDirectivaService(BomberosDbContext context, IImagenService imagenService)
        {
            _context = context;
            _imagenService = imagenService;
        }

        public async Task CrearComisionDirectivaAsync(ComisionDirectiva comisionDirectiva, Imagen? imagen = null)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (comisionDirectiva == null)
            {
                throw new ArgumentNullException(nameof(comisionDirectiva), "El Comisión Directiva no puede ser nulo.");
            }

            ValidationHelper.Validar(comisionDirectiva);

            // --- Paso B: Validaciones "Caras" (contra la BD) ---
            // (Se hacen antes de iniciar la transacción para no abrirla innecesariamente)

            if (await _context.ComisionDirectivas.AnyAsync(b => b.Documento == comisionDirectiva.Documento))
            {
                throw new InvalidOperationException("Número de documento ya existente.");
            }

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso A: Guardar el Comisión Directiva ---
                _context.ComisionDirectivas.Add(comisionDirectiva);

                // Guardamos para que la BD genere el 'comisionDirectiva.PersonaId'
                await _context.SaveChangesAsync();

                // --- Paso B: Guardar la Imagen (usando el Service) ---
                if (imagen != null)
                {
                    if (imagen is Imagen_Personal imagenPersonal)
                    {
                        // Asignamos el ID del comisionDirectiva recién creado
                        imagenPersonal.PersonalId = comisionDirectiva.PersonaId;

                        // Llamamos al servicio.
                        // Si GuardarImagenAsync falla, lanzará una excepción
                        // que será capturada por nuestro bloque 'catch' más abajo.
                        await _imagenService.GuardarImagenAsync(imagenPersonal);
                    }
                    else
                    {
                        // Esta excepción también será capturada y provocará un Rollback.
                        throw new InvalidOperationException("La imagen proporcionada no es del tipo correcto para un bombero.");
                    }
                }

                // --- Paso C: Confirmar la Transacción ---
                // Si llegamos aquí, tanto el SaveChanges del Bombero como
                // el SaveChanges (dentro del service) de la Imagen fueron exitosos.
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // --- Manejo de Error ---
                // Si CUALQUIER operación falla (el primer SaveChanges,
                // la lógica del service, o el segundo SaveChanges dentro del service),
                // revertimos TODA la operación.
                await transaction.RollbackAsync();

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

        public async Task EditarComisionDirectivaAsync(ComisionDirectiva comisionDirectiva)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (comisionDirectiva == null)
            {
                throw new ArgumentNullException(nameof(comisionDirectiva), "El Comisión Directiva no puede ser nulo.");
            }

            ValidationHelper.Validar(comisionDirectiva);

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            using var transaction = await _context.Database.BeginTransactionAsync();


            try
            {
                // Buscamos el Comisión Directiva existente con su contacto
                var existente = await _context.ComisionDirectivas
                    .Include(c => c.Contacto)
                    .FirstOrDefaultAsync(c => c.PersonaId == comisionDirectiva.PersonaId);

                if (existente == null)
                {
                    throw new KeyNotFoundException($"No se encontró un miembro de comisión directiva con el ID {comisionDirectiva.PersonaId}.");
                }

                // Validar que no exista otro Comisión Directiva con el mismo documento
                if (await _context.ComisionDirectivas.AnyAsync(b => b.Documento == comisionDirectiva.Documento && b.PersonaId != comisionDirectiva.PersonaId))
                {
                    throw new InvalidOperationException("Número de documento ya existente.");
                }

                // Actualizar los campos del Comisión Directiva existente

                // Información Personal

                existente.Documento = comisionDirectiva.Documento;
                existente.FechaNacimiento = comisionDirectiva.FechaNacimiento;
                existente.LugarNacimiento = comisionDirectiva.LugarNacimiento;
                existente.Direccion = comisionDirectiva.Direccion;
                existente.Sexo = comisionDirectiva.Sexo;
                existente.GrupoSanguineo = comisionDirectiva.GrupoSanguineo;

                // Información Profesional

                existente.Grado = comisionDirectiva.Grado;
                existente.Estado = comisionDirectiva.Estado;
                existente.FechaAceptacion = comisionDirectiva.FechaAceptacion;

                // Información de Contacto
                if (existente.Contacto == null)
                {
                    existente.Contacto = new Contacto
                    {
                        PersonalId = existente.PersonaId,
                        TelefonoCel = comisionDirectiva.Contacto?.TelefonoCel,
                        TelefonoLaboral = comisionDirectiva.Contacto?.TelefonoLaboral,
                        TelefonoFijo = comisionDirectiva.Contacto?.TelefonoFijo,
                        Email = comisionDirectiva.Contacto?.Email
                    };
                }
                else
                {
                    existente.Contacto.TelefonoCel = comisionDirectiva.Contacto?.TelefonoCel;
                    existente.Contacto.TelefonoLaboral = comisionDirectiva.Contacto?.TelefonoLaboral;
                    existente.Contacto.TelefonoFijo = comisionDirectiva.Contacto?.TelefonoFijo;
                    existente.Contacto.Email = comisionDirectiva.Contacto?.Email;
                }

                await _context.SaveChangesAsync();
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

        public async Task<List<ComisionDirectiva>> ObtenerTodosLosMiembrosDeComisionDirectivaAsync(bool ConImagenes = false)
        {
            IQueryable<ComisionDirectiva> query = _context.ComisionDirectivas;

            if (ConImagenes)
            {
                query = query.Include(c => c.Imagen);
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> CambiarEstado(int id, EstadoComisionDirectiva estado)
        {
            var miembro = await _context.ComisionDirectivas.FindAsync(id);

            if (miembro == null)
            {
                throw new KeyNotFoundException($"No se encontró un miembro de comisión directiva con el ID {id}.");
            }

            miembro.Estado = estado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ComisionDirectiva> ObtenerMiembroComisionDirectivaPorIdAsync(int id, bool asnotracking = false, bool conRelaciones = false)
        {
            IQueryable<ComisionDirectiva> query = _context.ComisionDirectivas;

            if (conRelaciones)
            {
                query = query
                    .Include(c => c.Imagen)
                    .Include(c => c.Contacto);
            }

            if (asnotracking)
            {
                query = query.AsNoTracking();
            }

            var miembro = await query.FirstOrDefaultAsync(c => c.PersonaId == id);

            if (miembro is null)
            {
                throw new KeyNotFoundException($"No se encontró un miembro de comisión directiva con el ID {id}.");
            }

            return miembro;
        }
    }
}
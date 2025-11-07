using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Data.Models.Imagenes;
using Vista.Helpers;

namespace Vista.Services
{
    public interface IBomberoService
    {
        Task CrearBomberoAsync(Bombero bombero, Imagen? imagen = null);
        Task<bool> BorrarBombero(Bombero bombero);
        Task EditarBombero(Bombero bombero);
        Task<Sancion> SancionarBombero(Sancion sancion);
        Task<bool> CambiarEstado(int id, EstadoBombero estado);
        Task<AscensoBombero> AscenderBombero(AscensoBombero ascenso);
        Task<List<Bombero>> ObtenerTodosLosBomberosAsync(bool ConImagenes = false, bool ConTodasLasDemasRelaciones = false);
        Task<Bombero> ObtenerBomberoPorIdAsync(int id, bool asnotracking = false, bool conRelaciones = true);
        Task<Bombero> ObtenerBomberoObjetoPorLegajoAsync(int numeroLegajo);
    }

    public class BomberoService : IBomberoService
    {
        private readonly BomberosDbContext _context;
        private readonly IImagenService _imagenService;

        public BomberoService(BomberosDbContext context, IImagenService imagenService)
        {
            _context = context;
            _imagenService = imagenService;
        }

        public async Task<Bombero> ObtenerBomberoPorIdAsync(int id, bool asNoTracking = false, bool conRelaciones = true)
        {
            IQueryable<Bombero> query = _context.Bomberos;

            if (conRelaciones)
            {
                query = query
                    .Include(b => b.Imagen)
                    .Include(b => b.Firmas)
                    .Include(b => b.Brigadas)
                    .Include(b => b.VehiculosEncargado)
                    .Include(b => b.Dependencias)
                    .Include(b => b.Contacto);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var bombero = await query.FirstOrDefaultAsync(b => b.PersonaId == id);

            if (bombero is null)
            {
                throw new KeyNotFoundException($"No se encontró un bombero con el ID {id}.");
            }

            return bombero;
        }

        public async Task CrearBomberoAsync(Bombero bombero, Imagen? imagen = null)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (bombero == null)
            {
                throw new ArgumentNullException(nameof(bombero), "El bombero no puede ser nulo.");
            }

            ValidationHelper.Validar(bombero);

            // --- Paso B: Validaciones "Caras" (contra la BD) ---
            // (Se hacen antes de iniciar la transacción para no abrirla innecesariamente)

            if (await _context.Bomberos.AnyAsync(b => b.Documento == bombero.Documento))
            {
                throw new InvalidOperationException("Número de documento ya existente.");
            }
            if (await _context.Bomberos.AnyAsync(b => b.NumeroLegajo == bombero.NumeroLegajo))
            {
                throw new InvalidOperationException("Número de legajo ya existente.");
            }

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso A: Guardar el Bombero ---
                _context.Bomberos.Add(bombero);

                // Guardamos para que la BD genere el 'bombero.PersonaId'
                await _context.SaveChangesAsync();

                // --- Paso B: Guardar la Imagen (usando el Service) ---
                if (imagen != null)
                {
                    if (imagen is Imagen_Personal imagenPersonal)
                    {
                        // Asignamos el ID del bombero recién creado
                        imagenPersonal.PersonalId = bombero.PersonaId;

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

        public async Task EditarBombero(Bombero bombero)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (bombero == null)
            {
                throw new ArgumentNullException(nameof(bombero), "El bombero no puede ser nulo.");
            }

            ValidationHelper.Validar(bombero);

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Buscar el bombero existente incluyendo su contacto
                var bomberoExistente = await _context.Bomberos
                    .Include(b => b.Contacto)
                    .SingleOrDefaultAsync(e => e.PersonaId == bombero.PersonaId);

                if (bomberoExistente == null)
                {
                    throw new KeyNotFoundException($"No se encontró un bombero con el ID {bombero.PersonaId}.");
                }

                // Validar que no exista otro bombero con el mismo número de legajo (si se cambió)
                if (bomberoExistente.NumeroLegajo != bombero.NumeroLegajo)
                {
                    bool legajoExistente = await _context.Bomberos
                        .AnyAsync(b => b.NumeroLegajo == bombero.NumeroLegajo && b.PersonaId != bombero.PersonaId);

                    if (legajoExistente)
                    {
                        throw new InvalidOperationException("Número de legajo ya existente para otro bombero.");
                    }
                }

                // Validar que no exista otro bombero con el mismo documento (si se cambió)
                if (bomberoExistente.Documento != bombero.Documento)
                {
                    bool documentoExistente = await _context.Bomberos
                        .AnyAsync(b => b.Documento == bombero.Documento && b.PersonaId != bombero.PersonaId);
                    if (documentoExistente)
                    {
                        throw new InvalidOperationException("Número de documento ya existente para otro bombero.");
                    }
                }

                // Actualizar las propiedades del bombero existente
                bomberoExistente.Nombre = bombero.Nombre;
                bomberoExistente.Apellido = bombero.Apellido;
                bomberoExistente.Documento = bombero.Documento;
                bomberoExistente.NumeroLegajo = bombero.NumeroLegajo;
                bomberoExistente.NumeroIoma = bombero.NumeroIoma;
                bomberoExistente.LugarNacimiento = bombero.LugarNacimiento;
                bomberoExistente.Grado = bombero.Grado;
                bomberoExistente.Dotacion = bombero.Dotacion;
                bomberoExistente.GrupoSanguineo = bombero.GrupoSanguineo;
                bomberoExistente.Altura = bombero.Altura;
                bomberoExistente.Peso = bombero.Peso;
                bomberoExistente.Estado = bombero.Estado;
                bomberoExistente.Chofer = bombero.Chofer;
                bomberoExistente.VencimientoRegistro = bombero.VencimientoRegistro;
                bomberoExistente.Direccion = bombero.Direccion;
                bomberoExistente.Observaciones = bombero.Observaciones;
                bomberoExistente.Profesion = bombero.Profesion;
                bomberoExistente.NivelEstudios = bombero.NivelEstudios;
                bomberoExistente.FechaAceptacion = bombero.FechaAceptacion;
                bomberoExistente.FechaNacimiento = bombero.FechaNacimiento;
                bomberoExistente.Sexo = bombero.Sexo;

                // Manejar la actualización del contacto
                if (bombero.Contacto != null)
                {
                    if (bomberoExistente.Contacto != null)
                    {
                        // Actualizar contacto existente
                        bomberoExistente.Contacto.TelefonoCel = bombero.Contacto.TelefonoCel;
                        bomberoExistente.Contacto.TelefonoFijo = bombero.Contacto.TelefonoFijo;
                        bomberoExistente.Contacto.TelefonoLaboral = bombero.Contacto.TelefonoLaboral;
                        bomberoExistente.Contacto.Email = bombero.Contacto.Email;
                    }
                    else
                    {
                        // Crear nuevo contacto
                        bomberoExistente.Contacto = new Contacto
                        {
                            PersonalId = bomberoExistente.PersonaId,
                            TelefonoCel = bombero.Contacto.TelefonoCel,
                            TelefonoFijo = bombero.Contacto.TelefonoFijo,
                            TelefonoLaboral = bombero.Contacto.TelefonoLaboral,
                            Email = bombero.Contacto.Email
                        };
                        _context.Contactos.Add(bomberoExistente.Contacto);
                    }
                }

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

        public async Task<bool> CambiarEstado(int id, EstadoBombero estado)
        {
            Bombero? BomberoE = await _context.Bomberos.SingleOrDefaultAsync(b => b.PersonaId == id);

            if (BomberoE == null)
            {
                return false;
            }

            BomberoE.Estado = estado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BorrarBombero(Bombero bombero)
        {
            if (bombero == null)
            {
                Console.WriteLine("ERROR: Bombero no puede ser nulo.");
                return false;
            }

            try
            {
                _context.Bomberos.Remove(bombero);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                // Manejo de excepción específico para errores de la base de datos
                Console.WriteLine($"ERROR: Error al eliminar el bombero. {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Manejo de excepción genérico
                Console.WriteLine($"ERROR: Ocurrió un error inesperado. {ex.Message}");
                return false;
            }
        }

        public async Task<Sancion> SancionarBombero(Sancion sancion)
        {
            Bombero? BomberoSancionado = await _context.Bomberos.Where(b => b.NumeroLegajo == sancion.PersonalSancionado.NumeroLegajo).SingleOrDefaultAsync();

            if (BomberoSancionado == null)
            {
                throw new InvalidOperationException("No se pudieron encontrar los bomberos.");
            }

            sancion.PersonalSancionado = BomberoSancionado;

            _context.Sanciones.Add(sancion);
            await _context.SaveChangesAsync();
            return sancion;
        }

        public async Task<AscensoBombero> AscenderBombero(AscensoBombero ascenso)
        {
            Bombero? BomberoAfectado = await _context.Bomberos.Where(b => b.NumeroLegajo == ascenso.PersonalAfectado.NumeroLegajo).SingleOrDefaultAsync();

            if (BomberoAfectado == null)
            {
                throw new InvalidOperationException("No se pudo encontrar el BomberoAfectado.");
            }
            BomberoAfectado.Grado = ascenso.GradoAscenso;
            ascenso.PersonalAfectado = BomberoAfectado;
            _context.AscensoBomberos.Add(ascenso);
            await _context.SaveChangesAsync();
            return ascenso;
        }

        public async Task<List<Bombero>> ObtenerTodosLosBomberosAsync(
            bool ConImagenes = false,
            bool ConTodasLasDemasRelaciones = false)
        {
            IQueryable<Bombero> query = _context.Bomberos.AsQueryable();

            // Si no se pide nada, traemos solo los bomberos pelados
            if (!ConImagenes && !ConTodasLasDemasRelaciones)
            {
                return await query
                    .AsNoTracking()
                    .ToListAsync();
            }

            // Carga condicional de relaciones
            if (ConImagenes)
            {
                query = query.Include(b => b.Imagen);
            }

            if (ConTodasLasDemasRelaciones)
            {
                query = query
                    .Include(b => b.Firmas)
                    .Include(b => b.Brigadas)
                    .Include(b => b.VehiculosEncargado)
                    .Include(b => b.Dependencias)
                    .Include(b => b.Incidentes)
                    .Include(b => b.Salidas)
                    .Include(b => b.Handie)
                    .Include(b => b.Ascensos)
                    .Include(b => b.DestinoMaterial)
                    .Include(b => b.SancionesRecibidas)
                    .Include(b => b.Limpieza)
                    .Include(b => b.Novedades)
                    .Include(b => b.Licencias);
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<Bombero> ObtenerBomberoObjetoPorLegajoAsync(int numeroLegajo)
        {
            var bombero = await _context.Bomberos
                .FirstOrDefaultAsync(b => b.NumeroLegajo == numeroLegajo);

            return bombero;
        }
    }
}

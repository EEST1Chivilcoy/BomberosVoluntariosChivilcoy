using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Models.Otros.Partes;
using Vista.Helpers;

namespace Vista.Services
{
    public interface IParteVehiculoService
    {
        Task AgregarParteVehiculoAsync(ParteVehiculo parteVehiculo);
        Task<List<ParteVehiculo>> ObtenerTodasLasPartesVehiculoAsync();
        Task<ParteVehiculo?> ObtenerParteVehiculoPorIdAsync(int id, bool asNoTrack = true);
    }
    public class ParteVehiculoService : IParteVehiculoService
    {
        private readonly BomberosDbContext _context;

        public ParteVehiculoService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task AgregarParteVehiculoAsync(ParteVehiculo parteVehiculo)
        {
            // 1. --- Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---

            if (parteVehiculo == null)
            {
                throw new ArgumentNullException(nameof(parteVehiculo), "El objeto parteVehiculo no puede ser nulo.");
            }

            ValidationHelper.Validar(parteVehiculo);

            // --- Validar duplicados ---
            var nombreNormalizado = parteVehiculo.Nombre?.Trim().ToLower();

            if (await _context.PartesVehiculo
                .AnyAsync(pv =>
                    pv.Nombre.ToLower().Trim() == nombreNormalizado &&
                    pv.Tipo == parteVehiculo.Tipo))
            {
                throw new InvalidOperationException(
                    $"Ya existe un ParteVehiculo con el nombre '{parteVehiculo.Nombre}' y tipo '{parteVehiculo.Tipo}'.");
            }


            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso A: Guardar la Parte del Vehiculo ---
                _context.PartesVehiculo.Add(parteVehiculo);

                // --- Guardar los cambios en la BD ---
                await _context.SaveChangesAsync();

                // --- Paso B: Confirmar la Transacción ---
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

                // Re-lanza la excepción
                throw;
            }
        }

        public async Task<List<ParteVehiculo>> ObtenerTodasLasPartesVehiculoAsync()
        {
            return await _context.PartesVehiculo
                .OrderBy(pv => pv.Nombre) // Ordena por nombre
                .AsNoTracking() // Evita el seguimiento de EF Core
                .ToListAsync();
        }

        public async Task<ParteVehiculo?> ObtenerParteVehiculoPorIdAsync(int id, bool asNoTrack = true)
        {
            IQueryable<ParteVehiculo> query = _context.PartesVehiculo;

            if (asNoTrack)
            {
                query = query.AsNoTracking(); // Evita el seguimiento de EF Core
            }

            return await query.FirstOrDefaultAsync(pv => pv.Id == id);
        }
    }
}

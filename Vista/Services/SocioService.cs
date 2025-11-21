using Vista.Data;
using Microsoft.EntityFrameworkCore;
using Vista.Data.Models.Socios;
using Vista.Helpers;

namespace Vista.Services
{
    public interface ISocioService
    {
        Task CrearSocioAsync(Socio socio);
        Task<List<Socio>?> ObtenerSociosAsync();
        Task<Socio?> ObtenerSocioPorIdAsync(int socioId, bool asNoTracking = true);
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
    }
}

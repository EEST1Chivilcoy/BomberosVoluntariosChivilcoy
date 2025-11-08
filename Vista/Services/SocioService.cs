using Vista.Data;
using Microsoft.EntityFrameworkCore;
using Vista.Data.Models.Socios;
using Vista.Helpers;

namespace Vista.Services
{
    public interface ISocioService
    {
        Task CrearSocioAsync(Socio socio);
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

            _context.Socios.Add(socio);
            await _context.SaveChangesAsync();
        }
    }
}

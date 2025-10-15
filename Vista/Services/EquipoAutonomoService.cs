using Vista.Data;
using Vista.Data.Models.Grupos.Dependencias.EquiposAutonomos;

namespace Vista.Services
{
    public interface IEquipoAutonomoService
    {
        Task CrearEquipoAutonomo(EquipoAutonomo equipo);
        //Task EditarEquipoAutonomo(EquipoAutonomo equipo);
        //Task BorrarEquipoAutonomo(int equipoId);
        //Task <List<EquipoAutonomo>> ObtenerEquiposAutonomos();
    }
    public class EquipoAutonomoService : IEquipoAutonomoService
    {
        private readonly BomberosDbContext _context;

        public EquipoAutonomoService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task CrearEquipoAutonomo(EquipoAutonomo equipo)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(equipo.NroSerie))
            {
                throw new ArgumentException("El número de serie es obligatorio.");
            }

            _context.EquiposAutonomos.Add(equipo);
            await _context.SaveChangesAsync();
        }
    }
}

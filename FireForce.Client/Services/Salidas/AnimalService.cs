//using FireForce.Data;
//using FireForce.Data.Models.Salidas.Componentes;
//using Microsoft.EntityFrameworkCore;

//namespace FireForce.Client.Services.Salidas
//{
//    public interface IAnimalService
//    {
//        Task<List<AnimalSalida>> ObtenerTodosLosAnimalesAsync();
//        Task<AnimalSalida?> ObtenerAnimalPorIdAsync(int id);
//        Task AgregarAnimalAsync(AnimalSalida animal);
//        Task EditarAnimalAsync(AnimalSalida animal);
//        Task BorrarAnimalAsync(int id);
//    }

//    class AnimalService : IAnimalService
//    {
//        private readonly BomberosDbContext _context;

//        public AnimalService(BomberosDbContext context)
//        {
//            _context = context;
//        }

//        public async Task AgregarAnimalAsync(AnimalSalida animal)
//        {
//            _context.AnimalesSalida.Add(animal);
//            await _context.SaveChangesAsync();
//        }

//        public async Task BorrarAnimalAsync(int id)
//        {
//            var animal = await _context.AnimalesSalida.FindAsync(id);
//            if (animal is null) return;
//            _context.AnimalesSalida.Remove(animal);
//            await _context.SaveChangesAsync();
//        }

//        public async Task EditarAnimalAsync(AnimalSalida animal)
//        {
//            var animalAEditar = await _context.AnimalesSalida.FindAsync(animal.AnimalId);

//            if (animalAEditar is null) return;

//            animalAEditar.Tipo = animal.Tipo;
//            animalAEditar.TipoOtro = animal.TipoOtro;
//            animalAEditar.Estado = animal.Estado;
//            animalAEditar.Cantidad = animal.Cantidad;
//            animalAEditar.Nombre = animal.Nombre;
//            animalAEditar.Observaciones = animal.Observaciones;
//            animalAEditar.DamnificadoId = animal.DamnificadoId;
//            animalAEditar.Damnificado = animal.Damnificado;

//            _context.AnimalesSalida.Update(animalAEditar);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<AnimalSalida?> ObtenerAnimalPorIdAsync(int id)
//        {
//            return await _context.AnimalesSalida.FindAsync(id);
//        }

//        public async Task<List<AnimalSalida>> ObtenerTodosLosAnimalesAsync()
//        {
//            return await _context.AnimalesSalida.Include(a => a.Damnificado).ToListAsync();
//        }
//    }
//}

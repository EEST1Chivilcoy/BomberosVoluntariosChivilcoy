using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Models.Grupos.Dependencias;
using Vista.Data.Models.Grupos.Dependencias.EquiposAutonomos;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Services
{
    public interface IMovimientoEquipoAutonomoService
    {
        Task CargarMovimientoAsync(int EquipoId, Movimiento_EquipoAutonomo Movimiento);
        Task<List<Movimiento_EquipoAutonomo>> ObtenerMovimientosPorEquipoAsync(int EquipoId);
    }

    public class MovimientoEquipoAutonomoService : IMovimientoEquipoAutonomoService
    {
        private readonly BomberosDbContext _context;
        private readonly IBomberoService _bomberoService;
        private readonly IDependenciaService _dependenciaService;
        private readonly IVehiculoSalidaService _vehiculoSalidaService;
        private readonly IEquipoAutonomoService _equipoAutonomoService;

        public MovimientoEquipoAutonomoService(BomberosDbContext context, IBomberoService bomberoService, IDependenciaService dependenciaService, IVehiculoSalidaService vehiculoSalidaService, IEquipoAutonomoService equipoAutonomoService)
        {
            _context = context;
            _bomberoService = bomberoService;
            _dependenciaService = dependenciaService;
            _vehiculoSalidaService = vehiculoSalidaService;
            _equipoAutonomoService = equipoAutonomoService;
        }

        public async Task CargarMovimientoAsync(int EquipoId, Movimiento_EquipoAutonomo Movimiento)
        {
            var equipo = await _equipoAutonomoService.ObtenerEquipoAutonomoAsync(EquipoId);
            var encargado = await _bomberoService.ObtenerBomberoPorIdAsync(Movimiento.EncargadoId);

            Dependencia? dependencia = null;
            VehiculoSalida? vehiculo = null;

            // ---- Validaciónes ----

            // Valida si el equipo existe. Si no, lanza una excepción para notificar el error.
            if (equipo == null)
            {
                throw new KeyNotFoundException($"No se encontró el equipo autónomo con el ID {EquipoId}.");
            }

            // Valida si el encargado existe. Si no, lanza una excepción para notificar el error.
            if (encargado == null)
            {
                throw new KeyNotFoundException($"No se encontró el bombero encargado con el ID {Movimiento.EncargadoId}.");
            }

            if (Movimiento.DependenciaAgenteId.HasValue && Movimiento.VehiculoAgenteId.HasValue)
            {
                throw new InvalidOperationException("El movimiento no puede tener asignados simultáneamente una dependencia y un vehículo.");
            }

            if (Movimiento.DependenciaAgenteId.HasValue)
            {
                dependencia = await _dependenciaService.ObtenerDependenciaPorIdAsync(Movimiento.DependenciaAgenteId.Value);

                if (dependencia == null)
                {
                    throw new KeyNotFoundException($"No se encontró la dependencia con ID: {Movimiento.DependenciaAgenteId}");
                }
            }
            else if (Movimiento.VehiculoAgenteId.HasValue)
            {
                vehiculo = await _vehiculoSalidaService.ObtenerVehiculoSalidaPorIdAsync(Movimiento.VehiculoAgenteId.Value);

                if (vehiculo == null)
                {
                    throw new KeyNotFoundException($"No se encontró el vehículo de salida con ID: {Movimiento.VehiculoAgenteId}");
                }
            }

            // Asigna el estado actual del equipo como el "EstadoAnterior" del movimiento.
            Movimiento.EstadoAnterior = equipo.Estado;

            _equipoAutonomoService.CambiarEstadoEquipoAutonomoAsync(EquipoId, Movimiento.EstadoNuevo).Wait();

            Movimiento.EquipoAutonomo = equipo;
            Movimiento.FechaMovimiento = DateTime.Now;

            // Agrega el nuevo registro de movimiento a la tabla correspondiente.
            await _context.MovimientosEquiposAutonomos.AddAsync(Movimiento);

            // Guarda todos los cambios (la actualización del nuevo movimiento) en la base de datos.
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movimiento_EquipoAutonomo>> ObtenerMovimientosPorEquipoAsync(int EquipoId)
        {
            return await _context.MovimientosEquiposAutonomos
                .Include(m => m.Encargado)
                .Include(m => m.DependenciaAgente)
                .Include(m => m.VehiculoAgente)
                .Where(m => m.EquipoAutonomoId == EquipoId)
                .OrderByDescending(m => m.FechaMovimiento)
                .ToListAsync();
        }
    }
}
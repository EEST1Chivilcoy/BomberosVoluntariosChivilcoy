using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.Dependencias;
using Vista.Data.Models.Grupos.Dependencias.EquiposAutonomos;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Services
{
    public interface IMovimientoEquipoAutonomoService
    {
        Task CargarMovimientoAsync(Movimiento_EquipoAutonomo Movimiento);
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

        public async Task CargarMovimientoAsync(Movimiento_EquipoAutonomo Movimiento)
        {
            var equipo = await _equipoAutonomoService.ObtenerEquipoAutonomoAsync(Movimiento.EquipoAutonomoId);
            var encargado = await _bomberoService.ObtenerBomberoPorIdAsync(Movimiento.EncargadoId);

            Dependencia? dependencia = null;
            VehiculoSalida? vehiculo = null;

            // ---- Validaciónes ----

            // Valida si el equipo existe.
            if (equipo == null)
            {
                throw new KeyNotFoundException($"No se encontró el equipo autónomo con el ID {Movimiento.EquipoAutonomoId}.");
            }

            // Valida si el encargado existe.
            if (encargado == null)
            {
                throw new KeyNotFoundException($"No se encontró el bombero encargado con el ID {Movimiento.EncargadoId}.");
            }

            // ---- Validación de Destino (Corregida) ----
            // Contamos cuántos destinos se especificaron.
            int destinationCount = 0;
            if (Movimiento.DependenciaDestinoId.HasValue) destinationCount++;
            if (Movimiento.VehiculoDestinoId.HasValue) destinationCount++;
            if (!string.IsNullOrWhiteSpace(Movimiento.OtroDestino)) destinationCount++;

            // Si hay más de uno, es un error.
            if (destinationCount > 1)
            {
                throw new InvalidOperationException("El movimiento solo puede tener un destino asignado a la vez (dependencia, vehículo u otro).");
            }

            // Si no hay ninguno, también es un error. Pero esto depende de el Tipo de Movimiento.
            if (Movimiento.EstadoNuevo == TipoEstadoEquipoAutonomo.Activo ||
                                 Movimiento.EstadoNuevo == TipoEstadoEquipoAutonomo.Reparacion ||
                                 Movimiento.EstadoNuevo == TipoEstadoEquipoAutonomo.PruebaHidraulica)
            {
                if (destinationCount == 0)
                {
                    throw new InvalidOperationException("El movimiento debe tener al menos un destino asignado (dependencia, vehículo u otro).");
                }
            }

            // ---- Carga de Destinos (si aplica) ----
            if (Movimiento.DependenciaDestinoId.HasValue)
            {
                dependencia = await _dependenciaService.ObtenerDependenciaPorIdAsync(Movimiento.DependenciaDestinoId.Value);
                if (dependencia == null)
                {
                    throw new KeyNotFoundException($"No se encontró la dependencia con ID: {Movimiento.DependenciaDestinoId}");
                }
                // Asignamos la entidad navegada (opcional, pero buena práctica)
                Movimiento.DependenciaDestino = dependencia;
            }
            else if (Movimiento.VehiculoDestinoId.HasValue)
            {
                vehiculo = await _vehiculoSalidaService.ObtenerVehiculoSalidaPorIdAsync(Movimiento.VehiculoDestinoId.Value);
                if (vehiculo == null)
                {
                    throw new KeyNotFoundException($"No se encontró el vehículo de salida con ID: {Movimiento.VehiculoDestinoId}");
                }
                // Asignamos la entidad navegada (opcional, pero buena práctica)
                Movimiento.VehiculoDestino = vehiculo;
            }
            else if (!string.IsNullOrWhiteSpace(Movimiento.OtroDestino))
            {
                throw new InvalidOperationException("El destino 'OtroDestino' debe ser una cadena de texto válida.");
            }

            // ---- Lógica de Agente Anterior ----
            var agenteAnterior = await _context.MovimientosEquiposAutonomos
                .Where(m => m.EquipoAutonomoId == Movimiento.EquipoAutonomoId)
                .OrderByDescending(m => m.FechaMovimiento)
                .Select(m =>
                    m.DependenciaDestinoId.HasValue ? m.DependenciaDestino.NombreDependencia :
                    m.VehiculoDestinoId.HasValue ? m.VehiculoDestino.NumeroMovil :
                    m.OtroDestino
                )
                .FirstOrDefaultAsync();

            Movimiento.AgenteAnterior = !string.IsNullOrWhiteSpace(agenteAnterior) ? agenteAnterior : "N/A";


            // ---- Actualización Atómica (Corregida) ----

            // 1. Asigna el estado actual del equipo como el "EstadoAnterior" del movimiento.
            Movimiento.EstadoAnterior = equipo.Estado;

            // 2. Modifica el estado del equipo *directamente* en la entidad que ya cargamos.
            equipo.Estado = Movimiento.EstadoNuevo;

            // 3. Configura el resto del movimiento
            Movimiento.EquipoAutonomo = equipo;
            Movimiento.Encargado = encargado; // Asignar el encargado también
            Movimiento.FechaMovimiento = DateTime.Now;

            // 4. Agrega el nuevo registro de movimiento.
            await _context.MovimientosEquiposAutonomos.AddAsync(Movimiento);

            // 5. Guarda TODOS los cambios (el estado actualizado del equipo Y el nuevo movimiento)
            //    en una sola transacción.
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movimiento_EquipoAutonomo>> ObtenerMovimientosPorEquipoAsync(int EquipoId)
        {
            return await _context.MovimientosEquiposAutonomos
                .Include(m => m.Encargado)
                .Include(m => m.DependenciaDestino)
                .Include(m => m.VehiculoDestino)
                .Where(m => m.EquipoAutonomoId == EquipoId)
                .OrderByDescending(m => m.FechaMovimiento)
                .ToListAsync();
        }
    }
}
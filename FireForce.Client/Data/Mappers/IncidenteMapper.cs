using FireForce.Client.Data.ViewModels.Incidentes;
using FireForce.Data.Models.Otros.Firmas;
using FireForce.Data.Models.Personas;
using FireForce.Data.Models.Personas.Personal;
using FireForce.Shared.Enums.Discriminadores;

namespace FireForce.Client.Data.Mappers
{
    /// <summary>
    /// Mapeo entre las entidades de Incidentes y sus ViewModels.
    /// Se hace a mano, con métodos de extensión, siguiendo el criterio del proyecto de no usar
    /// librerías de mapeo automático.
    /// </summary>
    public static class IncidenteMapper
    {
        public static IncidenteViewModel ToIncidenteViewModel(this IncidenteBase incidente)
        {
            ArgumentNullException.ThrowIfNull(incidente);

            var vm = new IncidenteViewModel
            {
                Id = incidente.Id,
                Fecha = incidente.Fecha,
                Descripcion = incidente.Descripcion,
                Categoria = incidente.Categoria,
                Estado = incidente.Estado,
                Tipo = incidente.Tipo,
                ObjetoAfectado = incidente.DescribirObjetoAfectado(),
                SolicitanteId = incidente.SolicitanteId,
                SolicitanteNombre = incidente.Solicitante.NombreCompleto(),
                SolicitanteEsBombero = incidente.Solicitante is Bombero,
                EncargadoNombre = incidente.Encargado.NombreCompleto(),
                CantidadFirmas = incidente.CantidadFirmas,
                CantidadTareas = incidente.Tareas?.Count ?? 0
            };

            if (incidente.Tareas is { Count: > 0 })
            {
                vm.Tareas = incidente.Tareas.ToArbolDeTareas();
            }

            if (incidente.Movimientos is { Count: > 0 })
            {
                vm.Movimientos = incidente.Movimientos
                    .OrderByDescending(m => m.Fecha)
                    .Select(m => m.ToMovimientoViewModel())
                    .ToList();
            }

            return vm;
        }

        public static List<IncidenteViewModel> ToIncidenteViewModelList(this List<IncidenteBase> incidentes)
        {
            ArgumentNullException.ThrowIfNull(incidentes);

            return incidentes.Select(i => i.ToIncidenteViewModel()).ToList();
        }

        public static MovimientoIncidenteViewModel ToMovimientoViewModel(this MovimientoIncidente movimiento)
        {
            ArgumentNullException.ThrowIfNull(movimiento);

            return new MovimientoIncidenteViewModel
            {
                Id = movimiento.Id,
                Fecha = movimiento.Fecha,
                Tipo = movimiento.Tipo,
                Descripcion = movimiento.Descripcion ?? string.Empty,
                AutorNombre = movimiento.Autor.NombreCompleto()
            };
        }

        public static TareaViewModel ToTareaViewModel(this Tarea tarea)
        {
            ArgumentNullException.ThrowIfNull(tarea);

            return new TareaViewModel
            {
                Id = tarea.Id,
                Nombre = tarea.Nombre,
                Descripcion = tarea.Descripcion,
                Fecha = tarea.Fecha,
                Estado = tarea.Estado,
                DependenciaNombre = tarea.Dependencia?.NombreDependencia ?? string.Empty,
                EncargadoNombre = tarea.Encargado.NombreCompleto(),
                TareaPadreId = tarea.TareaPadreId
            };
        }

        /// <summary>
        /// Arma el árbol de derivaciones a partir de la lista plana que devuelve EF: deja arriba
        /// las tareas sin padre y cuelga cada subtarea de la suya.
        /// </summary>
        public static List<TareaViewModel> ToArbolDeTareas(this ICollection<Tarea> tareas)
        {
            ArgumentNullException.ThrowIfNull(tareas);

            var porId = tareas.ToDictionary(t => t.Id, t => t.ToTareaViewModel());
            var raices = new List<TareaViewModel>();

            foreach (var tarea in porId.Values.OrderBy(t => t.Fecha))
            {
                if (tarea.TareaPadreId.HasValue && porId.TryGetValue(tarea.TareaPadreId.Value, out var padre))
                {
                    padre.Subtareas.Add(tarea);
                }
                else
                {
                    // Sin padre, o con un padre que no vino en esta consulta: va como raíz para que
                    // no desaparezca de la vista.
                    raices.Add(tarea);
                }
            }

            return raices;
        }

        /// <summary>
        /// Construye la entidad a partir del formulario. El encargado y el estado no se mapean:
        /// los resuelve <c>IncidenteService</c>.
        /// </summary>
        public static IncidenteBase ToIncidente(this IncidenteFormViewModel form)
        {
            ArgumentNullException.ThrowIfNull(form);

            return form.Tipo switch
            {
                TipoDeIncidente.Movil => new IncidenteMovil
                {
                    Descripcion = form.Descripcion.Trim(),
                    Categoria = form.Categoria,
                    SolicitanteId = form.SolicitanteId,
                    VehiculoId = form.VehiculoId ?? 0
                },
                TipoDeIncidente.Dependencia => new IncidenteDependencia
                {
                    Descripcion = form.Descripcion.Trim(),
                    Categoria = form.Categoria,
                    SolicitanteId = form.SolicitanteId,
                    DependenciaId = form.DependenciaId ?? 0
                },
                _ => throw new NotSupportedException($"Tipo de incidente no soportado: {form.Tipo}.")
            };
        }

        // ------------------------------------------------------------------
        //  Apoyo
        // ------------------------------------------------------------------

        private static string NombreCompleto(this Persona? persona) =>
            persona is null ? "—" : $"{persona.Apellido}, {persona.Nombre}".Trim(' ', ',');

        private static string DescribirObjetoAfectado(this IncidenteBase incidente) => incidente switch
        {
            IncidenteMovil movil =>
                string.IsNullOrWhiteSpace(movil.Vehiculo?.NumeroMovil)
                    ? "Móvil sin número"
                    : $"Móvil {movil.Vehiculo!.NumeroMovil}",
            IncidenteDependencia dependencia =>
                dependencia.Dependencia?.NombreDependencia ?? "Dependencia",
            _ => "—"
        };
    }
}

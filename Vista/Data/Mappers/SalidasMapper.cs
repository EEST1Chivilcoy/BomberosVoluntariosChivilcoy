using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Data.ViewModels.Accidente;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.ViewModels;
using System.Linq;
using Vista.Data.ViewModels.Rescates;

namespace Vista.Data.Mappers
{   
    public static class SalidasMapper
    {
        public static Salida ToSalida(this SalidasViewModels viewModel)
        {
            if (viewModel == null)
            {
                return null;
            }

            // Usamos un switch de tipos para determinar qué ViewModel concreto es y llamar al mapper correspondiente.
            return viewModel switch
            {
                RescatePersonaViewModels rpvm => MapToRescatePersona(rpvm),
                // Agrega aquí otros casos para los diferentes tipos de salidas que tengas
                // case IncendioViewModel ivm => MapToIncendio(ivm),
                _ => throw new ArgumentException($"Tipo de ViewModel de salida no soportado: {viewModel.GetType().Name}")
            };
        }

        /// <summary>
        /// Mapea las propiedades comunes desde cualquier SalidasViewModels a cualquier Salida.
        /// </summary>
        private static void MapBaseProperties(SalidasViewModels source, Salida destination)
        {
            destination.NumeroParte = source.NumeroParte;
            destination.AnioNumeroParte = source.AnioNumeroParte;
            destination.HoraSalida = source.HoraSalida;
            destination.HoraLlegada = source.HoraLLegada; // Nota: Tienes un typo "HoraLLegada" en tu ViewModel
            destination.Descripcion = source.Descripcion;
            destination.Direccion = source.CalleORuta;
            destination.PisoNumero = source.PisoNumero;
            destination.Depto = source.Depto;
            destination.TipoZona = source.TipoZona;
            destination.CuartelRegion = source.CuartelRegion;
            destination.Latitud = source.Latitud;
            destination.Longitud = source.Longitud;
            destination.NombreSolicitante = source.NombreSolicitante;
            destination.ApellidoSolicitante = source.ApellidoSolicitante;
            destination.DniSolicitante = source.DniSolicitante;
            destination.TelefonoSolicitante = source.TelefonoSolicitante;
            destination.NombreYApellidoReceptor = source.NombreYApellidoReceptor;
            destination.TipoServicio = source.TipoServicio;

            // Para las entidades relacionadas, solo transferimos el identificador.
            // El servicio se encargará de buscar y adjuntar la entidad completa.
            destination.Encargado = new Bombero { NumeroLegajo = source.LegajoEncargado };
            destination.QuienLleno = new Bombero { NumeroLegajo = source.LegajoLLenoPlanilla };

            // Mapeo de listas

            destination.Damnificados = source.Damnificados.ToList();
            destination.Moviles = source.Moviles.ToList();
            destination.CuerpoParticipante = source.CuerpoParticipante.ToList();
            // destination.FuerzasIntervinientes = source.FuerzasIntervinientes.ToList(); <-- Error en la conversión la lista de source es ViewModel, realizar la conversión.

        }

        private static RescatePersona MapToRescatePersona(RescatePersonaViewModels viewModel)
        {
            var rescate = new RescatePersona();
            MapBaseProperties(viewModel, rescate);

            // Mapeo de propiedades específicas de RescatePersona
            rescate.LugarRescatePersona = viewModel.TipoRescatePersona;
            rescate.OtroLugarRescate = viewModel.Otro;

            return rescate;
        }                                   

        public static RescatePersonaViewModels ToRescatePersonaViewModels(this RescatePersona model)
        {
            if (model == null) return null;

            var viewModel = new RescatePersonaViewModels
            {
                SalidaId = model.SalidaId,
                NumeroParte = model.NumeroParte,
                AnioNumeroParte = model.AnioNumeroParte,
                FechaSalida = model.HoraSalida.Date,
                TimeSalida = TimeOnly.FromDateTime(model.HoraSalida),
                TimeLlegada = TimeOnly.FromDateTime(model.HoraLlegada),
                Descripcion = model.Descripcion,
                CalleORuta = model.Direccion,
                PisoNumero = model.PisoNumero,
                Depto = model.Depto,
                TipoZona = model.TipoZona,
                CuartelRegion = model.CuartelRegion ?? 0,
                Latitud = model.Latitud,
                Longitud = model.Longitud,
                NombreSolicitante = model.NombreSolicitante,
                ApellidoSolicitante = model.ApellidoSolicitante,
                DniSolicitante = model.DniSolicitante,
                TelefonoSolicitante = model.TelefonoSolicitante,
                NombreReceptor = model.NombreYApellidoReceptor?.Split(',').LastOrDefault()?.Trim(),
                ApellidoReceptor = model.NombreYApellidoReceptor?.Split(',').FirstOrDefault()?.Trim(),
                TipoServicio = model.TipoServicio,

                // especificos de RescatePersona
                TipoRescatePersona = model.LugarRescatePersona,
                Otro = model.OtroLugarRescate,

                // Encargado y QuienLleno
                LegajoEncargado = model.Encargado?.NumeroLegajo ?? 0,
                NombreEncargado = model.Encargado?.Nombre ?? string.Empty,
                ApellidoEncargado = model.Encargado?.Apellido ?? string.Empty,
                LegajoLLenoPlanilla = model.QuienLleno?.NumeroLegajo ?? 0,
                NombreLLenoPlanilla = model.QuienLleno?.Nombre ?? string.Empty,
                ApllidoLLenoPlanilla = model.QuienLleno?.Apellido ?? string.Empty,

                // Colecciones
                Damnificados = model.Damnificados.ToList(),
                CuerpoParticipante = model.CuerpoParticipante.ToList(),
                Moviles = model.Moviles.ToList(),

                // FuerzasIntervinientes = model.FuerzasIntervinientes.ToList() <-- Error en la conversión la lista de model es ViewModel, realizar la conversión.
            };

            return viewModel;
        }
    }
}
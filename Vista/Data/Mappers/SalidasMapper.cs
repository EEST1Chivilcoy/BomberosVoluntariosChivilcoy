using System;
using System.Collections.Generic;
using System.Linq;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Data.ViewModels.Accidente;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.ViewModels;
using Vista.Data.Models.Grupos.FuerzasIntervinientes;
using Vista.Data.ViewModels.Personal;
using Vista.Data.ViewModels.Rescates;
using Vista.Data.Models.Personas;

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

            return viewModel switch
            {
                RescatePersonaViewModels rpvm => MapToRescatePersona(rpvm),
                _ => throw new ArgumentException($"Tipo de ViewModel de salida no soportado: {viewModel.GetType().Name}"),
            };
        }

        private static RescatePersona MapToRescatePersona(RescatePersonaViewModels viewModel)
        {
            var rescate = new RescatePersona();
            MapBaseProperties(viewModel, rescate);
            rescate.LugarRescatePersona = viewModel.TipoRescatePersona;
            rescate.OtroLugarRescate = viewModel.Otro;
            return rescate;
        }

        /// <summary>
        /// Mapea las propiedades comunes desde cualquier SalidasViewModels a cualquier Salida.
        /// </summary>
        private static void MapBaseProperties(SalidasViewModels source, Salida destination)
        {
            // Mapeo de propiedades directas
            destination.SalidaId = source.SalidaId;
            destination.TipoEmergencia = source.TipoEmergencia;
            destination.NumeroParte = source.NumeroParte;
            destination.AnioNumeroParte = source.AnioNumeroParte;
            destination.HoraSalida = source.HoraSalida;
            destination.HoraLlegada = source.HoraLLegada;
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

           
            if (source.LegajoEncargado > 0)
                destination.Encargado = new Bombero { NumeroLegajo = source.LegajoEncargado };

            if (source.LegajoLLenoPlanilla > 0)
                destination.QuienLleno = new Bombero { NumeroLegajo = source.LegajoLLenoPlanilla };

            // Mapeo de colecciones
            //destination.Damnificados = source.Damnificados?.Select(d => new Damnificado_Salida
            //{
            //    Damnificado_SalidaId = d.Damnificado_SalidaId,
            //    Nombre = d.Nombre,
            //    Apellido = d.Apellido,
            //    Documento = d.Documento,
            //    Edad = d.Edad,
            //    Sexo = d.Sexo,
            //    Estado = d.Estado,
            //    Destino = d.Destino,
            //    FuerzaIntervinienteId = d.FuerzaIntervinienteId,
            //    FuerzaInterviniente = null
            //}).ToList() ?? new List<Damnificado_Salida>();

            destination.Moviles = source.Moviles.ToList();

            destination.CuerpoParticipante = source.CuerpoParticipante?.Select(cp => new BomberoSalida
            {
                BomberoSalidaId = cp.BomberoSalidaId,
                PersonaId = cp.PersonaId,
                Bombero = (cp.Bombero != null && cp.Bombero.NumeroLegajo > 0) ? new Bombero { NumeroLegajo = cp.Bombero.NumeroLegajo } : null,
                Grado = cp.Grado,
                MovilId = cp.MovilId,
            }).ToList() ?? new List<BomberoSalida>();

            destination.FuerzasIntervinientes = source.FuerzasIntervinientes?.Select(fvm =>
            {
                var fi = new FuerzaInterviniente_Salida
                {
                    Id = (fvm?.Id) ?? 0,
                    FuerzaIntervinienteId = (fvm?.FuerzaViewModel?.Id) ?? 0,
                    EncargadoApellidoyNombre = fvm?.EncargadoApellidoyNombre,
                    NumeroUnidad = fvm?.NumeroUnidad,
                    // Damnificados = new List<Damnificado_Salida>() 
                };
                if (!string.IsNullOrWhiteSpace(fvm?.FuerzaViewModel?.Nombre))
                {
                    fi.Fuerzainterviniente = new FuerzaInterviniente
                    {
                        Id = fvm.FuerzaViewModel.Id,
                        NombreFuerza = fvm.FuerzaViewModel.Nombre
                    };
                }

                // if (fvm.Damnificados != null)
                // {
                //     fi.Damnificados = fvm.Damnificados.Select(dvm => new Damnificado_Salida {
                //     }).ToList();
                // }

                return fi;
            }).ToList() ?? new List<FuerzaInterviniente_Salida>();
        }
        
        public static RescatePersonaViewModels ToRescatePersonaViewModels(this RescatePersona model, List<SimpleFuerzaViewModel> todasLasFuerzas)
        {
            if (model == null) return null;

            var idsFuerzasParticipantes = model.FuerzasIntervinientes.Select(fi => fi.FuerzaIntervinienteId).ToHashSet();
             var viewModel = new RescatePersonaViewModels
            {
                SalidaId = model.SalidaId,
                TipoEmergencia = model.TipoEmergencia,
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
                NombreReceptor = model.NombreYApellidoReceptor?.Split(new[] { ',' }, 2).LastOrDefault()?.Trim(),
                ApellidoReceptor = model.NombreYApellidoReceptor?.Split(new[] { ',' }, 2).FirstOrDefault()?.Trim(),

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
                //Damnificados = model.Damnificados?.Select(d => new Damnificado_Salida
                //{
                //    Damnificado_SalidaId = d.Damnificado_SalidaId,
                //    Nombre = d.Nombre,
                //    Apellido = d.Apellido,
                //    Documento = d.Documento,
                //    Edad = d.Edad,
                //    Sexo = d.Sexo,
                //    Estado = d.Estado,
                //    Destino = d.Destino,
                //    FuerzaIntervinienteId = d.FuerzaIntervinienteId,
                //    FuerzaInterviniente = d.FuerzaInterviniente // Incluimos el objeto para la UI
                //}).ToList() ?? new List<Damnificado_Salida>(),
                CuerpoParticipante = model.CuerpoParticipante?.Select(cp => new BomberoSalida
                {
                    BomberoSalidaId = cp.BomberoSalidaId,
                    PersonaId = cp.PersonaId,
                    Bombero = cp.Bombero,
                    Grado = cp.Grado, // Aseguramos consistencia aquí también
                    MovilId = cp.MovilId,
                    MovilAsignado = cp.MovilAsignado
                }).ToList() ?? new List<BomberoSalida>(),
                BomberosParticipantes = model.CuerpoParticipante?.Select(cp => new BomberoViweModel
                {
                    Id = cp.PersonaId,
                    Nombre = cp.Bombero?.Nombre,
                    Apellido = cp.Bombero?.Apellido,
                    NumeroLegajo = cp.Bombero?.NumeroLegajo ?? 0
                }).ToList() ?? new List<BomberoViweModel>(),
                Moviles = model.Moviles.ToList(),
                FuerzasIntervinientes = model.FuerzasIntervinientes?.Select(f => new FuerzaIntervinienteViewModel
                {
                    Id = f.Id,
                    EncargadoApellidoyNombre = f.EncargadoApellidoyNombre,
                    NumeroUnidad = f.NumeroUnidad,
                    FuerzaViewModel = new SimpleFuerzaViewModel
                    {
                        Id = f.FuerzaIntervinienteId,
                        Nombre = f.Fuerzainterviniente?.NombreFuerza ?? string.Empty
                    }
                    // Damnificados mapping commented out by request
                }).ToList() ?? new List<FuerzaIntervinienteViewModel>(),
                FuerzasIntervinientesParticipantes = todasLasFuerzas.Where(f => idsFuerzasParticipantes.Contains(f.Id)).ToList()

            };

            return viewModel;
       }
    }
}
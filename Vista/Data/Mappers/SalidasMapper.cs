using System;
using System.Collections.Generic;
using System.Linq;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.ViewModels;
using Vista.Data.Models.Grupos.FuerzasIntervinientes;
using Vista.Data.ViewModels.Personal;
using Vista.Data.ViewModels.Rescates;
using Vista.Data.Models.Personas;
using Vista.Data.ViewModels.Incendios;
using Vista.Data.ViewModels.FactorClimatico;
using Vista.Data.ViewModels.Accidente;
using Vista.Data.ViewModels.MaterialesPeligrosos;
using Vista.Data.Models.Salidas.Planillas.Servicios;
using Vista.Data.Models.Salidas.Planillas.Incendios;
using Vista.Data.ViewModels.Servicios;
using Vista.Data.Enums.Salidas;

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
                RescateAnimaViewModels ravm => MapToRescateAnimal(ravm), // Asegúrate de que RescateAnimalViewModels exista
                IncendioComercioViewModels icvm => MapToIncendioComercio(icvm),
                IncendioEstablecimientoEducativoViewModels ieevm => MapToIncendioEstablecimientoEducativo(ieevm),
                IncendioEstablecimientoPublicoViewModels iepvm => MapToIncendioEstablecimientoPublico(iepvm),
                IncendioForestalViewModels ifvm => MapToIncendioForestal(ifvm),
                IncendioHospitalesYClinicasViewModels ihcvm => MapToIncendioHospitalesYClinicas(ihcvm),
                IncendioIndustriaViewModels iivm => MapToIncendioIndustria(iivm),
                IncendioViviendaViewModels ivvm => MapToIncendioVivienda(ivvm),
                MaterialPeligrosoViewModels mpvm => MapToMaterialPeligroso(mpvm),
                AccidenteViewModels avm => MapToAccidente(avm),
                IncendioAeronavesViewModels iavm => MapToIncendioAeronaves(iavm),
                IncendioOtroViewModels iovm => MapToIncendioOtro(iovm),
                FactorClimaticoViewModel fcvm => MapToFactorClimatico(fcvm),
                ServicioEspecialRepresentacionViewModels servm => MapToServicioEspecialRepresentacion(servm),
                ServicioEspecialPrevencionViewModels sepvm => MapToServicioEspecialPrevencion(sepvm),
                ServicioEspecialCapacitacionViewModel secvm => MapToServicioEspecialCapacitacion(secvm),
                ServicioEspecialColocaciónDrizaViewModels secdvm => MapToServicioEspecialColocacionDriza(secdvm),
                ServicioEspecialSuministroAguaViewModels sesavm => MapToServicioEspecialSuministroAgua(sesavm),
                ServicioEspecialFalsaAlarmaViewModel sefavm => MapToServicioEspecialFalsaAlarma(sefavm),
                ServicioEspecialRetiradoDeObitoViewModel serovm => MapToServicioEspecialRetiradoDeObito(serovm),
                ServicioEspecialColaboraciónFuerzasSeguridadViewModels secfsvm => MapToServicioEspecialColaboracionFuerzasSeguridad(secfsvm),
                _ => throw new ArgumentException($"Tipo de ViewModel de salida no soportado: {viewModel.GetType().Name}"),
            };
        }

        private static Accidente MapToAccidente(AccidenteViewModels viewModel)
        {
            var accidente = new Accidente();
            MapBaseProperties(viewModel, accidente);
            accidente.Tipo = viewModel.Tipo!.Value;
            accidente.CondicionesClimaticas = viewModel.CondicionesClimaticas!.Value;
            return accidente;
        }

        private static IncendioAeronaves MapToIncendioAeronaves(IncendioAeronavesViewModels viewModel)
        {
            var incendio = new IncendioAeronaves();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.TipoAeronave = viewModel.TipoAeronave;
            return incendio;
        }

        private static IncendioOtro MapToIncendioOtro(IncendioOtroViewModels viewModel)
        {
            var incendio = new IncendioOtro();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.OtroIncendio = viewModel.OtroIncendio;
            return incendio;
        }

        private static FactorClimatico MapToFactorClimatico(FactorClimaticoViewModel viewModel)
        {
            var factorClimatico = new FactorClimatico();
            MapBaseProperties(viewModel, factorClimatico);
            factorClimatico.Tipo = viewModel.Tipo;
            factorClimatico.Evacuacion = viewModel.Evacuacion!.Value;
            factorClimatico.Superficie = viewModel.Superficie!.Value;
            factorClimatico.CantidadAfectadaFactorClimatico = viewModel.CantidadAfectada!.Value;
            return factorClimatico;
        }

        private static MaterialPeligroso MapToMaterialPeligroso(MaterialPeligrosoViewModels viewModel)
        {
            var matpel = new MaterialPeligroso();
            MapBaseProperties(viewModel, matpel);
            matpel.Tipo = viewModel.Tipo!.Value;
            matpel.Sustancias = viewModel.Sustancias;
            matpel.Controlada = viewModel.Controlada;
            matpel.Venteo = viewModel.Venteo;
            matpel.DilucionDeVapores = viewModel.DilucionDeVapores;
            matpel.Neutralizacion = viewModel.Neutralizacion;
            matpel.Trasvase = viewModel.Trasvase;
            matpel.OtraAccionesMateriales = viewModel.OtraAccionesMateriales;
            matpel.DetallesAccionesMateriales = viewModel.DetallesAccionesMateriales;
            matpel.Evacuacion = viewModel.Evacuacion!.Value;
            matpel.Descontaminacion = viewModel.Descontaminacion;
            matpel.Confinamiento = viewModel.Confinamiento;
            matpel.SinAccion = viewModel.SinAccion;
            matpel.OtraAccionesPersonas = viewModel.OtraAccionesPersonas;
            matpel.DetallesAccionesPersonas = viewModel.DetallesAccionesPersonas;
            matpel.TipoSuperficie = viewModel.TipoSuperficie!.Value;
            matpel.CantidadAfectadaMaterialPeligroso = viewModel.Cantidad!.Value;
            matpel.TipoSituacion = viewModel.TipoSituacion!.Value;
            return matpel;
        }

        private static ServicioEspecialRepresentacion MapToServicioEspecialRepresentacion(ServicioEspecialRepresentacionViewModels viewModel)
        {
            var servicio = new ServicioEspecialRepresentacion();
            MapBaseProperties(viewModel, servicio);
            servicio.TipoOrganizacion = viewModel.TipoOrganizacion;
            servicio.TipoRepresentacion = viewModel.TipoServicioRepresentacion;
            return servicio;
        }

        private static ServicioEspecialPrevencion MapToServicioEspecialPrevencion(ServicioEspecialPrevencionViewModels viewModel)
        {
            var servicio = new ServicioEspecialPrevencion();
            MapBaseProperties(viewModel, servicio);
            servicio.TipoOrganizacion = viewModel.TipoOrganizacion!.Value;
            servicio.TipoPrevencion = viewModel.TipoPrevencion!.Value;
            return servicio;
        }

        private static ServicioEspecialCapacitacion MapToServicioEspecialCapacitacion(ServicioEspecialCapacitacionViewModel viewModel)
        {
            var servicio = new ServicioEspecialCapacitacion();
            MapBaseProperties(viewModel, servicio);
            servicio.NivelDeCapacitacion = viewModel.NivelDeCapacitacion;
            servicio.TipoCapacitacion = viewModel.TipoCapacitacion;
            servicio.DiaHora = viewModel.DiaHora ?? DateTime.MinValue;
            servicio.TipoCapacitacionOtra = viewModel.TipoCapacitacionOtra!;
            servicio.NivelDeCapacitacionOtro = viewModel.NivelDeCapacitacionOtro!;
            return servicio;
        }

        private static ServicioEspecialColocaciónDriza MapToServicioEspecialColocacionDriza(ServicioEspecialColocaciónDrizaViewModels viewModel)
        {
            var servicio = new ServicioEspecialColocaciónDriza();
            MapBaseProperties(viewModel, servicio);
            // ---> Aca cambiaron las propiedades
            servicio.NombreEstablecimiento = viewModel.NombreEstablecimiento;
            servicio.Detalles = viewModel.Detalles;
            return servicio;
        }

        private static ServicioEspecialSuministroAgua MapToServicioEspecialSuministroAgua(ServicioEspecialSuministroAguaViewModels viewModel)
        {
            var servicio = new ServicioEspecialSuministroAgua();
            MapBaseProperties(viewModel, servicio);
            // ---> Aca cambiaron las propiedades (Borrar datos viejos del Modelo)
            return servicio;
        }

        private static ServicioEspecialFalsaAlarma MapToServicioEspecialFalsaAlarma(ServicioEspecialFalsaAlarmaViewModel viewModel)
        {
            var servicio = new ServicioEspecialFalsaAlarma();
            MapBaseProperties(viewModel, servicio);
            // ---> Aca cambiaron las propiedades (Borrar datos viejos del Modelo)
            return servicio;
        }

        private static ServicioEspecialRetiradoDeObito MapToServicioEspecialRetiradoDeObito(ServicioEspecialRetiradoDeObitoViewModel viewModel)
        {
            var servicio = new ServicioEspecialRetiradoDeObito();
            MapBaseProperties(viewModel, servicio);
            // ---> Aca cambiaron las propiedades (Borrar datos viejos del Modelo)
            return servicio;
        }

        private static ServicioEspecialColaboraciónFuerzasSeguridad MapToServicioEspecialColaboracionFuerzasSeguridad(ServicioEspecialColaboraciónFuerzasSeguridadViewModels viewModel)
        {
            var servicio = new ServicioEspecialColaboraciónFuerzasSeguridad();
            MapBaseProperties(viewModel, servicio);
            servicio.DetallesColaboFuerzasSeguridad = viewModel.DetallesColaboFuerzasSeguridad;
            return servicio;
        }

        private static void MapIncendioCommonProperties(IncendioViewModels source, Incendio destination)
        {
            // ---> Aca cambiaron las propiedades
            destination.DeteccionAutomatica = source.DeteccionAutomatica;
            destination.Extintor = source.Extintor;
            destination.Hidrante = source.Hidrante;
            destination.TipoEvacuacion = source.TipoEvacuacion;
            destination.TipoSuperficieAfectada = source.TipoSuperficieAfectada!.Value;
            destination.DetalleSuperficieAfectadaIncendio = source.DetalleSuperficieAfectadaIncendio;
            destination.SuperficieAfectadaCausa = source.SuperficieAfectadaCausa;
            destination.TipoAbertura = source.TipoAbertura;
            destination.OtraAbertura = source.OtraAbertura;
            destination.TipoTecho = source.TipoTecho;
            destination.OtroTecho = source.OtroTecho;
            destination.NombreEstablecimiento = source.NombreEstablecimiento;
            destination.CantidadPisos = source.CantidadPisos;
            destination.PisoAfectado = source.PisoAfectado;
            destination.CantidadAmbientes = source.CantidadAmbientes;
        }

        private static IncendioComercio MapToIncendioComercio(IncendioComercioViewModels viewModel)
        {
            var incendio = new IncendioComercio();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.TipoLugar = viewModel.TipoLugar;
            return incendio;
        }

        private static IncendioEstablecimientoEducativo MapToIncendioEstablecimientoEducativo(IncendioEstablecimientoEducativoViewModels viewModel)
        {
            var incendio = new IncendioEstablecimientoEducativo();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.TipoLugar = viewModel.TipoLugar;
            return incendio;
        }

        private static IncendioEstablecimientoPublico MapToIncendioEstablecimientoPublico(IncendioEstablecimientoPublicoViewModels viewModel)
        {
            var incendio = new IncendioEstablecimientoPublico();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.TipoLugar = viewModel.TipoLugar!.Value;
            return incendio;
        }

        private static IncendioForestal MapToIncendioForestal(IncendioForestalViewModels viewModel)
        {
            var incendio = new IncendioForestal();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.TipoLugar = viewModel.TipoLugar;
            return incendio;
        }

        private static IncendioHospitalesYClinicas MapToIncendioHospitalesYClinicas(IncendioHospitalesYClinicasViewModels viewModel)
        {
            var incendio = new IncendioHospitalesYClinicas();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.TipoLugar = viewModel.TipoLugar;
            return incendio;
        }

        private static IncendioIndustria MapToIncendioIndustria(IncendioIndustriaViewModels viewModel)
        {
            var incendio = new IncendioIndustria();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.TipoLugar = viewModel.TipoLugar;
            return incendio;
        }

        private static IncendioVivienda MapToIncendioVivienda(IncendioViviendaViewModels viewModel)
        {
            var incendio = new IncendioVivienda();
            MapBaseProperties(viewModel, incendio);
            MapIncendioCommonProperties(viewModel, incendio);
            incendio.TipoLugar = viewModel.TipoLugar;
            return incendio;
        }
        private static RescatePersona MapToRescatePersona(RescatePersonaViewModels viewModel)
        {
            var rescate = new RescatePersona();
            MapBaseProperties(viewModel, rescate);
            rescate.LugarRescatePersona = viewModel.TipoRescatePersona!.Value;
            //rescate.OtroLugarRescate = viewModel.OtroLugar;
            return rescate;
        }

        private static RescateAnimal MapToRescateAnimal(RescateAnimaViewModels viewModel) // Asegúrate de que RescateAnimalViewModels exista
        {
            var rescate = new RescateAnimal();
            MapBaseProperties(viewModel, rescate);
            rescate.LugarRescateAnimal = viewModel.TipoRescateAnimal!.Value;
            //rescate.OtroLugarRescate = viewModel.OtroLugar;
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
            destination.Direccion = source.Direccion;
            destination.PisoNumero = source.PisoNumero;
            destination.Depto = source.Depto;
            destination.TipoZona = source.TipoZona!.Value;
            destination.CuartelRegion = source.CuartelRegion;
            destination.Latitud = source.Latitud;
            destination.Longitud = source.Longitud;
            destination.NombreSolicitante = source.NombreSolicitante;
            destination.ApellidoSolicitante = source.ApellidoSolicitante;
            destination.DniSolicitante = source.DniSolicitante;
            destination.TelefonoSolicitante = source.TelefonoSolicitante;
            destination.TipoServicio = source.TipoServicio!.Value;

            // Arreglar lo de la planilla y el encargado

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

        public static SalidasViewModels ToViewModel(this Salida model, List<SimpleFuerzaViewModel> todasLasFuerzas)
        {
            if (model == null) return null;

            // 1. Crear la instancia del ViewModel específico
            SalidasViewModels viewModel = model switch
            {
                RescatePersona _ => new RescatePersonaViewModels(), // Existing
                RescateAnimal _ => new RescateAnimaViewModels(), // Existing
                IncendioComercio _ => new IncendioComercioViewModels(),
                IncendioEstablecimientoEducativo _ => new IncendioEstablecimientoEducativoViewModels(),
                IncendioEstablecimientoPublico _ => new IncendioEstablecimientoPublicoViewModels(),
                IncendioForestal _ => new IncendioForestalViewModels(),
                IncendioHospitalesYClinicas _ => new IncendioHospitalesYClinicasViewModels(),
                IncendioIndustria _ => new IncendioIndustriaViewModels(),
                IncendioVivienda _ => new IncendioViviendaViewModels(),
                IncendioAeronaves _ => new IncendioAeronavesViewModels(),
                IncendioOtro _ => new IncendioOtroViewModels(),
                Incendio _ => new IncendioViewModels(), // Caso general al final
                MaterialPeligroso _ => new MaterialPeligrosoViewModels(),
                Accidente _ => new AccidenteViewModels(),
                FactorClimatico _ => new FactorClimaticoViewModel(),
                ServicioEspecialRepresentacion _ => new ServicioEspecialRepresentacionViewModels(),
                ServicioEspecialPrevencion _ => new ServicioEspecialPrevencionViewModels(),
                ServicioEspecialCapacitacion _ => new ServicioEspecialCapacitacionViewModel(),
                ServicioEspecialColocaciónDriza _ => new ServicioEspecialColocaciónDrizaViewModels(),
                ServicioEspecialSuministroAgua _ => new ServicioEspecialSuministroAguaViewModels(),
                ServicioEspecialFalsaAlarma _ => new ServicioEspecialFalsaAlarmaViewModel(),
                ServicioEspecialRetiradoDeObito _ => new ServicioEspecialRetiradoDeObitoViewModel(),
                ServicioEspecialColaboraciónFuerzasSeguridad _ => new ServicioEspecialColaboraciónFuerzasSeguridadViewModels(),
                _ => throw new ArgumentException($"No se encontró un mapeo de ViewModel para el tipo: {model.GetType().Name}")
            };


            // 2. Mapear todas las propiedades base comunes
            MapToBaseViewModel(model, viewModel, todasLasFuerzas);

            // 3. Mapear las propiedades específicas de cada tipo
            switch (model)
            {
                case RescatePersona RescaPerso:
                    var rpvm = (RescatePersonaViewModels)viewModel;
                    rpvm.TipoRescatePersona = RescaPerso.LugarRescatePersona;
                    //rpvm.OtroLugar = RescaPerso.OtroLugarRescate;
                    break;
                case RescateAnimal RescaAnima:
                    var ravm = (RescateAnimaViewModels)viewModel;
                    ravm.TipoRescateAnimal = RescaAnima.LugarRescateAnimal;
                    //ravm.OtroLugar = RescaAnima.OtroLugarRescate;
                    break;
                case IncendioComercio IncenComer:
                    var icvm = (IncendioComercioViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenComer, icvm);
                    icvm.TipoLugar = IncenComer.TipoLugar;
                    break;
                case IncendioEstablecimientoEducativo IncenEstaEdu:
                    var ieevm = (IncendioEstablecimientoEducativoViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenEstaEdu, ieevm);
                    ieevm.TipoLugar = IncenEstaEdu.TipoLugar;
                    break;
                case IncendioEstablecimientoPublico IncenEstaPubli:
                    var iepvm = (IncendioEstablecimientoPublicoViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenEstaPubli, iepvm);
                    iepvm.TipoLugar = IncenEstaPubli.TipoLugar;
                    break;
                case IncendioForestal IncenFore:
                    var ifovm = (IncendioForestalViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenFore, ifovm);
                    ifovm.TipoLugar = IncenFore.TipoLugar;
                    break;
                case IncendioHospitalesYClinicas IncenHospiClini:
                    var ihcvm = (IncendioHospitalesYClinicasViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenHospiClini, ihcvm);
                    ihcvm.TipoLugar = IncenHospiClini.TipoLugar;
                    break;
                case IncendioIndustria IncenIndus:
                    var iivm = (IncendioIndustriaViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenIndus, iivm);
                    iivm.TipoLugar = IncenIndus.TipoLugar;
                    break;
                case IncendioVivienda IncenVivien:
                    var ivvm = (IncendioViviendaViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenVivien, ivvm);
                    ivvm.TipoLugar = IncenVivien.TipoLugar;
                    break;
                case IncendioAeronaves IncenAero:
                    var iavm = (IncendioAeronavesViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenAero, iavm);
                    iavm.TipoAeronave = IncenAero.TipoAeronave;
                    break;
                case IncendioOtro IncenOtro:
                    var iovm = (IncendioOtroViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(IncenOtro, iovm);
                    iovm.OtroIncendio = IncenOtro.OtroIncendio;
                    break;
                case Incendio Incen: // Caso general movido al final de los incendios
                    var ivm = (IncendioViewModels)viewModel;
                    MapIncendioCommonViewModelProperties(Incen, ivm);
                    break;
                case MaterialPeligroso mp:
                    var mpvm = (MaterialPeligrosoViewModels)viewModel;
                    mpvm.Tipo = mp.Tipo;
                    mpvm.Sustancias = mp.Sustancias;
                    mpvm.Controlada = mp.Controlada;
                    mpvm.Venteo = mp.Venteo;
                    mpvm.DilucionDeVapores = mp.DilucionDeVapores;
                    mpvm.Neutralizacion = mp.Neutralizacion;
                    mpvm.Trasvase = mp.Trasvase;
                    mpvm.OtraAccionesMateriales = mp.OtraAccionesMateriales;
                    mpvm.DetallesAccionesMateriales = mp.DetallesAccionesMateriales;
                    mpvm.Evacuacion = mp.Evacuacion;
                    mpvm.Descontaminacion = mp.Descontaminacion;
                    mpvm.Confinamiento = mp.Confinamiento;
                    mpvm.SinAccion = mp.SinAccion;
                    mpvm.OtraAccionesPersonas = mp.OtraAccionesPersonas;
                    mpvm.DetallesAccionesPersonas = mp.DetallesAccionesPersonas;
                    mpvm.TipoSuperficie = mp.TipoSuperficie;
                    mpvm.Cantidad = mp.CantidadAfectadaMaterialPeligroso;
                    mpvm.TipoSituacion = mp.TipoSituacion;
                    break;
                case Accidente a:
                    var avm = (AccidenteViewModels)viewModel;
                    avm.Tipo = a.Tipo;
                    avm.CondicionesClimaticas = a.CondicionesClimaticas;
                    //avm.OtroCondicion = a.OtroCondicion;
                    break;
                case FactorClimatico fc:
                    var fcvm = (FactorClimaticoViewModel)viewModel;
                    fcvm.Tipo = fc.Tipo;
                    fcvm.Evacuacion = fc.Evacuacion;
                    fcvm.Superficie = fc.Superficie;
                    fcvm.CantidadAfectada = fc.CantidadAfectadaFactorClimatico;
                    break;
                case ServicioEspecialRepresentacion ser:
                    var servm = (ServicioEspecialRepresentacionViewModels)viewModel;
                    servm.Tipo = ServicioEspecialTipo.Representacion;
                    servm.TipoOrganizacion = ser.TipoOrganizacion;
                    servm.TipoServicioRepresentacion = ser.TipoRepresentacion;
                    break;
                case ServicioEspecialPrevencion sep:
                    var sepvm = (ServicioEspecialPrevencionViewModels)viewModel;
                    sepvm.Tipo = ServicioEspecialTipo.Prevencion;
                    sepvm.TipoOrganizacion = sep.TipoOrganizacion;
                    sepvm.TipoPrevencion = sep.TipoPrevencion;
                    break;
                case ServicioEspecialCapacitacion sec:
                    var secvm = (ServicioEspecialCapacitacionViewModel)viewModel;
                    secvm.Tipo = ServicioEspecialTipo.Capacitacion;
                    secvm.NivelDeCapacitacion = sec.NivelDeCapacitacion;
                    secvm.TipoCapacitacion = sec.TipoCapacitacion;
                    secvm.DiaHora = (sec.DiaHora == DateTime.MinValue) ? (DateTime?)null : sec.DiaHora; // Convierte a null si es el valor por defecto
                    secvm.TipoCapacitacionOtra = sec.TipoCapacitacionOtra;
                    secvm.NivelDeCapacitacionOtro = sec.NivelDeCapacitacionOtro;
                    break;
                case ServicioEspecialColocaciónDriza secd:
                    var secdvm = (ServicioEspecialColocaciónDrizaViewModels)viewModel;
                    secdvm.Tipo = ServicioEspecialTipo.ColocacionDriza;
                    //secdvm.TipoLugar = secd.TipoLugar;
                    secdvm.NombreEstablecimiento = secd.NombreEstablecimiento;
                    secdvm.Detalles = secd.Detalles;
                    break;
                case ServicioEspecialSuministroAgua sesa:
                    var sesavm = (ServicioEspecialSuministroAguaViewModels)viewModel;
                    sesavm.Tipo = ServicioEspecialTipo.SuministroAgua;
                    //sesavm.NombreEstablecimientoSuministroAgua = sesa.NombreEstablecimientoSuministroAgua;
                    //sesavm.DetallesSuministroAgua = sesa.DetallesSuministroAgua;
                    break;
                case ServicioEspecialFalsaAlarma sefa:
                    var sefavm = (ServicioEspecialFalsaAlarmaViewModel)viewModel;
                    sefavm.Tipo = ServicioEspecialTipo.FalsaAlarma;
                    //sefavm.Detalles = sefa.Detalles;
                    break;
                case ServicioEspecialRetiradoDeObito sero:
                    var serovm = (ServicioEspecialRetiradoDeObitoViewModel)viewModel;
                    serovm.Tipo = ServicioEspecialTipo.RetiradoDeObito;
                    //serovm.DetallesObito = sero.DetallesObito;
                    break;
                case ServicioEspecialColaboraciónFuerzasSeguridad secfs:
                    var secfsvm = (ServicioEspecialColaboraciónFuerzasSeguridadViewModels)viewModel;
                    secfsvm.Tipo = ServicioEspecialTipo.ColaboracionFuerzasSeguridad;
                    secfsvm.DetallesColaboFuerzasSeguridad = secfs.DetallesColaboFuerzasSeguridad;
                    break;
            }

            return viewModel;
        }

        // Helper para mapear propiedades comunes de Incendio a IncendioViewModels
        private static void MapIncendioCommonViewModelProperties(Incendio source, IncendioViewModels destination)
        {
            destination.DeteccionAutomatica = source.DeteccionAutomatica;
            destination.Extintor = source.Extintor;
            destination.Hidrante = source.Hidrante;
            destination.TipoEvacuacion = source.TipoEvacuacion;
            destination.TipoSuperficieAfectada = source.TipoSuperficieAfectada;
            destination.DetalleSuperficieAfectadaIncendio = source.DetalleSuperficieAfectadaIncendio;
            destination.SuperficieAfectadaCausa = source.SuperficieAfectadaCausa;
            destination.TipoAbertura = source.TipoAbertura;
            destination.OtraAbertura = source.OtraAbertura;
            destination.TipoTecho = source.TipoTecho;
            destination.OtroTecho = source.OtroTecho;
            destination.NombreEstablecimiento = source.NombreEstablecimiento;
            destination.CantidadPisos = source.CantidadPisos;
            destination.PisoAfectado = source.PisoAfectado;
            destination.CantidadAmbientes = source.CantidadAmbientes;
        }

        private static void MapToBaseViewModel(Salida model, SalidasViewModels viewModel, List<SimpleFuerzaViewModel> todasLasFuerzas)
        {
            var idsFuerzasParticipantes = model.FuerzasIntervinientes.Select(fi => fi.FuerzaIntervinienteId).ToHashSet();
            viewModel.SalidaId = model.SalidaId;
            viewModel.TipoEmergencia = model.TipoEmergencia;
            viewModel.NumeroParte = model.NumeroParte;
            viewModel.AnioNumeroParte = model.AnioNumeroParte;
            viewModel.FechaSalida = model.HoraSalida.Date;
            viewModel.TimeSalida = TimeOnly.FromDateTime(model.HoraSalida);
            viewModel.TimeLlegada = TimeOnly.FromDateTime(model.HoraLlegada);
            viewModel.Descripcion = model.Descripcion;
            viewModel.Direccion = model.Direccion;
            viewModel.PisoNumero = model.PisoNumero;
            viewModel.Depto = model.Depto;
            viewModel.TipoZona = model.TipoZona;
            viewModel.CuartelRegion = model.CuartelRegion ?? 0;
            viewModel.Latitud = model.Latitud;
            viewModel.Longitud = model.Longitud;
            viewModel.NombreSolicitante = model.NombreSolicitante;
            viewModel.ApellidoSolicitante = model.ApellidoSolicitante;
            viewModel.DniSolicitante = model.DniSolicitante;
            viewModel.TelefonoSolicitante = model.TelefonoSolicitante;
            viewModel.NombreReceptor = model.NombreYApellidoReceptor?.Split(new[] { ',' }, 2).LastOrDefault()?.Trim();
            viewModel.ApellidoReceptor = model.NombreYApellidoReceptor?.Split(new[] { ',' }, 2).FirstOrDefault()?.Trim();
            viewModel.TipoServicio = model.TipoServicio;
            viewModel.CuerpoParticipante = model.CuerpoParticipante?.Select(cp => new BomberoSalida { BomberoSalidaId = cp.BomberoSalidaId, PersonaId = cp.PersonaId, Bombero = cp.Bombero, Grado = cp.Grado, MovilId = cp.MovilId, MovilAsignado = cp.MovilAsignado }).ToList() ?? new List<BomberoSalida>();
            viewModel.BomberosParticipantes = model.CuerpoParticipante?.Select(cp => new BomberoViweModel { Id = cp.PersonaId, Nombre = cp.Bombero?.Nombre, Apellido = cp.Bombero?.Apellido, NumeroLegajo = cp.Bombero?.NumeroLegajo ?? 0 }).ToList() ?? new List<BomberoViweModel>();
            viewModel.Moviles = model.Moviles.ToList();
            viewModel.FuerzasIntervinientes = model.FuerzasIntervinientes?.Select(f => new FuerzaIntervinienteViewModel { Id = f.Id, EncargadoApellidoyNombre = f.EncargadoApellidoyNombre, NumeroUnidad = f.NumeroUnidad, FuerzaViewModel = new SimpleFuerzaViewModel { Id = f.FuerzaIntervinienteId, Nombre = f.Fuerzainterviniente?.NombreFuerza ?? string.Empty } }).ToList() ?? new List<FuerzaIntervinienteViewModel>();
        }
    }
}
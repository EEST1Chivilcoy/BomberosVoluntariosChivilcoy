﻿﻿﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Data.ViewModels.Rescates;
using Vista.Services;
using AntDesign;
using Vista.Data.Models.Vehiculos.Flota;
using Vista.Data.Mappers;
using DocumentFormat.OpenXml.Office2010.Drawing;
using Vista.Data.Enums.Salidas;
using System.Threading.Tasks;
using Vista.Data.ViewModels.Personal;
using Vista.Data.Enums;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.ViewModels.Incendios;
using Vista.Data.ViewModels; // Asegúrate de que este using esté presente para SalidasViewModels
using Vista.Data.Models.Salidas.Planillas.Incendios;

namespace Vista.Pages.Salidas
{
    public partial class Incendios
    {
        // ViewModel para la carga de Incendios.
        private SalidasViewModels IncendioViewModel = new IncendioComercioViewModels();

        // Listas de datos
        private List<Bombero> BomberosTodos = new();

        // Lista con todos los vehiculos de la flota del sistema.
        private List<VehiculoSalida> MovilesTodos = new();
        // Variables tipo boolean para indicar las partes del formulario completadas.
        private bool _parte1Completa = false;
        private bool _parte2Completa = false;
        private bool _parte3Completa = false;

        // Variables parametros para la carga de Rescates.

        // Numero de Salida del Año en Seleccionado.
        [Parameter]
        [SupplyParameterFromQuery] public int? NumeroSalida { get; set; }

        // Anio de Salida del Año en Seleccionado.
        [Parameter]
        [SupplyParameterFromQuery] public int? AnioSalida { get; set; }
        
        // Tipo de Incendio.
        [Parameter]
        public int TipoIncendio { get; set; }


        // Funcion de Carga de Salida.
        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                var incendio = IncendioViewModel.ToSalida() as Incendio
                    ?? throw new InvalidOperationException("Error al convertir el ViewModel a la entidad de incendio.");

                Salida salidaGuardada;

                if (IncendioViewModel.SalidaId > 0) // Asumimos que si tiene ID, es una edición
                {                    salidaGuardada = await SalidaService.EditarSalida(incendio);
                    await message.SuccessAsync("Salida editada correctamente.");
                    HandleOk1(salidaGuardada.AnioNumeroParte, salidaGuardada.NumeroParte);                }
                else
                {
                    // Modo creación
                    var numeroParteExistente = await SalidaService.ObtenerSalidaPorNumeroParteAsync<Incendio>(
                        IncendioViewModel.NumeroParte,
                        m => m.NumeroParte == IncendioViewModel.NumeroParte && m.AnioNumeroParte == IncendioViewModel.AnioNumeroParte
                    );

                    if (numeroParteExistente != null)
                    {
                        await message.WarningAsync($"El número de parte {IncendioViewModel.NumeroParte}/{IncendioViewModel.AnioNumeroParte} ya existe.");
                        return;
                    }

                    salidaGuardada = await SalidaService.GuardarSalida(incendio);
                    await message.SuccessAsync("Salida guardada correctamente.");
                    HandleOk1(salidaGuardada.AnioNumeroParte, salidaGuardada.NumeroParte);
                }

                if (salidaGuardada == null)
                    throw new Exception("No se pudo guardar o editar la salida.");
                await Init();
                StateHasChanged();
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    await message.ErrorAsync(e.InnerException.Message, 5);
                else
                    await message.ErrorAsync(e.Message, 5);
            }
        }

        // Inicio
        protected override async Task OnInitializedAsync()
        {
            await Init();
        }

        private async Task Init()
        {
            BomberosTodos = await BomberoService.ObtenerTodosLosBomberosAsync();
            MovilesTodos = await VehiculoSalidaService.ObtenerVehiculosSalidasPorEstadoAsync(TipoEstadoMovil.Activo);

            // Si se pasan parámetros, puede ser modo Visualización/Edición
            if (NumeroSalida.HasValue && AnioSalida.HasValue && NumeroSalida.Value > 0 && AnioSalida.Value > 0)
            {
                var salidaAEditar = await SalidaService.ObtenerSalidaParaEditarAsync<Incendio>(NumeroSalida.Value, AnioSalida.Value);

                if (salidaAEditar != null)
                {
                    var todasLasFuerzas = await FuerzaIntervinienteService.ObtenerTodasLasFuerzasAsync();
                    var fuerzasVM = todasLasFuerzas.Select(f => new Vista.Data.ViewModels.Personal.SimpleFuerzaViewModel { Id = f.Id, Nombre = f.NombreFuerza }).ToList();

                    IncendioViewModel = salidaAEditar.ToViewModel(fuerzasVM);
                }
                else
                {
                    await message.WarningAsync("No se encontró la salida solicitada. Se abrirá el formulario en modo creación.");
                    if (AnioSalida.HasValue && AnioSalida.Value > 0) IncendioViewModel.AnioNumeroParte = AnioSalida.Value;
                    if (NumeroSalida.HasValue && NumeroSalida.Value > 0) IncendioViewModel.NumeroParte = NumeroSalida.Value;
                }

                return; // Finaliza la inicialización aquí para el modo edición/visualización
            }

            // Modo Creación
            IncendioViewModel = CrearViewModelPorTipo((TipoDeEmergencia)TipoIncendio);

            // Modo crear (se inicializa arriba, aquí se setean los valores por defecto)
            if (AnioSalida.HasValue && AnioSalida.Value > 0)
                IncendioViewModel.AnioNumeroParte = AnioSalida.Value;
            else
                IncendioViewModel.AnioNumeroParte = DateTime.Now.Year;

            if (NumeroSalida.HasValue && NumeroSalida.Value > 0)
                IncendioViewModel.NumeroParte = NumeroSalida.Value;
            else
                IncendioViewModel.NumeroParte = await SalidaService.ObtenerUltimoNumeroParteDelAnioAsync(IncendioViewModel.AnioNumeroParte) + 1;
        }

        private SalidasViewModels CrearViewModelPorTipo(TipoDeEmergencia tipo)
        {
            return tipo switch
            {
                TipoDeEmergencia.IncendioComercio => new IncendioComercioViewModels(),
                TipoDeEmergencia.IncendioEstablecimientoEducativo => new IncendioEstablecimientoEducativoViewModels(),
                TipoDeEmergencia.IncendioEstablecimientoPublico => new IncendioEstablecimientoPublicoViewModels(),
                TipoDeEmergencia.IncendioForestal => new IncendioForestaViewModels(),
                TipoDeEmergencia.IncendioHospitalesYClinicas => new IncendioHospitalesYClinicasViewModels(),
                TipoDeEmergencia.IncendioIndustria => new IncendioIndustriaViewModels(),
                TipoDeEmergencia.IncendioVivienda => new IncendioViviendaViewModels(),
                TipoDeEmergencia.IncendioAeronaves => new IncendioAeronavesViewModels(),
                TipoDeEmergencia.Incendio => new IncendioOtroViewModels(),
                _ => CrearViewModelNoReconocido()
            };
        }

        private SalidasViewModels CrearViewModelNoReconocido()
        {
            message.ErrorAsync("Tipo de incendio no reconocido. Se usará un formulario base.");
            // Devuelve un ViewModel por defecto para evitar un NullReferenceException
            return new IncendioComercioViewModels();
        }

        //Finish Failed
        private void OnFinishFailed(EditContext editContext)
        {
            message.Error("Error al cargar, posible información ausente");
             Console.WriteLine($"Failed:{System.Text.Json.JsonSerializer.Serialize(IncendioViewModel)}");
        }

        //Impresión
        bool _visible1;

        public int ImprimirAnio;
        public int ImprimirNumeroParte;
        void HandleOk1(int _anio, int _numeroParte)
        {
            ImprimirAnio = _anio;
            ImprimirNumeroParte = _numeroParte;
            _visible1 = true;
        }
    }
}

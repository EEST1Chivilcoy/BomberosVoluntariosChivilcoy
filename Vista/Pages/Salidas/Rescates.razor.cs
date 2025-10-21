﻿﻿﻿﻿﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Data.ViewModels.Rescates;
using Vista.Services;
using AntDesign;
using Vista.Data.Models.Vehiculos.Flota;
using Vista.Data.Mappers;
using DocumentFormat.OpenXml.Office2010.Drawing;
using System.Threading.Tasks;
using Vista.Data.ViewModels.Personal;
using Vista.Data.Enums;

namespace Vista.Pages.Salidas
{
    public partial class Rescates
    {
        // ViewModel para la carga de Rescates de Personas.
        private RescatePersonaViewModels PersonaViewModel = new();

        // Lista con todos los Bomberos del sistema.
        private List<Bombero> BomberosTodos = new();

        // Lista con todos los vehiculos de la flota del sistema.
        private List<VehiculoSalida> MovilesTodos = new();
        private bool _isEditMode = false;

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
        
        // Tipo de Rescate.
        [Parameter]
        public int TipoRescate { get; set; }


        // Funcion de Carga de Salida.
        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                var rescate = PersonaViewModel.ToSalida() as RescatePersona
                    ?? throw new InvalidOperationException("Error al convertir el ViewModel a la entidad de rescate.");

                Salida salidaGuardada;

                if (_isEditMode)
                {
                    // Modo edición
                    salidaGuardada = await SalidaService.EditarSalida(rescate);
                    await message.SuccessAsync("Salida editada correctamente.");
                    HandleOk1(salidaGuardada.AnioNumeroParte, salidaGuardada.NumeroParte);
                }
                else
                {
                    // Modo creación
                    var numeroParteExistente = await SalidaService.ObtenerSalidaPorNumeroParteAsync<RescatePersona>(
                        PersonaViewModel.NumeroParte,
                        m => m.NumeroParte == PersonaViewModel.NumeroParte && m.AnioNumeroParte == PersonaViewModel.AnioNumeroParte
                    );

                    if (numeroParteExistente != null)
                    {
                        await message.WarningAsync("El número de parte ya existe para el año seleccionado.");
                        return;
                    }

                    salidaGuardada = await SalidaService.GuardarSalida(rescate);
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

            // Inicial defaults
            _isEditMode = false;
            PersonaViewModel = new RescatePersonaViewModels();

            // Modo edición
            if (NumeroSalida.HasValue && AnioSalida.HasValue && NumeroSalida.Value > 0 && AnioSalida.Value > 0)
            {
                var salidaAEditar = await SalidaService.ObtenerSalidaParaEditarAsync<RescatePersona>(NumeroSalida.Value, AnioSalida.Value);

                if (salidaAEditar != null && salidaAEditar is RescatePersona rp)
                {
                    var todasLasFuerzas = await FuerzaIntervinienteService.ObtenerTodasLasFuerzasAsync();
                    var fuerzasVM = todasLasFuerzas.Select(f => new Vista.Data.ViewModels.Personal.SimpleFuerzaViewModel { Id = f.Id, Nombre = f.NombreFuerza }).ToList();

                    PersonaViewModel = rp.ToRescatePersonaViewModels(fuerzasVM);
                   _isEditMode = true;
                }
                else
                {
                    await message.WarningAsync("No se encontró la salida solicitada. Se abrirá el formulario en modo creación.");
                    PersonaViewModel = new RescatePersonaViewModels();
                    if (AnioSalida.HasValue && AnioSalida.Value > 0) PersonaViewModel.AnioNumeroParte = AnioSalida.Value;
                    if (NumeroSalida.HasValue && NumeroSalida.Value > 0) PersonaViewModel.NumeroParte = NumeroSalida.Value;
                    _isEditMode = false;
                }

                return;
            }

            // Modo crear
            PersonaViewModel = new RescatePersonaViewModels();
            if (AnioSalida.HasValue && AnioSalida.Value > 0)
                PersonaViewModel.AnioNumeroParte = AnioSalida.Value;
            else
                PersonaViewModel.AnioNumeroParte = DateTime.Now.Year;

            if (NumeroSalida.HasValue && NumeroSalida.Value > 0)
                PersonaViewModel.NumeroParte = NumeroSalida.Value;
            else
                PersonaViewModel.NumeroParte = await SalidaService.ObtenerUltimoNumeroParteDelAnioAsync(PersonaViewModel.AnioNumeroParte) + 1;

            _isEditMode = false;
        }
        //Finish Failed
        private void OnFinishFailed(EditContext editContext)
        {
            message.Error("Error al cargar, posible información ausente");
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(PersonaViewModel)}");
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

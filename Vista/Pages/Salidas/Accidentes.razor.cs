using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Services;
using AntDesign;
using Vista.Data.Models.Vehiculos.Flota;
using Vista.Data.Mappers;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.Enums.Salidas;
using System.Threading.Tasks;
using Vista.Data.ViewModels.Personal;
using Vista.Data.Enums;
using Vista.Data.ViewModels;
using Vista.Data.ViewModels.Accidente;

namespace Vista.Pages.Salidas
{
    public partial class Accidentes
    {
        // ViewModel para la carga de Accidentes.
        private AccidenteViewModels AccidenteViewModel = new AccidenteViewModels();

        // Listas de datos
        private List<Bombero> BomberosTodos = new();

        // Lista con todos los vehiculos de la flota del sistema.
        private List<VehiculoSalida> MovilesTodos = new();
        // Variables tipo boolean para indicar las partes del formulario completadas.
        private bool _parte1Completa = false;
        private bool _parte2Completa = false;
        private bool _parte3Completa = false;

        // Variables parametros para la carga de Accidentes.

        // Numero de Salida del Año en Seleccionado.
        [Parameter]
        [SupplyParameterFromQuery] public int? NumeroSalida { get; set; }

        // Anio de Salida del Año en Seleccionado.
        [Parameter]
        [SupplyParameterFromQuery] public int? AnioSalida { get; set; }
        
        // Tipo de Accidente.
        [Parameter]
        public int TipoAccidente { get; set; }


        // Funcion de Carga de Salida.
        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                var accidente = AccidenteViewModel.ToSalida() as Accidente
                    ?? throw new InvalidOperationException("Error al convertir el ViewModel a la entidad de accidente.");

                Salida salidaGuardada;

                if (AccidenteViewModel.SalidaId > 0)
                {
                    salidaGuardada = await SalidaService.EditarSalida(accidente);
                    await message.SuccessAsync("Salida editada correctamente.");
                    HandleOk1(salidaGuardada.AnioNumeroParte, salidaGuardada.NumeroParte);
                }
                else
                {
                    // Modo creación
                    var numeroParteExistente = await SalidaService.ObtenerSalidaPorNumeroParteAsync<Accidente>(
                        AccidenteViewModel.NumeroParte,
                        m => m.NumeroParte == AccidenteViewModel.NumeroParte && m.AnioNumeroParte == AccidenteViewModel.AnioNumeroParte
                    );

                    if (numeroParteExistente != null)
                    {
                        await message.WarningAsync($"El número de parte {AccidenteViewModel.NumeroParte}/{AccidenteViewModel.AnioNumeroParte} ya existe.");
                        return;
                    }


                    salidaGuardada = await SalidaService.GuardarSalida(accidente);
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
            
           AccidenteViewModel = new AccidenteViewModels();


            


            // Si se pasan parámetros, puede ser modo Visualización/Edición
            if (NumeroSalida.HasValue && AnioSalida.HasValue && NumeroSalida.Value > 0 && AnioSalida.Value > 0)
            {
                var salidaAEditar = await SalidaService.ObtenerSalidaParaEditarAsync<Accidente>(NumeroSalida.Value, AnioSalida.Value);

                if (salidaAEditar != null)
                {
                    var todasLasFuerzas = await FuerzaIntervinienteService.ObtenerTodasLasFuerzasAsync();
                    var fuerzasVM = todasLasFuerzas.Select(f => new Vista.Data.ViewModels.Personal.SimpleFuerzaViewModel { Id = f.Id, Nombre = f.NombreFuerza }).ToList();

                    AccidenteViewModel = (AccidenteViewModels)salidaAEditar.ToViewModel(fuerzasVM);
                }
                else
                {
                    await message.WarningAsync("No se encontró la salida solicitada. Se abrirá el formulario en modo creación.");
                    if (AnioSalida.HasValue && AnioSalida.Value > 0) AccidenteViewModel.AnioNumeroParte = AnioSalida.Value;
                    if (NumeroSalida.HasValue && NumeroSalida.Value > 0) AccidenteViewModel.NumeroParte = NumeroSalida.Value;
                }

                return;
            }

            // Modo crear (se inicializa arriba, aquí se setean los valores por defecto)
            AccidenteViewModel.TipoEmergencia = TipoDeEmergencia.Accidente;
            AccidenteViewModel.Tipo = (Vista.Data.Enums.TipoAccidente)TipoAccidente;
            if (AnioSalida.HasValue && AnioSalida.Value > 0)
                AccidenteViewModel.AnioNumeroParte = AnioSalida.Value;
            else
            AccidenteViewModel.AnioNumeroParte = DateTime.Now.Year;

            if (NumeroSalida.HasValue && NumeroSalida.Value > 0)
                AccidenteViewModel.NumeroParte = NumeroSalida.Value;
            else
                AccidenteViewModel.NumeroParte = await SalidaService.ObtenerUltimoNumeroParteDelAnioAsync(AccidenteViewModel.AnioNumeroParte) + 1;
        }
        //Finish Failed
        private void OnFinishFailed(EditContext editContext)
        {
            message.Error("Error al cargar, posible información ausente.");
             Console.WriteLine($"Failed:{System.Text.Json.JsonSerializer.Serialize(AccidenteViewModel)}");
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

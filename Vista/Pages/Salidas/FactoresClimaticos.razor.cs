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
using Vista.Data.ViewModels.FactorClimatico;

namespace Vista.Pages.Salidas
{
    public partial class FactoresClimaticos
    {
        // ViewModel para la carga de Factores Climáticos.
        private FactorClimaticoViewModel FactorClimaticoViewModel = new FactorClimaticoViewModel();

        // Listas de datos
        private List<Bombero> BomberosTodos = new();

        // Lista con todos los vehiculos de la flota del sistema.
        private List<VehiculoSalida> MovilesTodos = new();
        // Variables tipo boolean para indicar las partes del formulario completadas.
        private bool _parte1Completa = false;
        private bool _parte2Completa = false;
        private bool _parte3Completa = false;

        // Variables parametros para la carga de Factores Climáticos.

        // Numero de Salida del Año en Seleccionado.
        [Parameter]
        [SupplyParameterFromQuery] public int? NumeroSalida { get; set; }

        // Anio de Salida del Año en Seleccionado.
        [Parameter]
        [SupplyParameterFromQuery] public int? AnioSalida { get; set; }
        
        // Tipo de Factor Climático.
        [Parameter]
        public int TipoFactorClimatico { get; set; }


        // Funcion de Carga de Salida.
        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                var factorClimatico = FactorClimaticoViewModel.ToSalida() as FactorClimatico
                    ?? throw new InvalidOperationException("Error al convertir el ViewModel a la entidad de Factor Climático.");

                Salida salidaGuardada;

                if (FactorClimaticoViewModel.SalidaId > 0)
                {
                    salidaGuardada = await SalidaService.EditarSalida(factorClimatico);
                    await message.SuccessAsync("Salida editada correctamente.");
                    HandleOk1(salidaGuardada.AnioNumeroParte, salidaGuardada.NumeroParte);
                }
                else
                {
                    // Modo creación
                    var numeroParteExistente = await SalidaService.ObtenerSalidaPorNumeroParteAsync<FactorClimatico>(
                        FactorClimaticoViewModel.NumeroParte,
                        m => m.NumeroParte == FactorClimaticoViewModel.NumeroParte && m.AnioNumeroParte == FactorClimaticoViewModel.AnioNumeroParte
                    );

                    if (numeroParteExistente != null)
                    {
                        await message.WarningAsync($"El número de parte {FactorClimaticoViewModel.NumeroParte}/{FactorClimaticoViewModel.AnioNumeroParte} ya existe.");
                        return;
                    }


                    salidaGuardada = await SalidaService.GuardarSalida(factorClimatico);
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
            
           FactorClimaticoViewModel = new FactorClimaticoViewModel();


            


            // Si se pasan parámetros, puede ser modo Visualización/Edición
            if (NumeroSalida.HasValue && AnioSalida.HasValue && NumeroSalida.Value > 0 && AnioSalida.Value > 0)
            {
                var salidaAEditar = await SalidaService.ObtenerSalidaParaEditarAsync<FactorClimatico>(NumeroSalida.Value, AnioSalida.Value);

                if (salidaAEditar != null)
                {
                    var todasLasFuerzas = await FuerzaIntervinienteService.ObtenerTodasLasFuerzasAsync();
                    var fuerzasVM = todasLasFuerzas.Select(f => new Vista.Data.ViewModels.Personal.SimpleFuerzaViewModel { Id = f.Id, Nombre = f.NombreFuerza }).ToList();

                    FactorClimaticoViewModel = (FactorClimaticoViewModel)salidaAEditar.ToViewModel(fuerzasVM);
                }
                else
                {
                    await message.WarningAsync("No se encontró la salida solicitada. Se abrirá el formulario en modo creación.");
                    if (AnioSalida.HasValue && AnioSalida.Value > 0) FactorClimaticoViewModel.AnioNumeroParte = AnioSalida.Value;
                    if (NumeroSalida.HasValue && NumeroSalida.Value > 0) FactorClimaticoViewModel.NumeroParte = NumeroSalida.Value;
                }

                return;
            }

            // Modo crear (se inicializa arriba, aquí se setean los valores por defecto)
            FactorClimaticoViewModel.TipoEmergencia = TipoDeEmergencia.FactorClimatico;
            FactorClimaticoViewModel.Tipo = (TipoFactoresClimaticos)TipoFactorClimatico;
            if (AnioSalida.HasValue && AnioSalida.Value > 0)
                FactorClimaticoViewModel.AnioNumeroParte = AnioSalida.Value;
            else
            FactorClimaticoViewModel.AnioNumeroParte = DateTime.Now.Year;

            if (NumeroSalida.HasValue && NumeroSalida.Value > 0)
                FactorClimaticoViewModel.NumeroParte = NumeroSalida.Value;
            else
                FactorClimaticoViewModel.NumeroParte = await SalidaService.ObtenerUltimoNumeroParteDelAnioAsync(FactorClimaticoViewModel.AnioNumeroParte) + 1;
        }
        //Finish Failed
        private void OnFinishFailed(EditContext editContext)
        {
            message.Error("Error al cargar, posible información ausente.");
             Console.WriteLine($"Failed:{System.Text.Json.JsonSerializer.Serialize(FactorClimaticoViewModel)}");
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

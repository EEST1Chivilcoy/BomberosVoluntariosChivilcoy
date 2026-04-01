using Microsoft.AspNetCore.Components;
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
using Vista.Data.Enums.Salidas;
using System.Threading.Tasks;
using Vista.Data.ViewModels.Personal;
using Vista.Data.Enums;
using Vista.Data.ViewModels;

namespace Vista.Pages.Salidas
{
    public partial class Rescates
    {
        // ViewModel para la carga de Rescates.
        private SalidasViewModels RescateViewModel = new RescatePersonaViewModels();

        // Listas de datos
        private List<Bombero> BomberosTodos = new();
        private List<BomberoViweModel> BomberosVM = new();

        // Lista con todos los vehiculos de la flota del sistema.
        private List<VehiculoSalida> MovilesTodos = new();

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
                var rescate = RescateViewModel.ToSalida() as Rescate
                    ?? throw new InvalidOperationException("Error al convertir el ViewModel a la entidad de rescate.");

                Salida salidaGuardada;

                if (RescateViewModel.SalidaId > 0)
                {
                    salidaGuardada = await SalidaService.EditarSalida(rescate);
                    await message.SuccessAsync("Salida editada correctamente.");
                    HandleOk1(salidaGuardada.AnioNumeroParte, salidaGuardada.NumeroParte);
                }
                else
                {
                    // Modo creación
                    var numeroParteExistente = await SalidaService.ObtenerSalidaPorNumeroParteAsync<Salida>(
                        RescateViewModel.NumeroParte,
                        m => m.NumeroParte == RescateViewModel.NumeroParte && m.AnioNumeroParte == RescateViewModel.AnioNumeroParte
                    );

                    if (numeroParteExistente != null)
                    {
                        await message.WarningAsync($"El número de parte {RescateViewModel.NumeroParte}/{RescateViewModel.AnioNumeroParte} ya existe.");
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

            BomberosVM = BomberosTodos.Select(b => new BomberoViweModel
            {
                Id = b.PersonaId,
                Nombre = b.Nombre,
                Apellido = b.Apellido,
                NumeroLegajo = b.NumeroLegajo,
            }).ToList();

            if ((RescateTipo)TipoRescate == RescateTipo.Animal)
            {
                RescateViewModel = new RescateAnimaViewModels();
            }
            else
            {
                RescateViewModel = new RescatePersonaViewModels();
            }


            // Si se pasan parámetros, puede ser modo Visualización/Edición
            if (NumeroSalida.HasValue && AnioSalida.HasValue && NumeroSalida.Value > 0 && AnioSalida.Value > 0)
            {
                var salidaAEditar = await SalidaService.ObtenerSalidaParaEditarAsync<Salida>(NumeroSalida.Value, AnioSalida.Value);

                if (salidaAEditar != null)
                {
                    var todasLasFuerzas = await FuerzaIntervinienteService.ObtenerTodasLasFuerzasAsync();
                    var fuerzasVM = todasLasFuerzas.Select(f => new Vista.Data.ViewModels.Personal.SimpleFuerzaViewModel { Id = f.Id, Nombre = f.NombreFuerza }).ToList();

                    RescateViewModel = salidaAEditar.ToViewModel(fuerzasVM);
                }
                else
                {
                    await message.WarningAsync("No se encontró la salida solicitada. Se abrirá el formulario en modo creación.");
                    if (AnioSalida.HasValue && AnioSalida.Value > 0) RescateViewModel.AnioNumeroParte = AnioSalida.Value;
                    if (NumeroSalida.HasValue && NumeroSalida.Value > 0) RescateViewModel.NumeroParte = NumeroSalida.Value;
                }

                return;
            }

            // Modo crear (se inicializa arriba, aquí se setean los valores por defecto)
            if (AnioSalida.HasValue && AnioSalida.Value > 0)
                RescateViewModel.AnioNumeroParte = AnioSalida.Value;
            else
                RescateViewModel.AnioNumeroParte = DateTime.Now.Year;

            if (NumeroSalida.HasValue && NumeroSalida.Value > 0)
                RescateViewModel.NumeroParte = NumeroSalida.Value;
            else
                RescateViewModel.NumeroParte = await SalidaService.ObtenerUltimoNumeroParteDelAnioAsync(RescateViewModel.AnioNumeroParte) + 1;
        }
        //Finish Failed
        private void OnFinishFailed(EditContext editContext)
        {
            message.Error("Error al cargar, posible información ausente");
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(RescateViewModel)}");
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

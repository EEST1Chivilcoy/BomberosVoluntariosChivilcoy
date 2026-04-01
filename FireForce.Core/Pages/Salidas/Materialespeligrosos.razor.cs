using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Services;
using AntDesign;
using Vista.Data.Models.Vehiculos.Flota;
using Vista.Data.Mappers;
using Vista.Data.Enums.Salidas;
using System.Threading.Tasks;
using Vista.Data.ViewModels.Personal;
using Vista.Data.Enums;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.ViewModels;
using Vista.Data.ViewModels.MaterialesPeligrosos;

namespace Vista.Pages.Salidas
{
    public partial class MaterialesPeligrosos
    {
        // ViewModel para la carga de Materiales Peligrosos.
        private MaterialPeligrosoViewModels MaterialPeligrosoViewModel = new MaterialPeligrosoViewModels();

        private List<Bombero> BomberosTodos = new();
        private List<BomberoViweModel> BomberosVM = new();
        private List<VehiculoSalida> MovilesTodos = new();

        // Parámetros de la URL
        [Parameter]
        [SupplyParameterFromQuery]
        public int? NumeroSalida { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public int? AnioSalida { get; set; }

        [Parameter]
        public int TipoMaterialPeligroso { get; set; }

        // Función para guardar/editar la salida
        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                var matpel = MaterialPeligrosoViewModel.ToSalida() as MaterialPeligroso
                    ?? throw new InvalidOperationException("Error al convertir el ViewModel a la entidad de Materiales Peligrosos.");

                Salida salidaGuardada;

                if (MaterialPeligrosoViewModel.SalidaId > 0)
                {
                    salidaGuardada = await SalidaService.EditarSalida(matpel);
                    await message.SuccessAsync("Salida editada correctamente.");
                    HandleOk1(salidaGuardada.AnioNumeroParte, salidaGuardada.NumeroParte);
                }
                else // Modo Creación
                {
                    var numeroParteExistente = await SalidaService.ObtenerSalidaPorNumeroParteAsync<MaterialPeligroso>(
                        MaterialPeligrosoViewModel.NumeroParte,
                        m => m.NumeroParte == MaterialPeligrosoViewModel.NumeroParte && m.AnioNumeroParte == MaterialPeligrosoViewModel.AnioNumeroParte
                    );

                    if (numeroParteExistente != null)
                    {
                        await message.WarningAsync($"El número de parte {MaterialPeligrosoViewModel.NumeroParte}/{MaterialPeligrosoViewModel.AnioNumeroParte} ya existe.");
                        return;
                    }

                    salidaGuardada = await SalidaService.GuardarSalida(matpel);
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

        // Inicialización del componente
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

            // Modo Visualización/Edición
            if (NumeroSalida.HasValue && AnioSalida.HasValue && NumeroSalida.Value > 0 && AnioSalida.Value > 0)
            {
                var salidaAEditar = await SalidaService.ObtenerSalidaParaEditarAsync<Salida>(NumeroSalida.Value, AnioSalida.Value);

                if (salidaAEditar != null)
                {
                    var todasLasFuerzas = await FuerzaIntervinienteService.ObtenerTodasLasFuerzasAsync();
                    var fuerzasVM = todasLasFuerzas.Select(f => new SimpleFuerzaViewModel { Id = f.Id, Nombre = f.NombreFuerza }).ToList();
                    MaterialPeligrosoViewModel = (MaterialPeligrosoViewModels)salidaAEditar.ToViewModel(fuerzasVM);
                }
                else
                {
                    await message.WarningAsync("No se encontró la salida solicitada. Se abrirá el formulario en modo creación.");
                    MaterialPeligrosoViewModel = new MaterialPeligrosoViewModels();
                }
            }
            else
            {
                MaterialPeligrosoViewModel = new MaterialPeligrosoViewModels();
            }

            MaterialPeligrosoViewModel.TipoEmergencia = (TipoDeEmergencia)TipoMaterialPeligroso;
            MaterialPeligrosoViewModel.Tipo = (CategoriaMaterialPeligroso)TipoMaterialPeligroso;
            MaterialPeligrosoViewModel.AnioNumeroParte = AnioSalida.HasValue && AnioSalida.Value > 0 ? AnioSalida.Value : DateTime.Now.Year;
            MaterialPeligrosoViewModel.NumeroParte = NumeroSalida.HasValue && NumeroSalida.Value > 0 ? NumeroSalida.Value : await SalidaService.ObtenerUltimoNumeroParteDelAnioAsync(MaterialPeligrosoViewModel.AnioNumeroParte) + 1;

        }

        private void OnFinishFailed(EditContext editContext)
        {
            message.Error("Error al cargar, posible información ausente.");
            Console.WriteLine($"Failed:{System.Text.Json.JsonSerializer.Serialize(MaterialPeligrosoViewModel)}");
        }

        // Lógica para el modal de impresión
        private bool _visible1;
        public int ImprimirAnio;
        public int ImprimirNumeroParte;
        private void HandleOk1(int anio, int numeroParte)
        {
            ImprimirAnio = anio;
            ImprimirNumeroParte = numeroParte;
            _visible1 = true;
        }
    }
}
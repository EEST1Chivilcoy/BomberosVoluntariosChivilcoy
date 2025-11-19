using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Services;
using AntDesign;
using Vista.Data.Models.Vehiculos.Flota;
using Vista.Data.Mappers;
using Vista.Data.Enums.Salidas;
using Vista.Data.Enums;
using Vista.Data.ViewModels;
using Vista.Data.ViewModels.Servicios;
using Vista.Data.Models.Salidas.Planillas.Servicios;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.ViewModels.Personal;

namespace Vista.Pages.Salidas
{
    public partial class ServicioEspecial
    {
        // ViewModel para la carga de Servicios Especiales.
        private SalidasViewModels ServicioEspecialViewModel { get; set; } = new ServicioEspecialViewModel();

        // Listas de datos
        private List<Bombero> BomberosTodos = new();
        private List<BomberoViweModel> BomberosVM = new();

        // Lista con todos los vehiculos de la flota del sistema.
        private List<VehiculoSalida> MovilesTodos = new();

        [Parameter]
        public int TipoServicioEspecial { get; set; }

        [Parameter]
        public int? AnioSalida { get; set; }

        [Parameter]
        public int? NumeroSalida { get; set; }

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

            if (NumeroSalida.HasValue && AnioSalida.HasValue && NumeroSalida.Value > 0 && AnioSalida.Value > 0)
            {
                var salidaAEditar = await SalidaService.ObtenerSalidaParaEditarAsync<Vista.Data.Models.Salidas.Planillas.Servicios.ServicioEspecial>(NumeroSalida.Value, AnioSalida.Value);

                if (salidaAEditar != null)
                {
                    var todasLasFuerzas = await FuerzaIntervinienteService.ObtenerTodasLasFuerzasAsync();
                    var fuerzasVM = todasLasFuerzas.Select(f => new Vista.Data.ViewModels.Personal.SimpleFuerzaViewModel { Id = f.Id, Nombre = f.NombreFuerza }).ToList();

                    ServicioEspecialViewModel = salidaAEditar.ToViewModel(fuerzasVM);
                }
                else
                {
                    await message.WarningAsync("No se encontró la salida solicitada. Se abrirá el formulario en modo creación.");
                    ServicioEspecialViewModel = CrearViewModelPorTipo((ServicioEspecialTipo)TipoServicioEspecial);
                    if (AnioSalida.HasValue) ServicioEspecialViewModel.AnioNumeroParte = AnioSalida.Value;
                    if (NumeroSalida.HasValue) ServicioEspecialViewModel.NumeroParte = NumeroSalida.Value;
                }
                return;
            }

            // Modo Creación
            ServicioEspecialViewModel = CrearViewModelPorTipo((ServicioEspecialTipo)TipoServicioEspecial);

            if (AnioSalida.HasValue && AnioSalida.Value > 0)
                ServicioEspecialViewModel.AnioNumeroParte = AnioSalida.Value;
            else
                ServicioEspecialViewModel.AnioNumeroParte = DateTime.Now.Year;

            if (NumeroSalida.HasValue && NumeroSalida.Value > 0)
                ServicioEspecialViewModel.NumeroParte = NumeroSalida.Value;
            else
                ServicioEspecialViewModel.NumeroParte = await SalidaService.ObtenerUltimoNumeroParteDelAnioAsync(ServicioEspecialViewModel.AnioNumeroParte) + 1;
        }

        private SalidasViewModels CrearViewModelPorTipo(ServicioEspecialTipo tipo)
        {
            return tipo switch
            {
                ServicioEspecialTipo.Representacion => new ServicioEspecialRepresentacionViewModels { Tipo = tipo, TipoEmergencia = TipoDeEmergencia.ServicioEspecialRepresentacion },
                ServicioEspecialTipo.Prevencion => new ServicioEspecialPrevencionViewModels { Tipo = tipo, TipoEmergencia = TipoDeEmergencia.ServicioEspecialPrevencion },
                ServicioEspecialTipo.Capacitacion => new ServicioEspecialCapacitacionViewModel { Tipo = tipo, TipoEmergencia = TipoDeEmergencia.ServicioEspecialCapacitacion },
                ServicioEspecialTipo.ColocacionDriza => new ServicioEspecialColocaciónDrizaViewModels { Tipo = tipo, TipoEmergencia = TipoDeEmergencia.ServicioEspecialColocacionDriza },
                ServicioEspecialTipo.SuministroAgua => new ServicioEspecialSuministroAguaViewModels { Tipo = tipo, TipoEmergencia = TipoDeEmergencia.ServicioEspecialSuministroAgua },
                ServicioEspecialTipo.FalsaAlarma => new ServicioEspecialFalsaAlarmaViewModel { Tipo = tipo, TipoEmergencia = TipoDeEmergencia.ServicioEspecialFalsaAlarma },
                ServicioEspecialTipo.RetiradoDeObito => new ServicioEspecialRetiradoDeObitoViewModel { Tipo = tipo, TipoEmergencia = TipoDeEmergencia.ServicioEspecialRetiradoDeObito },
                ServicioEspecialTipo.ColaboracionFuerzasSeguridad => new ServicioEspecialColaboraciónFuerzasSeguridadViewModels { Tipo = tipo, TipoEmergencia = TipoDeEmergencia.ServicioEspecialColaboracionFuerzasSeguridad },
                _ => throw new ArgumentOutOfRangeException(nameof(tipo), $"Tipo de servicio especial no soportado: {tipo}")
            };
        }

        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                var servicioEspecial = ServicioEspecialViewModel.ToSalida()
                    ?? throw new InvalidOperationException("Error al convertir el ViewModel a la entidad de Servicio Especial.");

                Salida salidaGuardada;

                if (ServicioEspecialViewModel.SalidaId > 0)
                {
                    salidaGuardada = await SalidaService.EditarSalida(servicioEspecial);
                    await message.SuccessAsync("Salida editada correctamente.");
                }
                else
                {
                    var numeroParteExistente = await SalidaService.ObtenerSalidaPorNumeroParteAsync<Salida>(
                        ServicioEspecialViewModel.NumeroParte,
                        m => m.NumeroParte == ServicioEspecialViewModel.NumeroParte && m.AnioNumeroParte == ServicioEspecialViewModel.AnioNumeroParte
                    );

                    if (numeroParteExistente != null)
                    {
                        await message.WarningAsync($"El número de parte {ServicioEspecialViewModel.NumeroParte}/{ServicioEspecialViewModel.AnioNumeroParte} ya existe.");
                        return;
                    }

                    salidaGuardada = await SalidaService.GuardarSalida(servicioEspecial);
                    await message.SuccessAsync("Salida guardada correctamente.");
                }

                if (salidaGuardada == null)
                    throw new Exception("No se pudo guardar o editar la salida.");

                HandleOk1(salidaGuardada.AnioNumeroParte, salidaGuardada.NumeroParte);
                await Init();
                StateHasChanged();
            }
            catch (Exception e)
            {
                await message.ErrorAsync(e.InnerException?.Message ?? e.Message, 5);
            }
        }

        private void OnFinishFailed(EditContext editContext)
        {
            message.Error("Error al cargar, posible información ausente.");
            Console.WriteLine($"Failed:{System.Text.Json.JsonSerializer.Serialize(ServicioEspecialViewModel)}");
        }

        // Impresión
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
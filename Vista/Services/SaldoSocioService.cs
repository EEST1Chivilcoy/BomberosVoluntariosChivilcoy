using Vista.Data.Enums.Socios;
using Vista.Data.Models.Socios.Componentes;

namespace Vista.Services
{
    public interface ISaldoSocioService
    {
        /// <summary>
        /// Calcula el saldo actual del socio (cuotas adeudadas - pagos realizados).
        /// Un saldo positivo indica deuda, un saldo negativo indica saldo a favor.
        /// </summary>
        /// <param name="socioId">Identificador único del socio.</param>
        /// <returns>El saldo actual del socio.</returns>
        Task<decimal> CalcularSaldoActualAsync(int socioId);

        /// <summary>
        /// Calcula el total de cuotas que el socio debe desde su ingreso al sistema.
        /// Solo considera los períodos donde el socio estuvo activo.
        /// </summary>
        /// <param name="socioId">Identificador único del socio.</param>
        /// <returns>El total de cuotas adeudadas.</returns>
        Task<decimal> CalcularTotalCuotasAdeudadasAsync(int socioId);

        /// <summary>
        /// Obtiene un resumen detallado del saldo del socio.
        /// </summary>
        /// <param name="socioId">Identificador único del socio.</param>
        /// <returns>Objeto con el detalle del saldo.</returns>
        Task<ResumenSaldoSocio> ObtenerResumenSaldoAsync(int socioId);
    }

    /// <summary>
    /// Representa un resumen detallado del saldo de un socio.
    /// </summary>
    public class ResumenSaldoSocio
    {
        /// <summary>
        /// Total de cuotas que el socio debería haber pagado.
        /// </summary>
        public decimal TotalCuotasAdeudadas { get; set; }

        /// <summary>
        /// Total de pagos confirmados realizados por el socio.
        /// </summary>
        public decimal TotalPagosRealizados { get; set; }

        /// <summary>
        /// Saldo actual (TotalCuotasAdeudadas - TotalPagosRealizados).
        /// Positivo = deuda, Negativo = saldo a favor.
        /// </summary>
        public decimal SaldoActual { get; set; }

        /// <summary>
        /// Cantidad de cuotas pendientes de pago.
        /// </summary>
        public int CuotasPendientes { get; set; }

        /// <summary>
        /// Indica si el socio tiene deuda.
        /// </summary>
        public bool TieneDeuda => SaldoActual > 0;
    }

    public class SaldoSocioService : ISaldoSocioService
    {
        private readonly IHistorialSocioService _historialSocioService;
        private readonly IPagoService _pagoService;

        public SaldoSocioService(
            IHistorialSocioService historialSocioService,
            IPagoService pagoService)
        {
            _historialSocioService = historialSocioService;
            _pagoService = pagoService;
        }

        public async Task<decimal> CalcularSaldoActualAsync(int socioId)
        {
            var totalCuotas = await CalcularTotalCuotasAdeudadasAsync(socioId);
            var totalPagos = await _pagoService.ObtenerTotalPagosConfirmadosAsync(socioId);

            Console.WriteLine($"DEBUG: Cuotas={totalCuotas}, Pagos={totalPagos}");

            return totalPagos - totalCuotas;
        }

        public async Task<decimal> CalcularTotalCuotasAdeudadasAsync(int socioId)
        {
            var periodosActivos = await _historialSocioService.ObtenerPeriodosActivos(socioId);
            if (!periodosActivos.Any()) return 0;

            var historialCuotas = await _historialSocioService.ObtenerHistorialCuotas(socioId);
            if (!historialCuotas.Any()) return 0;

            decimal totalCuotas = 0;

            // Obtener la zona horaria de Argentina
            TimeZoneInfo zonaArgentina = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");

            // Convertir la hora UTC actual a la hora de Argentina
            DateTime hoy = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaArgentina);

            foreach (var periodo in periodosActivos)
            {
                var inicioP = periodo.FechaDesde;
                var finP = periodo.FechaHasta ?? hoy;

                // Filtrar cuotas que caen dentro de este periodo de actividad
                var cuotasAplicables = historialCuotas.Where(c =>
                    c.FechaDesde <= finP &&
                    (c.FechaHasta == null || c.FechaHasta >= inicioP));

                foreach (var cuota in cuotasAplicables)
                {
                    // Determinar el rango real de deuda
                    DateTime inicioDeuda = inicioP > cuota.FechaDesde ? inicioP : cuota.FechaDesde;
                    DateTime finDeuda = (cuota.FechaHasta == null || cuota.FechaHasta > finP) ? finP : cuota.FechaHasta.Value;

                    if (inicioDeuda < finDeuda)
                    {
                        var cantidad = CalcularCantidadCuotas(inicioDeuda, finDeuda, cuota.FrecuenciaDePago);
                        totalCuotas += cantidad * (decimal)cuota.Monto;
                    }
                }
            }

            return totalCuotas;
        }

        public async Task<ResumenSaldoSocio> ObtenerResumenSaldoAsync(int socioId)
        {
            var totalCuotas = await CalcularTotalCuotasAdeudadasAsync(socioId);
            var totalPagos = await _pagoService.ObtenerTotalPagosConfirmadosAsync(socioId);
            var saldoActual = totalCuotas - totalPagos;

            // Calcular cuotas pendientes basándose en la cuota actual vigente
            var cuotaActual = await _historialSocioService.ObtenerCuotaVigenteEnFecha(socioId, DateTime.UtcNow);
            var cuotasPendientes = 0;

            if (cuotaActual != null && cuotaActual.Monto > 0)
            {
                cuotasPendientes = (int)Math.Ceiling((double)(saldoActual / (decimal)cuotaActual.Monto));
                if (cuotasPendientes < 0) cuotasPendientes = 0;
            }

            return new ResumenSaldoSocio
            {
                TotalCuotasAdeudadas = totalCuotas,
                TotalPagosRealizados = totalPagos,
                SaldoActual = saldoActual,
                CuotasPendientes = cuotasPendientes
            };
        }

        /// <summary>
        /// Calcula la cantidad de cuotas entre dos fechas según la frecuencia de pago.
        /// </summary>
        private int CalcularCantidadCuotas(DateTime fechaInicio, DateTime fechaFin, FrecuenciaPago frecuencia)
        {
            if (fechaFin < fechaInicio)
            {
                return 0;
            }

            // Calcular la diferencia en meses
            int mesesDiferencia = ((fechaFin.Year - fechaInicio.Year) * 12) + fechaFin.Month - fechaInicio.Month;

            // Si estamos dentro del mismo mes, contar al menos 1 cuota
            if (mesesDiferencia == 0)
            {
                mesesDiferencia = 1;
            }

            // Calcular cantidad de cuotas según frecuencia
            return frecuencia switch
            {
                FrecuenciaPago.Mensual => mesesDiferencia,
                FrecuenciaPago.Trimestral => (int)Math.Ceiling(mesesDiferencia / 3.0),
                FrecuenciaPago.Semestral => (int)Math.Ceiling(mesesDiferencia / 6.0),
                FrecuenciaPago.Anual => (int)Math.Ceiling(mesesDiferencia / 12.0),
                _ => mesesDiferencia
            };
        }
    }
}

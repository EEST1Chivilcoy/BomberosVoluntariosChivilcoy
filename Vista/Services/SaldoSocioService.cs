namespace Vista.Services
{
    public interface ISaldoSocioService
    {
        decimal CalcularSaldoActual(int socioId);
    }

    public class SaldoSocioService : ISaldoSocioService
    {
        public decimal CalcularSaldoActual(int socioId)
        {
            // Lógica para calcular el saldo actual del socio
            decimal saldo = 0.0m;

            // Aquí se deberia poder acceder a la base de datos para obtener los pagos y cuotas del socio
            // y calcular el saldo en función de esos datos.
            return saldo;

        }
    }
}

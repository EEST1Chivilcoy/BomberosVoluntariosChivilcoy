using Microsoft.EntityFrameworkCore;
using Vista.Data;
using Vista.Data.Enums.Discriminadores;
using Vista.Data.Enums.Socios;
using Vista.Data.Models.Socios;
using Vista.Data.Models.Socios.Componentes.Pagos;

namespace Vista.Services
{
    public interface IPagoPendienteService
    {
        /// <summary>
        /// Genera los pagos pendientes para todos los socios activos.
        /// Solo puede ejecutarse una vez cada 24 horas, a menos que haya cambios en la cantidad de socios.
        /// </summary>
        Task GenerarPagosPendientes();
    }

    public class PagoPendienteService : IPagoPendienteService
    {
        private readonly BomberosDbContext _context;

        public PagoPendienteService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task GenerarPagosPendientes()
        {
            try
            {
                // --- 1. Verificar control de ejecución ---

                // A Implementar: lógica para controlar la ejecución del servicio de generación de pagos pendientes.
                // Esto porque no debe ejecutarse más de una vez cada 24 horas, seria un exceso innecesario de procesamiento.


                // --- 2. Iniciar la transacción ---
                await using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // --- 3. Obtener socios activos con pago en efectivo ---

                    var sociosActivos = await _context.Socios
                        .Where(s => s.EstadoSocio == TipoEstadoSocio.Activo)
                        .Where(s => s.FormaPago == FormaDePago.Efectivo)
                        .Include(s => s.Pagos)
                        .ToListAsync();

                    var pagosGenerados = 0;
                    var fechaActual = DateTime.UtcNow;

                    foreach (var socio in sociosActivos)
                    {
                        // Calcular las fechas de pago que deberían existir desde FechaIngresoSistemaNuevo
                        var fechasPago = CalcularFechasDePago(socio, fechaActual);

                        foreach (var fechaCobro in fechasPago)
                        {
                            // Verificar si ya existe un pago para esa fecha
                            var pagoExistente = socio.Pagos
                                .Any(p => p.FechaCobro.Year == fechaCobro.Year &&
                                         p.FechaCobro.Month == fechaCobro.Month);

                            if (!pagoExistente)
                            {
                                // Crear el pago en efectivo pendiente
                                var nuevoPago = new PagoEfectivo
                                {
                                    Tipo = TipoPagoSocio.Efectivo,
                                    FechaGeneradoPendiente = fechaActual,
                                    FechaCobro = fechaCobro,
                                    Monto = socio.MontoCuota,
                                    SocioId = socio.Id,
                                    Estado = EstadoPago.PendienteAPagar
                                };

                                _context.PagosEfectivo.Add(nuevoPago);
                                pagosGenerados++;
                            }
                        }
                    }

                    // --- 4. Actualizar o crear el control de ejecución ---

                    // --- 5. Guardar cambios ---
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _context.ChangeTracker.Clear();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar pagos pendientes: {ex.Message}");
            }
        }

        /// <summary>
        /// Genera la lista de fechas de pago correspondientes a un socio,
        /// comenzando desde su fecha de ingreso al sistema hasta la fecha actual.
        /// </summary>
        /// <param name="socio">El socio para el cual se calcularán las fechas de pago.</param>
        /// <param name="fechaActual">La fecha límite hasta la cual se calcularán los pagos.</param>
        /// <returns>
        /// Una lista de objetos <see cref="DateTime"/> que representan las fechas de pago
        /// que deberían existir para el socio en el período indicado.
        /// </returns>
        private List<DateTime> CalcularFechasDePago(Socio socio, DateTime fechaActual)
        {
            var fechas = new List<DateTime>();
            var fechaInicio = socio.FechaIngresoSistemaNuevo;

            // Determinar el intervalo según la frecuencia de pago
            int mesesIntervalo = socio.FrecuenciaDePago switch
            {
                FrecuenciaPago.Mensual => 1,
                FrecuenciaPago.Trimestral => 3,
                FrecuenciaPago.Semestral => 6,
                FrecuenciaPago.Anual => 12,
                _ => 1 // Default mensual
            };

            // Comenzar desde el primer día del mes de ingreso
            var fechaCobro = new DateTime(fechaInicio.Year, fechaInicio.Month, 1);

            // Generar fechas hasta el mes actual
            while (fechaCobro <= fechaActual)
            {
                // Solo agregar fechas desde la fecha de ingreso en adelante
                if (fechaCobro >= new DateTime(fechaInicio.Year, fechaInicio.Month, 1))
                {
                    fechas.Add(fechaCobro);
                }

                fechaCobro = fechaCobro.AddMonths(mesesIntervalo);
            }

            return fechas;
        }
    }
}
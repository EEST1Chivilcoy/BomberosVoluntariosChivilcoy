using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Salidas.Planillas;
using Vista.Data.Models.Vehiculos.Flota;
using Vista.Data.ViewModels.Personal;
using Vista.Data.Models.Personas;
using Vista.Data.Models.Grupos.FuerzasIntervinientes;

namespace Vista.Services
{
    public interface ISalidaService
    {
        Task<Salida> GuardarSalida<T>(T entidad) where T : Salida;

        Task<T?> ObtenerSalidaPorNumeroParteAsync<T>(int numeroParte,
    Expression<Func<T, bool>> predicate) where T : class;

        Task<List<Salida>> ObtenerTodasLasSalidasAsync();

        Task<List<Salida>> ObtenerSalidasPorAnioAsync(int anio);

        Task<Salida?> ObtenerSalidaPorIdAsync(int id);

        Task<bool> BorrarSalidaAsync(int id);

        Task<int> ObtenerUltimoNumeroParteDelAnioAsync(int anio);
    }   

    public class SalidaService : ISalidaService
    {
        private readonly BomberosDbContext _context;

        public SalidaService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task<T?> ObtenerSalidaPorNumeroParteAsync<T>(int numeroParte,
        Expression<Func<T, bool>> predicate) where T : class
        {
            // Consulta genérica a la base de datos utilizando el predicado de búsqueda
            return await _context.Set<T>()
                .Where(predicate)
                .SingleOrDefaultAsync();
        }

        public async Task<Salida> GuardarSalida<T>(T salida) where T : Salida
        {
            try
            {
                Bombero? bomberoEncargado = await _context.Bomberos.Where(b => b.NumeroLegajo == salida.Encargado.NumeroLegajo).SingleOrDefaultAsync();
                Bombero? BomberoLlenoPlanilla = await _context.Bomberos.Where(b => b.NumeroLegajo == salida.QuienLleno.NumeroLegajo).SingleOrDefaultAsync();
                salida.Encargado = bomberoEncargado;
                salida.QuienLleno = BomberoLlenoPlanilla;

                //Cuerpo participante
                List<BomberoSalida> bomberossalida = new List<BomberoSalida>();
                foreach (BomberoSalida bom in salida.CuerpoParticipante)
                {
                    Bombero? bomberoSalida = await _context.Bomberos.Where(b => b.NumeroLegajo == bom.Bombero.NumeroLegajo).SingleOrDefaultAsync();
                    Movil? movilSalio = null;

                    if (bom.MovilAsignado != null)
                    {
                        movilSalio = await _context.Moviles
                            .Where(m => m.NumeroMovil == bom.MovilAsignado.NumeroMovil)
                            .SingleOrDefaultAsync();
                    }

                    BomberoSalida BomSalida = new()
                    {
                        MovilAsignado = movilSalio,
                        Bombero = bomberoSalida
                    };
                    bomberossalida.Add(BomSalida);
                }
                salida.CuerpoParticipante = bomberossalida;

                //Moviles
                List<Movil_Salida> movilessalida = new List<Movil_Salida>();
                foreach (Movil_Salida m in salida.Moviles)
                {
                    Bombero? bomberoChofer = await _context.Bomberos.SingleOrDefaultAsync(b => b.NumeroLegajo == m.Chofer.NumeroLegajo);
                    Movil? Movilsalida = await _context.Moviles.SingleOrDefaultAsync(mob => mob.NumeroMovil == m.Movil.NumeroMovil);

                    if (bomberoChofer == null || Movilsalida == null) break;
                    Movilsalida.Kilometraje = m.KmLlegada;
                    Movil_Salida movilS = new()
                    {
                        CargoCombustible = m.CargoCombustible,
                        NumeroFactura = m.NumeroFactura,
                        FechaFactura = m.FechaFactura,
                        TipoConbustible = m.TipoConbustible,
                        CantidadLitros = m.CantidadLitros,
                        QuienLleno = m.QuienLleno,
                        TelefonoQuienLleno = m.TelefonoQuienLleno,
                        KmLlegada = m.KmLlegada,
                        Chofer = bomberoChofer,
                        Movil = Movilsalida
                    };
                    movilessalida.Add(movilS);
                }
                salida.Moviles = movilessalida;

                //FuerzasIntervinientes

                List<FuerzaInterviniente_Salida> fuerzasintervinientessalida = new List<FuerzaInterviniente_Salida>();

                if (salida.FuerzasIntervinientes != null)
                {
                    foreach (FuerzaInterviniente_Salida f in salida.FuerzasIntervinientes)
                    {
                        // Buscamos la FuerzaInterviniente -- (Pasar esto luego a tomarlo del Servicio de FuerzasIntervinientes)
                        var fuerzaCompleta = await _context.Fuerzas.FindAsync(f.FuerzaIntervinienteId);

                        // Nos aseguramos de que la fuerza exista

                        if (fuerzaCompleta == null)
                        {
                            throw new Exception($"La fuerza interviniente con ID {f.FuerzaIntervinienteId} no existe.");
                        }

                        // Creamos la nueva entidad de relación para la salida.
                        FuerzaInterviniente_Salida fuerzaS = new()
                        {
                            EncargadoApellidoyNombre = f.EncargadoApellidoyNombre,
                            NumeroUnidad = f.NumeroUnidad,
                            SalidaId = salida.SalidaId,
                            FuerzaIntervinienteId = f.FuerzaIntervinienteId,
                            Fuerzainterviniente = fuerzaCompleta
                        };

                        fuerzasintervinientessalida.Add(fuerzaS);
                    }
                }

                salida.FuerzasIntervinientes = fuerzasintervinientessalida;

                //Damnificados

                List<Damnificado_Salida> damnificadossalida = new List<Damnificado_Salida>();
                foreach (Damnificado_Salida d in salida.Damnificados)
                {
                    Damnificado_Salida damn = new()
                    {
                        Nombre = d.Nombre,
                        Apellido = d.Apellido,
                        Documento = d.Documento,
                        Sexo = d.Sexo,
                        LugarNacimiento = d.LugarNacimiento,
                        Edad = d.Edad,
                        Estado = d.Estado,
                    };
                    damnificadossalida.Add(damn);
                }
                salida.Damnificados = damnificadossalida;

                _context.Set<T>().Add(salida);
                await _context.SaveChangesAsync();
                return salida;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERROR: Error al cargar la salida. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Ocurrió un error inesperado. {ex.Message}");
                throw;
            }
        }

        public async Task<List<Salida>> ObtenerTodasLasSalidasAsync()
        {
            return await _context.Salidas.ToListAsync();
        }

        public async Task<List<Salida>> ObtenerSalidasPorAnioAsync(int anio)
        {
            return await _context.Salidas
                .Where(s => s.AnioNumeroParte == anio)
                .ToListAsync();
        }

        public async Task<Salida?> ObtenerSalidaPorIdAsync(int id)
        {
            return await _context.Salidas.FindAsync(id);
        }

        public async Task<bool> BorrarSalidaAsync(int id)
        {
            var salida = await _context.Salidas.FindAsync(id);
            if (salida == null)
            {
                return false;
            }

            _context.Salidas.Remove(salida);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> ObtenerUltimoNumeroParteDelAnioAsync(int anio)
        {
            return await _context.Salidas
                .Where(p => p.AnioNumeroParte == anio)
                .MaxAsync(p => (int?)p.NumeroParte) ?? 1;
        }
    }
}


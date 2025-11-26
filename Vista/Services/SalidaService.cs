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

        Task<Salida> EditarSalida<T>(T entidad) where T : Salida;

        Task<T?> ObtenerSalidaParaEditarAsync<T>(int numeroParte, int anio) where T : Salida;

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
            return await _context.Set<T>()
                .Where(predicate)
                .SingleOrDefaultAsync();
        }

        public async Task<Salida> GuardarSalida<T>(T salida) where T : Salida
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await PreProcesarEntidadSalidaAsync(salida);

                if (salida.AnioNumeroParte <= 0)
                {
                    salida.AnioNumeroParte = DateTime.Now.Year;
                }

                if (salida.NumeroParte <= 0)
                {

                    var ultimo = await ObtenerUltimoNumeroParteDelAnioAsync(salida.AnioNumeroParte);
                    salida.NumeroParte = ultimo + 1;
                }
                foreach (var mv in (salida?.Moviles ?? new List<Movil_Salida>()))
                {
                    Console.WriteLine($"DEBUG Movil_Salida: Movil_SalidaId={mv.Movil_SalidaId}, MovilId={mv.MovilId}, Movil?.VehiculoId={mv.Movil?.VehiculoId}");
                    if (mv.MovilId > 0)
                    {
                        var exists = await _context.Moviles.AsNoTracking().AnyAsync(v => v.VehiculoId == mv.MovilId);
                        if (!exists) throw new KeyNotFoundException($"Vehículo con ID {mv.MovilId} no existe en la BD (antes de Save).");
                    }
                }
                _context.Set<T>().Add(salida);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return salida;
            }
            catch (Exception ex)
            {
                // --- Manejo de Error ---
                // Si CUALQUIER operación falla (el primer SaveChanges,
                // la lógica del service, o el segundo SaveChanges dentro del service),
                // revertimos TODA la operación.
                await transaction.RollbackAsync();

                // Limpiar el contexto para evitar conflictos futuros
                _context.ChangeTracker.Clear();

                // Lanza una excepción genérica o la 'ex' original
                // para que la capa superior sepa que algo falló.
                if (ex is DbUpdateException)
                {
                    throw new Exception("Error al guardar en la base de datos. Verifique datos duplicados.", ex);
                }

                // Re-lanza la excepción (ej. la ValidationException del service)
                throw;
            }
        }

        public async Task<T?> ObtenerSalidaParaEditarAsync<T>(int numeroParte, int anio) where T : Salida
        {
            return await _context.Set<T>()
                .Include(s => s.Encargado)
                .Include(s => s.QuienLleno)
                //.Include(s => s.Damnificados).ThenInclude(d => d.FuerzaInterviniente)
                .Include(s => s.Moviles).ThenInclude(m => m.Chofer)
                .Include(s => s.Moviles).ThenInclude(m => m.Movil)
                .Include(s => s.CuerpoParticipante).ThenInclude(cp => cp.Bombero)
                .Include(s => s.CuerpoParticipante).ThenInclude(cp => cp.MovilAsignado)
                .Include(s => s.FuerzasIntervinientes).ThenInclude(fi => fi.Fuerzainterviniente)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.NumeroParte == numeroParte && s.AnioNumeroParte == anio);
        }

        public async Task<Salida> EditarSalida<T>(T entidad) where T : Salida
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var salidaExistente = await _context.Set<T>()
                 .Include(s => s.Encargado)
                 .Include(s => s.QuienLleno)
                 //.Include(s => s.Damnificados).ThenInclude(d => d.FuerzaInterviniente)
                 .Include(s => s.Moviles).ThenInclude(m => m.Chofer)
                 .Include(s => s.Moviles).ThenInclude(m => m.Movil)
                 .Include(s => s.CuerpoParticipante).ThenInclude(cp => cp.Bombero)
                 .Include(s => s.CuerpoParticipante).ThenInclude(cp => cp.MovilAsignado)
                 .Include(s => s.FuerzasIntervinientes).ThenInclude(fi => fi.Fuerzainterviniente)
                 .AsTracking()
                 .FirstOrDefaultAsync(s => s.SalidaId == entidad.SalidaId);

                if (salidaExistente == null) throw new KeyNotFoundException($"Salida con ID {entidad.SalidaId} no encontrada para editar.");

                entidad.NumeroParte = salidaExistente.NumeroParte;
                entidad.AnioNumeroParte = salidaExistente.AnioNumeroParte;
                entidad.TipoEmergencia = salidaExistente.TipoEmergencia;

                await PreProcesarEntidadSalidaAsync(entidad);

                // 1) Actualizar escalares de la Salida
                _context.Entry(salidaExistente).CurrentValues.SetValues(entidad);

                // 2) Sincronizar colecciones (añadir/actualizar/borrar)
                SyncCollection(salidaExistente.CuerpoParticipante, entidad.CuerpoParticipante ?? new List<BomberoSalida>(), bs => bs.BomberoSalidaId, (dbItem, newItem) =>
                {
                    dbItem.PersonaId = newItem.PersonaId;
                    dbItem.Grado = newItem.Grado;
                    dbItem.MovilId = newItem.MovilId;
                });

                SyncCollection(salidaExistente.Moviles, entidad.Moviles ?? new List<Movil_Salida>(), m => m.Movil_SalidaId, (dbItem, newItem) =>
                {
                    dbItem.PersonaId = newItem.PersonaId;
                    dbItem.MovilId = newItem.MovilId;
                    dbItem.KmLlegada = newItem.KmLlegada;
                });

                await SyncFuerzasIntervinientesAsync(salidaExistente.FuerzasIntervinientes, entidad.FuerzasIntervinientes ?? new List<FuerzaInterviniente_Salida>());

                // Damnificados sincronización comentada por ahora:
                // SyncCollection(salidaExistente.Damnificados, entidad.Damnificados ?? new List<Damnificado_Salida>(), d => d.Damnificado_SalidaId, (dbItem, newItem) =>
                // {
                //     _context.Entry(dbItem).CurrentValues.SetValues(newItem);
                //     dbItem.FuerzaInterviniente = null;
                // });

                // Guardar cambios y finalizar
                foreach (var mv in (entidad?.Moviles ?? salidaExistente?.Moviles ?? new List<Movil_Salida>()))
                {
                    Console.WriteLine($"DEBUG Movil_Salida: Movil_SalidaId={mv.Movil_SalidaId}, MovilId={mv.MovilId}, Movil?.VehiculoId={mv.Movil?.VehiculoId}");
                    if (mv.MovilId > 0)
                    {
                        var exists = await _context.Moviles.AsNoTracking().AnyAsync(v => v.VehiculoId == mv.MovilId);
                        if (!exists) throw new KeyNotFoundException($"Vehículo con ID {mv.MovilId} no existe en la BD (antes de Save).");
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return salidaExistente;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"ERROR: {ex.Message}");
                throw;
            }
        }

        private async Task PreProcesarEntidadSalidaAsync(Salida salida)
        {
            var bomberosCache = new Dictionary<int, Bombero>();

            async Task<Bombero> ObtenerBomberoPorLegajoAsync(int legajo)
            {
                if (legajo <= 0) throw new ArgumentException("Legajo inválido.");
                if (bomberosCache.TryGetValue(legajo, out var cached)) return cached;

                var b = await _context.Bomberos.FirstOrDefaultAsync(x => x.NumeroLegajo == legajo);
                if (b == null) throw new KeyNotFoundException($"Bombero con legajo {legajo} no encontrado.");

                if (_context.Entry(b).State == EntityState.Detached)
                {
                    _context.Attach(b);
                }
                bomberosCache[legajo] = b;
                return b;
            }

            // 1) Encargado / QuienLleno 
            if (salida.Encargado != null && salida.Encargado.NumeroLegajo > 0)
            {
                var encargado = await ObtenerBomberoPorLegajoAsync(salida.Encargado.NumeroLegajo);
                salida.EncargadoId = encargado.PersonaId;
                salida.Encargado = null;
            }

            if (salida.QuienLleno != null && salida.QuienLleno.NumeroLegajo > 0)
            {
                var quien = await ObtenerBomberoPorLegajoAsync(salida.QuienLleno.NumeroLegajo);
                salida.QuienLlenoId = quien.PersonaId;
                salida.QuienLleno = null;
            }

            // 2) CuerpoParticipante
            if (salida.CuerpoParticipante != null)
            {
                foreach (var bs in salida.CuerpoParticipante)
                {
                    if (bs.Bombero != null && bs.Bombero.NumeroLegajo > 0)
                    {
                        var bom = await ObtenerBomberoPorLegajoAsync(bs.Bombero.NumeroLegajo);
                        bs.PersonaId = bom.PersonaId;
                        bs.Bombero = null;
                    }

                    if (bs.MovilAsignado != null && bs.MovilAsignado.VehiculoId > 0)
                    {
                        var movilDb = await _context.Moviles.AsNoTracking().FirstOrDefaultAsync(m => m.VehiculoId == bs.MovilAsignado.VehiculoId);
                        if (movilDb == null) throw new KeyNotFoundException($"Móvil con ID {bs.MovilAsignado.VehiculoId} no encontrado.");
                        bs.MovilId = movilDb.VehiculoId;
                        bs.MovilAsignado = null;
                    }
                }
            }

            // 3) Moviles de la salida
            if (salida.Moviles != null)
            {
                foreach (var mv in salida.Moviles)
                {
                    var choferLeg = mv.Chofer?.NumeroLegajo ?? 0;
                    var vehIdInput = mv.Movil?.VehiculoId ?? 0;

                    if (mv.Chofer != null && mv.Chofer.NumeroLegajo > 0)
                    {
                        var chofer = await ObtenerBomberoPorLegajoAsync(mv.Chofer.NumeroLegajo);
                        mv.PersonaId = chofer.PersonaId;
                    }

                    if (mv.MovilId <= 0 && vehIdInput > 0)
                    {
                        var movilDb = await _context.Moviles.FindAsync(vehIdInput);
                        if (movilDb == null) throw new KeyNotFoundException($"Móvil con ID {vehIdInput} no encontrado.");
                        mv.MovilId = movilDb.VehiculoId;
                    }

                    mv.Chofer = null;
                    mv.Movil = null;

                    if (mv.MovilId <= 0)
                    {
                        throw new ArgumentException($"Móvil inválido en la lista. Se recibió un MovilId=0 para Movil_SalidaId={mv.Movil_SalidaId}. (Chofer Legajo: {choferLeg})");
                    }
                }
            }

            // 4) FuerzasIntervinientes en la salida
            if (salida.FuerzasIntervinientes != null)
            {
                foreach (var fi in salida.FuerzasIntervinientes)
                {
                    if (fi.FuerzaIntervinienteId > 0)
                    {
                        var existe = await _context.Fuerzas.FindAsync(fi.FuerzaIntervinienteId);
                        if (existe == null) throw new KeyNotFoundException($"Fuerza interviniente con ID {fi.FuerzaIntervinienteId} no encontrada.");
                        fi.Fuerzainterviniente = null;
                    }
                    else if (fi.Fuerzainterviniente != null)
                    {
                        if (fi.Fuerzainterviniente.Id > 0)
                        {
                            _context.Attach(fi.Fuerzainterviniente);
                            fi.FuerzaIntervinienteId = fi.Fuerzainterviniente.Id;
                            fi.Fuerzainterviniente = null;
                        }
                    }
                }
            }
        }

        private async Task SyncFuerzasIntervinientesAsync(ICollection<FuerzaInterviniente_Salida> dbCollection, ICollection<FuerzaInterviniente_Salida> newCollection)
        {
            var toDelete = dbCollection.Where(db => !newCollection.Any(n => n.Id == db.Id && n.Id != 0)).ToList();
            foreach (var item in toDelete)
            {
                _context.Remove(item);
            }

            foreach (var newItem in newCollection)
            {
                FuerzaInterviniente_Salida? dbItem = null;
                if (newItem.Id != 0)
                {
                    dbItem = dbCollection.FirstOrDefault(db => db.Id == newItem.Id);
                }

                if (dbItem != null) // Actualizar
                {
                    dbItem.EncargadoApellidoyNombre = newItem.EncargadoApellidoyNombre;
                    dbItem.NumeroUnidad = newItem.NumeroUnidad;
                    dbItem.FuerzaIntervinienteId = newItem.FuerzaIntervinienteId;
                }
                else // Agregar
                {

                    if (newItem.Fuerzainterviniente != null && newItem.Fuerzainterviniente.Id == 0)
                    {
                        _context.Fuerzas.Add(newItem.Fuerzainterviniente);
                    }

                    else if (newItem.Fuerzainterviniente != null)
                    {
                        var existingFuerza = _context.Fuerzas.Local.FirstOrDefault(f => f.Id == newItem.Fuerzainterviniente.Id);
                        if (existingFuerza == null)
                        {
                            _context.Attach(newItem.Fuerzainterviniente);
                        }
                    }


                    if (newItem.FuerzaIntervinienteId > 0) newItem.Fuerzainterviniente = null;

                    dbCollection.Add(newItem);
                }
            }
        }

        private void SyncCollection<TEntity, TKey>(ICollection<TEntity> dbCollection, ICollection<TEntity> newCollection, Func<TEntity, TKey> keySelector, Action<TEntity, TEntity> updateAction) where TEntity : class where TKey : IEquatable<TKey>
        {
            var dbKeys = dbCollection.Select(keySelector).ToHashSet();
            var newKeys = newCollection.Select(keySelector).ToHashSet();


            var toDelete = dbCollection.Where(item => !newKeys.Contains(keySelector(item))).ToList();
            foreach (var item in toDelete) _context.Remove(item);

            // Añadir y actualizar
            foreach (var newItem in newCollection)
            {
                var key = keySelector(newItem);
                if (dbKeys.Contains(key))
                {
                    // Actualizar
                    var dbItem = dbCollection.First(i => keySelector(i).Equals(key));
                    updateAction(dbItem, newItem);
                }
                else
                {
                    // Añadir
                    dbCollection.Add(newItem);
                }
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
                .MaxAsync(p => (int?)p.NumeroParte) ?? 0;
        }
    }
}

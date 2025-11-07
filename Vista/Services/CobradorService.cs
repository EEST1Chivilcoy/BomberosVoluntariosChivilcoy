using AntDesign;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Enums.Personal.ComisionDirectiva;
using Vista.Data.Models.Grupos.Brigadas;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Data.ViewModels.Personal;
using Vista.Helpers;

namespace Vista.Services
{
    public interface ICobradorService
    {
        Task CrearCobradorAsync(Cobrador cobrador, Imagen? imagen = null);
        Task<List<Cobrador>> ObtenerTodosLosCobradoresAsync(bool ConImagenes = false);
        Task<Cobrador> ObtenerCobradorPorIdAsync(int id, bool asnotracking = false, bool conRelaciones = true);
    }

    public class CobradorService : ICobradorService
    {
        private readonly BomberosDbContext _context;
        private readonly IImagenService _imagenService;

        public CobradorService(BomberosDbContext context, IImagenService imagenService)
        {
            _context = context;
            _imagenService = imagenService;
        }

        public async Task CrearCobradorAsync(Cobrador cobrador, Imagen? imagen = null)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (cobrador == null)
            {
                throw new ArgumentNullException(nameof(cobrador), "El cobrador no puede ser nulo.");
            }

            ValidationHelper.Validar(cobrador);

            // --- Paso B: Validaciones "Caras" (contra la BD) ---
            // (Se hacen antes de iniciar la transacción para no abrirla innecesariamente)

            if (await _context.Cobradores.AnyAsync(b => b.Documento == cobrador.Documento))
            {
                throw new InvalidOperationException("Número de documento ya existente.");
            }

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso A: Guardar el Cobrador ---
                _context.Cobradores.Add(cobrador);

                // Guardamos para que la BD genere el 'cobrador.PersonaId'
                await _context.SaveChangesAsync();

                // --- Paso B: Guardar la Imagen (usando el Service) ---
                if (imagen != null)
                {
                    if (imagen is Imagen_Personal imagenPersonal)
                    {
                        // Asignamos el ID del cobrador recién creado
                        imagenPersonal.PersonalId = cobrador.PersonaId;

                        // Llamamos al servicio.
                        // Si GuardarImagenAsync falla, lanzará una excepción
                        // que será capturada por nuestro bloque 'catch' más abajo.
                        await _imagenService.GuardarImagenAsync(imagenPersonal);
                    }
                    else
                    {
                        // Esta excepción también será capturada y provocará un Rollback.
                        throw new InvalidOperationException("La imagen proporcionada no es del tipo correcto para un bombero.");
                    }
                }

                // --- Paso C: Confirmar la Transacción ---
                // Si llegamos aquí, tanto el SaveChanges del Bombero como
                // el SaveChanges (dentro del service) de la Imagen fueron exitosos.
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // --- Manejo de Error ---
                // Si CUALQUIER operación falla (el primer SaveChanges,
                // la lógica del service, o el segundo SaveChanges dentro del service),
                // revertimos TODA la operación.
                await transaction.RollbackAsync();

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

        public async Task<List<Cobrador>> ObtenerTodosLosCobradoresAsync(bool ConImagenes = false)
        {
            IQueryable<Cobrador> query = _context.Cobradores;

            if (ConImagenes)
            {
                query = query.Include(c => c.Imagen);
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Cobrador> ObtenerCobradorPorIdAsync(int id, bool asnotracking = false, bool conRelaciones = true)
        {
            IQueryable<Cobrador> query = _context.Cobradores;

            if (conRelaciones)
            {
                query = query
                    .Include(c => c.Imagen)
                    .Include(c => c.Contacto);
            }

            if (asnotracking)
            {
                query = query.AsNoTracking();
            }

            var cobrador = await query.FirstOrDefaultAsync(c => c.PersonaId == id);

            if (cobrador == null)
            {
                throw new KeyNotFoundException("No se encontró un cobrador con el ID proporcionado.");
            }

            return cobrador;
        }
    }
}

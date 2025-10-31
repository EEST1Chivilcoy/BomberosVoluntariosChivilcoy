using AntDesign;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.Brigadas;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Data.ViewModels.Personal;
using Vista.Data.Enums.Personal.ComisionDirectiva;

namespace Vista.Services
{
    public interface IComisionDirectivaService
    {
        Task CrearComisionDirectivaAsync(ComisionDirectiva comisionDirectiva, Imagen? imagen = null);
        Task<List<ComisionDirectiva>> ObtenerTodosLosMiembrosDeComisionDirectivaAsync(bool ConImagenes = false);
        Task<ComisionDirectiva> ObtenerMiembroComisionDirectivaPorIdAsync(int id, bool asnotracking = false, bool conRelaciones = false);
        Task<bool> CambiarEstado(int id, EstadoComisionDirectiva estado);
    }

    public class ComisionDirectivaService : IComisionDirectivaService
    {
        private readonly BomberosDbContext _context;
        private readonly IImagenService _imagenService;

        public ComisionDirectivaService(BomberosDbContext context, IImagenService imagenService)
        {
            _context = context;
            _imagenService = imagenService;
        }

        public async Task CrearComisionDirectivaAsync(ComisionDirectiva comisionDirectiva, Imagen? imagen = null)
        {
            // --- 1. Validaciones ---

            // --- Paso A: Validaciones "Baratas" (en memoria) ---
            if (comisionDirectiva == null)
            {
                throw new ArgumentNullException(nameof(comisionDirectiva), "El Comisión Directiva no puede ser nulo.");
            }

            var validationContext = new ValidationContext(comisionDirectiva, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool esValido = Validator.TryValidateObject(comisionDirectiva, validationContext, validationResults, validateAllProperties: true);

            if (!esValido)
            {
                string errores = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"El modelo Comisión Directiva no es válido: {Environment.NewLine}{errores}");
            }

            // --- Paso B: Validaciones "Caras" (contra la BD) ---
            // (Se hacen antes de iniciar la transacción para no abrirla innecesariamente)

            if (await _context.ComisionDirectivas.AnyAsync(b => b.Documento == comisionDirectiva.Documento))
            {
                throw new InvalidOperationException("Número de documento ya existente.");
            }

            // --- 2. Inicio de la Transacción ---
            // Esta será la transacción "principal" que controlará todo.
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // --- Paso A: Guardar el Comisión Directiva ---
                _context.ComisionDirectivas.Add(comisionDirectiva);

                // Guardamos para que la BD genere el 'comisionDirectiva.PersonaId'
                await _context.SaveChangesAsync();

                // --- Paso B: Guardar la Imagen (usando el Service) ---
                if (imagen != null)
                {
                    if (imagen is Imagen_Personal imagenPersonal)
                    {
                        // Asignamos el ID del comisionDirectiva recién creado
                        imagenPersonal.PersonalId = comisionDirectiva.PersonaId;

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

        public async Task<List<ComisionDirectiva>> ObtenerTodosLosMiembrosDeComisionDirectivaAsync(bool ConImagenes = false)
        {
            IQueryable<ComisionDirectiva> query = _context.ComisionDirectivas;

            if (ConImagenes)
            {
                query = query.Include(c => c.Imagen);
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> CambiarEstado(int id, EstadoComisionDirectiva estado)
        {
            var miembro = await _context.ComisionDirectivas.FindAsync(id);

            if (miembro == null)
            {
                throw new KeyNotFoundException($"No se encontró un miembro de comisión directiva con el ID {id}.");
            }

            miembro.Estado = estado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ComisionDirectiva> ObtenerMiembroComisionDirectivaPorIdAsync(int id, bool asnotracking = false, bool conRelaciones = false)
        {
            IQueryable<ComisionDirectiva> query = _context.ComisionDirectivas;

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

            var miembro = await query.FirstOrDefaultAsync(c => c.PersonaId == id);

            if (miembro is null)
            {
                throw new KeyNotFoundException($"No se encontró un miembro de comisión directiva con el ID {id}.");
            }

            return miembro;
        }
    }
}
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
        Task CrearComisionDirectiva(ComisionDirectiva comisionDirectiva, Imagen? imagen = null);
        Task<List<ComisionDirectiva>> ObtenerTodosLosMiembrosDeComisionDirectivaAsync(bool ConImagenes = false);
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

        public async Task CrearComisionDirectiva(ComisionDirectiva comisionDirectiva, Imagen? imagen = null)
        {
            // Asumiendo que Id es la clave primaria
            if (await _context.ComisionDirectivas.AnyAsync(c => c.PersonaId == comisionDirectiva.PersonaId))
            {
                throw new InvalidOperationException("Ya existe una Persona con este ID.");
            }

            _context.ComisionDirectivas.Add(comisionDirectiva);

            await _context.SaveChangesAsync(); // Guardar el miembro de comisión directiva primero para obtener su PersonaId

            // Si hay una imagen, asociarla al bombero
            if (imagen != null)
            {
                if (imagen is Imagen_Personal imagenPersonal)
                {
                    imagenPersonal.PersonalId = comisionDirectiva.PersonaId;
                    await _imagenService.GuardarImagenAsync(imagenPersonal);
                }
                else
                {
                    throw new InvalidOperationException("La imagen proporcionada no es del tipo correcto para un personal.");
                }
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
    }
}
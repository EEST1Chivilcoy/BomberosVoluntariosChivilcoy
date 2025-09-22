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
    public interface ICobradorService
    {
        Task CrearCobrador(Cobrador cobrador, Imagen? imagen = null);
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

        public async Task CrearCobrador(Cobrador cobrador, Imagen? imagen = null)
        {
            // Asumiendo que Id es la clave primaria
            if (await _context.Cobradores.AnyAsync(c => c.PersonaId == cobrador.PersonaId))
            {
                throw new InvalidOperationException("Ya existe una Persona con este ID.");
            }

            _context.Cobradores.Add(cobrador);

            await _context.SaveChangesAsync(); // Guardar el miembro de comisión directiva primero para obtener su PersonaId

            // Si hay una imagen, asociarla al bombero
            if (imagen != null)
            {
                if (imagen is Imagen_Personal imagenPersonal)
                {
                    imagenPersonal.PersonalId = cobrador.PersonaId;
                    await _imagenService.GuardarImagenAsync(imagenPersonal);
                }
                else
                {
                    throw new InvalidOperationException("La imagen proporcionada no es del tipo correcto para un personal.");
                }
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

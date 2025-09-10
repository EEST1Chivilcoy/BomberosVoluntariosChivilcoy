using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Grupos.Brigadas;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Data.Models.Imagenes;
using Vista.Data.ViewModels.Personal;

namespace Vista.Services
{
    public interface IBomberoService
    {
        Task CrearBombero(Bombero bombero, Imagen? imagen = null);
        Task<bool> BorrarBombero(Bombero bombero);
        Task<bool> EditarBombero(Bombero bombero);
        Task<Sancion> SancionarBombero(Sancion sancion);
        Task<bool> CambiarEstado(int id, EstadoBombero estado);
        Task<AscensoBombero> AscenderBombero(AscensoBombero ascenso);
        Task<List<Bombero>> ObtenerTodosLosBomberosAsync(bool ConImagenes = false, bool ConTodasLasDemasRelaciones = false);
        Task<Bombero> ObtenerBomberoPorIdAsync(int id, bool asnotracking = false, bool conRelaciones = true);
        Task<Bombero> ObtenerBomberoObjetoPorLegajoAsync(int numeroLegajo);
    }

    public class BomberoService : IBomberoService
    {
        private readonly BomberosDbContext _context;
        private readonly IImagenService _imagenService;

        public BomberoService(BomberosDbContext context, IImagenService imagenService)
        {
            _context = context;
            _imagenService = imagenService;
        }

        public async Task<Bombero> ObtenerBomberoPorIdAsync(int id, bool asNoTracking = false, bool conRelaciones = true)
        {
            IQueryable<Bombero> query = _context.Bomberos;

            if (conRelaciones)
            {
                query = query
                    .Include(b => b.Imagen)
                    .Include(b => b.Firmas)
                    .Include(b => b.Brigadas)
                    .Include(b => b.VehiculosEncargado)
                    .Include(b => b.Dependencias)
                    .Include(b => b.Incidentes)
                    .Include(b => b.Salidas)
                    .Include(b => b.Handie)
                    .Include(b => b.Ascensos)
                    .Include(b => b.DestinoMaterial)
                    .Include(b => b.SancionesRecibidas)
                    .Include(b => b.Limpieza)
                    .Include(b => b.Novedades)
                    .Include(b => b.Licencias);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var bombero = await query.FirstOrDefaultAsync(b => b.PersonaId == id);

            if (bombero is null)
            {
                throw new KeyNotFoundException($"No se encontró un bombero con el ID {id}.");
            }

            return bombero;
        }

        public async Task CrearBombero(Bombero bombero, Imagen? imagen = null)
        {
            // Asumiendo que Id es la clave primaria
            if (await _context.Bomberos.AnyAsync(b => b.PersonaId == bombero.PersonaId))
            {
                throw new InvalidOperationException("Ya existe un bombero con este ID.");
            }

            bool legajoExistente = await _context.Bomberos.AnyAsync(b => b.NumeroLegajo == bombero.NumeroLegajo);

            if (legajoExistente)
            {
                throw new Exception("Número de legajo ya existente.");
            }

            _context.Bomberos.Add(bombero);

            await _context.SaveChangesAsync(); // Guardar el bombero primero para obtener su PersonaId

            // Si hay una imagen, asociarla al bombero
            if (imagen != null)
            {
                if (imagen is Imagen_Personal imagenPersonal)
                {
                    imagenPersonal.PersonalId = bombero.PersonaId;
                    await _imagenService.GuardarImagenAsync(imagenPersonal);
                }
                else
                {
                    throw new InvalidOperationException("La imagen proporcionada no es del tipo correcto para un bombero.");
                }
            }
        }

        public async Task<bool> EditarBombero(Bombero bombero)
        {
            try
            {
                Bombero? Editar = await _context.Bomberos.SingleOrDefaultAsync(e => e.PersonaId == bombero.PersonaId);
                Contacto? contacto = await _context.Contactos.SingleOrDefaultAsync(c => c.PersonalId == bombero.PersonaId);

                if (Editar == null)
                {
                    throw new Exception("No se encontró el bombero para editar.");
                }

                if (contacto != null)
                {
                    _context.Contactos.Remove(contacto);
                }

                Editar = bombero;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al editar el bombero {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CambiarEstado(int id, EstadoBombero estado)
        {
            Bombero? BomberoE = await _context.Bomberos.SingleOrDefaultAsync(b => b.PersonaId == id);
            
            if (BomberoE == null)
            {
                return false;
            }

            BomberoE.Estado = estado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BorrarBombero(Bombero bombero)
        {
            if (bombero == null)
            {
                Console.WriteLine("ERROR: Bombero no puede ser nulo.");
                return false;
            }

            try
            {
                _context.Bomberos.Remove(bombero);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                // Manejo de excepción específico para errores de la base de datos
                Console.WriteLine($"ERROR: Error al eliminar el bombero. {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Manejo de excepción genérico
                Console.WriteLine($"ERROR: Ocurrió un error inesperado. {ex.Message}");
                return false;
            }
        }

        public async Task<Sancion> SancionarBombero(Sancion sancion)
        {
            Bombero? BomberoSancionado = await _context.Bomberos.Where(b => b.NumeroLegajo == sancion.PersonalSancionado.NumeroLegajo).SingleOrDefaultAsync();

            if (BomberoSancionado == null)
            {
                throw new InvalidOperationException("No se pudieron encontrar los bomberos.");
            }

            sancion.PersonalSancionado = BomberoSancionado;

            _context.Sanciones.Add(sancion);
            await _context.SaveChangesAsync();
            return sancion;
        }

        public async Task<AscensoBombero> AscenderBombero(AscensoBombero ascenso)
        {
            Bombero? BomberoAfectado = await _context.Bomberos.Where(b => b.NumeroLegajo == ascenso.PersonalAfectado.NumeroLegajo).SingleOrDefaultAsync();

            if (BomberoAfectado == null)
            {
                throw new InvalidOperationException("No se pudo encontrar el BomberoAfectado.");
            }
            BomberoAfectado.Grado = ascenso.GradoAscenso;
            ascenso.PersonalAfectado = BomberoAfectado;
            _context.AscensoBomberos.Add(ascenso);
            await _context.SaveChangesAsync();
            return ascenso;
        }

        public async Task<List<Bombero>> ObtenerTodosLosBomberosAsync(
            bool ConImagenes = false,
            bool ConTodasLasDemasRelaciones = false)
        {
            IQueryable<Bombero> query = _context.Bomberos.AsQueryable();

            // Si no se pide nada, traemos solo los bomberos pelados
            if (!ConImagenes && !ConTodasLasDemasRelaciones)
            {
                return await query
                    .AsNoTracking()
                    .ToListAsync();
            }

            // Carga condicional de relaciones
            if (ConImagenes)
            {
                query = query.Include(b => b.Imagen);
            }

            if (ConTodasLasDemasRelaciones)
            {
                query = query
                    .Include(b => b.Firmas)
                    .Include(b => b.Brigadas)
                    .Include(b => b.VehiculosEncargado)
                    .Include(b => b.Dependencias)
                    .Include(b => b.Incidentes)
                    .Include(b => b.Salidas)
                    .Include(b => b.Handie)
                    .Include(b => b.Ascensos)
                    .Include(b => b.DestinoMaterial)
                    .Include(b => b.SancionesRecibidas)
                    .Include(b => b.Limpieza)
                    .Include(b => b.Novedades)
                    .Include(b => b.Licencias);
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<Bombero> ObtenerBomberoObjetoPorLegajoAsync(int numeroLegajo)
        {
            var bombero = await _context.Bomberos
                .FirstOrDefaultAsync(b => b.NumeroLegajo == numeroLegajo);

            return bombero;
        }
    }
}

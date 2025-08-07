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
using Vista.Data.ViewModels.Personal;

namespace Vista.Services
{
    public interface IBomberoService
    {
        Task CrearBombero(Bombero bombero);
        Task<bool> BorrarBombero(Bombero bombero);
        Task<Bombero> EditarBombero(Bombero bombero);
        Task<Sancion> SancionarBombero(Sancion sancion);
        Task<Bombero> CambiarEstado(Bombero bombero);
        Task<AscensoBombero> AscenderBombero(AscensoBombero ascenso);
        Task<List<Bombero>> ObtenerTodosLosBomberosAsync();
        Task<Bombero> ObtenerBomberoObjetoPorLegajoAsync(int numeroLegajo);
    }

    public class BomberoService : IBomberoService
    {
        private readonly BomberosDbContext _context;

        public BomberoService(BomberosDbContext context)
        {
            _context = context;
        }

        public async Task CrearBombero(Bombero bombero)
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
            await _context.SaveChangesAsync();
        }

        public async Task<Bombero> EditarBombero(Bombero bombero)
        {
            try
            {
                Bombero Editar = await _context.Bomberos.SingleOrDefaultAsync(e => e.PersonaId == bombero.PersonaId);
                Contacto? contacto = await _context.Contactos.SingleOrDefaultAsync(c => c.PersonalId == bombero.PersonaId);
                if (contacto != null)
                {
                    _context.Contactos.Remove(contacto);
                }
                foreach (var i in bombero.GetType().GetProperties())
                {
                    var propNombre = i.Name;
                    var propValor = i.GetValue(bombero);
                    var editarProp = Editar.GetType().GetProperty(propNombre);
                    if (editarProp != null && editarProp.CanWrite && propValor != null && propNombre != "Brigada" && propNombre != "BrigadaId")
                    {
                        editarProp.SetValue(Editar, propValor);
                    }
                }

                await _context.SaveChangesAsync();
                return bombero;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al editar el bombero {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado {ex.Message}");
                return null;
            }
        }

        public async Task<Bombero> CambiarEstado(Bombero bombero)
        {
            Bombero BomberoBaja = await _context.Bomberos.SingleOrDefaultAsync(b => b.PersonaId == bombero.PersonaId);
            BomberoBaja.Estado = bombero.Estado;
            await _context.SaveChangesAsync();
            return bombero;
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

        public async Task<List<Bombero>> ObtenerTodosLosBomberosAsync()
        {
            return await _context.Bomberos.ToListAsync();
        }

        public async Task<Bombero> ObtenerBomberoObjetoPorLegajoAsync(int numeroLegajo)
        {
            var bombero = await _context.Bomberos
                .FirstOrDefaultAsync(b => b.NumeroLegajo == numeroLegajo);

            return bombero;
        }
    }
}

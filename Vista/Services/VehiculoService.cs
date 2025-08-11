using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Imagenes;
using Vista.Data.Models.Otros;
using Vista.Data.Models.Personas.Personal;
using Vista.Data.Models.Vehiculos.Flota;

namespace Vista.Services
{
    public interface IVehiculoService
    {
        Task<VehiculoSalida> AgregarVehiculo(VehiculoSalida vehiculo);
        Task<VehiculoSalida> EditarVehiculo(VehiculoSalida vehiculo);
        Task<Incidente> AgregarIncidente(Incidente incidente);
        Task<Incidente> BorrarIncidente(Incidente incidente);
        Task<Movil> ObtenerMovilPorNumero(string NumeroMovil);
    }

    public class VehiculoService : IVehiculoService
    {
        private readonly BomberosDbContext _context;

        public VehiculoService(BomberosDbContext context)
        {
            _context = context;
        }
        public async Task<VehiculoSalida> AgregarVehiculo(VehiculoSalida vehiculo)
        {
            if (vehiculo.Encargado != null)
            {
                Bombero? Encargado = await _context.Bomberos.SingleOrDefaultAsync(b => b.PersonaId == vehiculo.Encargado.PersonaId);
                vehiculo.Encargado = Encargado;
                if (Encargado.VehiculosEncargado == null) Encargado.VehiculosEncargado = new();
                Encargado.VehiculosEncargado.Add(vehiculo);
            }
            if (vehiculo is Movil)
            {
                _context.Moviles.Add((Movil)vehiculo);
            }
            else
            {
                _context.Embarcacion.Add((Embarcacion)vehiculo);
            }
            await _context.SaveChangesAsync();
            return vehiculo;
        }
        public async Task<VehiculoSalida> EditarVehiculo(VehiculoSalida vehiculo)
        {
            try
            { //PENDIENTE: Terminar de pulir
                VehiculoSalida Editar = await _context.Set<VehiculoSalida>().SingleOrDefaultAsync(e => e.VehiculoId == vehiculo.VehiculoId);
                if( (Editar is Embarcacion && vehiculo is Movil) || (Editar is Movil && vehiculo is Embarcacion) )
                {
                    int? ImagenId = 0;
                    if (vehiculo.Imagen == null && Editar.Imagen != null)
                    {
                        ImagenId = Editar.ImagenId;
                    }
                    _context.Set<VehiculoSalida>().Remove(Editar);
                    await _context.SaveChangesAsync();
                    if (vehiculo.Encargado != null)
                    {
                        Bombero? Encargado = await _context.Bomberos.SingleOrDefaultAsync(b => b.PersonaId == vehiculo.Encargado.PersonaId);
                        vehiculo.Encargado = Encargado;
                        if (Encargado.VehiculosEncargado == null) Encargado.VehiculosEncargado = new();
                        Encargado.VehiculosEncargado.Add(vehiculo);
                    }
                    if(ImagenId != null && ImagenId != 0)
                    {
                        Imagen_VehiculoSalida EditarImagen = await _context.ImagenesVehiculo.SingleOrDefaultAsync(i=>i.ImagenId == ImagenId);
                        vehiculo.Imagen = EditarImagen;
                        vehiculo.ImagenId = EditarImagen.ImagenId;
                    }
                    if (vehiculo is Movil)
                    {
                        _context.Moviles.Add((Movil)vehiculo);
                    }
                    else
                    {
                        _context.Embarcacion.Add((Embarcacion)vehiculo);
                    }
                    await _context.SaveChangesAsync();
                    return vehiculo;
                }
                else
                {
                    foreach (var i in vehiculo.GetType().GetProperties())
                    {
                        var propNombre = i.Name;
                        var propValor = i.GetValue(vehiculo);
                        var editarProp = Editar.GetType().GetProperty(propNombre);
                        //Validaciones.
                        //Encargado, EncargadoId se establecen a parte para evitar errores
                        if (editarProp != null && editarProp.CanWrite && propValor != null && propNombre != "Encargado" && propNombre != "SeguroId" && propNombre != "EncargadoId")
                        {
                            editarProp.SetValue(Editar, propValor);
                        }
                    }
                    if (vehiculo.Encargado != null)
                    {
                        Bombero? Encargado = await _context.Bomberos.SingleOrDefaultAsync(b => b.PersonaId == vehiculo.Encargado.PersonaId);
                        Editar.Encargado = Encargado;
                        Editar.EncargadoId = Encargado.PersonaId;
                    }

                    await _context.SaveChangesAsync();
                }
                return vehiculo;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al editar el movil {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado {ex.Message}");
                return null;
            }
        }
        public async Task<VehiculoSalida> CambiarEstado(int movilid, TipoEstadoMovil estado)
        {
            VehiculoSalida vehiculo = await _context.Set<VehiculoSalida>().SingleOrDefaultAsync(e => e.VehiculoId == movilid);
            vehiculo.Estado = estado;
            await _context.SaveChangesAsync();
            return vehiculo;
        }
        public async Task<Incidente> AgregarIncidente(Incidente incidente)
        {
            //PENDIENTE: Solucionar carga de datos en las List de las relaciones
            if (incidente.Encargado != null)
            {
                Bombero? Responsable = await _context.Bomberos.SingleOrDefaultAsync(b => b.PersonaId == incidente.Encargado.PersonaId);
                incidente.Encargado = Responsable;
                Responsable.Incidentes.Add(incidente);
            }
            if (incidente.Vehiculo != null)
            {
                VehiculoSalida? vehiculo = await _context.Set<VehiculoSalida>().SingleOrDefaultAsync(m => m.VehiculoId == incidente.Vehiculo.VehiculoId);
                incidente.Vehiculo = vehiculo;
                vehiculo.Incidentes.Add(incidente);
            }
            _context.Set<Incidente>().Add(incidente);
            await _context.SaveChangesAsync();
            return incidente;
        }
        public async Task<Incidente> BorrarIncidente(Incidente incidente)
        {
            try
            {
                if (incidente != null)
                {
                    Incidente IncidenteBorrar = await _context.Set<Incidente>().Where(inci => inci.IncidenteId == incidente.IncidenteId).SingleOrDefaultAsync();
                    _context.Set<Incidente>().Remove(IncidenteBorrar);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"ERROR: Error al eliminar el incidente. {ex.Message}");
                return incidente;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Ocurrió un error inesperado. {ex.Message}");
                return incidente;
            }
            return incidente;
        }

        public async Task<Movil> ObtenerMovilPorNumero(string NumeroMovil)
        {
            var movil = await _context.Moviles
                                      .FirstOrDefaultAsync(m => m.NumeroMovil == NumeroMovil);

            return movil;
        }

        public async Task<List<Movil>> ObtenerTodosLosMoviles()
        {
            var moviles = await _context.Moviles.ToListAsync();
            return moviles;
        }
    }
}
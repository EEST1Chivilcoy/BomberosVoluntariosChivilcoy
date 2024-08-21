﻿using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Vista.Data;
using Vista.Data.Enums;
using Vista.Data.Models.Personales;
using Vista.Data.Models.Salidas.Componentes;

namespace Vista.Services
{
    public interface IDepositoService
    {
        Task<Material> AgregarMaterial(Material material);
        Task<MovimientoMaterial> CargarMovimiento(MovimientoMaterial material);
    }

    public class DepositoService : IDepositoService
    {
        private readonly BomberosDbContext _context;

        public DepositoService(BomberosDbContext context)
        {
            _context = context;
        }
        public async Task<Material> AgregarMaterial(Material material)
        {
            try
            {
                if (await _context.Materiales.AnyAsync(b => b.MaterialId == material.MaterialId))
                {
                    throw new InvalidOperationException("Ya existe un material con este ID.");
                }
                _context.Materiales.Add(material);
                await _context.SaveChangesAsync();
                return material;
            }
            catch (Exception ex)
            {
                // Registrar el error en un log en el futuro
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<MovimientoMaterial> CargarMovimiento(MovimientoMaterial movimiento)
        {
            try
            {
                var BomberoDestino = movimiento.DestinoBombero != null ? await _context.Bomberos.SingleOrDefaultAsync(m => m.NumeroLegajo == movimiento.DestinoBombero.NumeroLegajo) : null;
                var MovilDestino = movimiento.DestinoMovil != null ? await _context.Moviles.SingleOrDefaultAsync(m => m.NumeroMovil == movimiento.DestinoMovil.NumeroMovil) : null;
                var MaterialAsignado = await _context.Materiales.SingleOrDefaultAsync(ma => ma.MaterialId == movimiento.Materiales.MaterialId);
                if (MaterialAsignado == null)
                {
                    throw new Exception("Material no encontrado");
                }
                movimiento.DestinoBombero = BomberoDestino;
                movimiento.DestinoMovil = MovilDestino;
                movimiento.Materiales = MaterialAsignado;

                _context.Movimientos.Add(movimiento);
                ActualizarStock(movimiento, MaterialAsignado);
                await _context.SaveChangesAsync();
                return movimiento;
            }
            catch (Exception ex)
            {
                // Registrar el error en un log en el futuro
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        void ActualizarStock(MovimientoMaterial movimiento, Material material)
        {
            try
            {

                if(movimiento.TipoMovimiento == TipoMovimiento.Entrada)
                {
                    material.Stock = material.Stock + movimiento.Cantidad;
                }
                else if (movimiento.TipoMovimiento == TipoMovimiento.Entrada_Salida)
                {
                    return;
                }
                else
                {
                    material.Stock = material.Stock - movimiento.Cantidad;
                }

                if (material.Stock < 0)
                {
                    throw new Exception("Stock insuficiente");
                }
                
                _context.SaveChanges();
                return;
            }
            catch (Exception ex)
            {
                // Registrar el error en un log en el futuro
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

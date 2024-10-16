﻿using Vista.Data.Models.Personales;
using Vista.Data.Models.Salidas.Componentes;
using Vista.Data.Models.Salidas.Planillas;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Vista.Data.Models.Salidas.Planillas.Incendios;
using Vista.Data.Models.Salidas.Planillas.Servicios;

namespace Vista.Data
{
    public class BomberosDbContext : DbContext
    {
        public DbSet<Bombero> Bomberos { get; set; }
        public DbSet<Movil> Moviles { get; set; }
        public DbSet<VehiculoPersonal> VehiculosPersonales { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Damnificado> Damnificados { get; set; }
        public DbSet<ImagenBombero> ImagenesBomberos { get; set; }
        public DbSet<ImagenVehiculo> ImagenesVehiculo { get; set; }
        public DbSet<SeguroSalida> SegurosSalidas { get; set; }
        public DbSet<SeguroVehiculo> SeguroVehiculos { get; set; }
       
        public DbSet<EmbarcacionAfectada> EmbarcacionesAfectadas { get; set; }
        public DbSet<VehiculoAfectadoAccidente> VehiculosAfectadosAccidentes { get; set; }
        public DbSet<VehiculoAfectadoIncendio> VehiculosAfectadoIncendios { get; set; }
        public DbSet<VehiculoDamnificado> VehiculosDamnificados { get; set; }
        public DbSet<Accidente> Accidentes { get; set; }
        public DbSet<FactorClimatico> FactoresClimaticos { get; set; }
        public DbSet<RescatePersona> RescatePersonas { get; set; }
        public DbSet<RescateAnimal> RescateAnimales { get; set; }
        public DbSet<IncendioComercio> IncendiosComercios { get; set; }
        public DbSet<IncendioEstablecimientoEducativo> IncendiosEstablecimientosEducativos { get; set; }
        public DbSet<IncendioEstablecimientoPublico> IncendiosEstablecimientosPublicos { get; set; }
        public DbSet<IncendioForestal> IncendiosForestales { get; set; }
        public DbSet<IncendioHospitalesYClinicas> IncendiosHospitalesYClinicas { get; set; }
        public DbSet<IncendioIndustria> IncendiosIndustrias { get; set; }
        public DbSet<IncendioVivienda> IncendiosViviendas { get; set; }
        public DbSet<ServicioEspecialRetiradoDeObito> ServicioEspecialRetiradoDeObito { get; set; }
        public DbSet<MaterialPeligroso> MaterialesPeligrosos { get; set; }
        public DbSet<ServicioEspecialRepresentacion> ServicioEspecialesRespresentaciones { get; set; }
        public DbSet<ServicioEspecialPrevencion> ServicioEspecialPrevenciones { get; set; }
        public DbSet<ServicioEspecialCapacitacion> ServicioEspecialCapacitacion { get; set; }
        public DbSet<ServicioEspecialColocaciónDriza> ServicioEspecialColocaciónDriza { get; set; }
        public DbSet<ServicioEspecialSuministroAgua> ServicioEspecialSuministroAgua { get; set; }
        public DbSet<ServicioEspecialFalsaAlarma> ServicioEspecialFalsaAlarma { get; set; }
        public DbSet<ServicioEspecialColaboraciónFuerzasSeguridad> ServicioEspecialColaboraciónFuerzasSeguridad { get; set; }
        public DbSet<Firma> Firmas { get; set; }
        public DbSet<Brigada> Brigadas { get; set; }
        public DbSet<BomberoBrigada> bomberoBrigadas { get; set; }
        public DbSet<MovilSalida> MovilesSalida { get; set; }
        public DbSet<BomberoSalida> BomberosSalida { get; set; }
        public DbSet<Limpieza> Limpiezas { get; set; }
        public DbSet<Material> Materiales { get; set; }

        public DbSet<MovimientoMaterial> Movimientos { get; set; }
        public DbSet<Embarcacion> Embarcacion { get; set; }
        public DbSet<Comunicacion> Comunicacion { get; set; }
        public DbSet<AscensoBombero> AscensoBomberos { get; set; }
        public DbSet<Licencia> Licencias { get; set; }
        public DbSet<HorarioBombero> HorariosBomberos { get; set; }
        public DbSet<Sancion> Sanciones { get; set; }
        public DbSet<Novedad> Novedades { get; set; }
        public DbSet<NovedadVehiculo> NovedadesVehiculos { get; set; }

        //Fuerzas

        public DbSet<Fuerza> Fuerzas { get; set; }
        public DbSet<FuerzaInterviniente> fuerzaIntervinientes { get; set; }

        //propiedad experimental
        public DbSet<Salida> Salidas { get; set; }

        public BomberosDbContext(DbContextOptions<BomberosDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BomberoBrigada>()
            .HasKey(bb => new { bb.BomberoId, bb.BrigadaId }); // Configura la clave primaria compuesta

            modelBuilder.Entity<BomberoBrigada>()
                .HasOne(bb => bb.Bombero)
                .WithMany(b => b.BomberoBrigadas) // Asegúrate de que en Bombero tienes una colección de BomberoBrigada
                .HasForeignKey(bb => bb.BomberoId);

            modelBuilder.Entity<BomberoBrigada>()
                .HasOne(bb => bb.Brigada)
                .WithMany(b => b.BomberoBrigadas) // Asegúrate de que en Brigada tienes una colección de BomberoBrigada
                .HasForeignKey(bb => bb.BrigadaId);

            modelBuilder.Entity<Brigada>()
                .HasIndex(b => b.Nombre)
                .IsUnique();

            modelBuilder.Entity<Movil>()
            .HasIndex(m => m.NumeroMovil)
            .IsUnique();

            modelBuilder.Entity<Embarcacion>()
            .HasIndex(e => e.NumeroMovil)
            .IsUnique();

            modelBuilder.Entity<Bombero>()
                .HasIndex(b => b.NumeroLegajo)
                .IsUnique();

            modelBuilder.Entity<Licencia>()
             .HasKey(l => l.LicenciaId);

            modelBuilder.Entity<NovedadBase>()
             .HasKey(n => n.NovedadId);

            modelBuilder.Entity<Incidente>()
                .ToTable("Incidente");

            modelBuilder.Entity<Dependencia>()
                .ToTable("Dependencia");

            modelBuilder.Entity<BomberoDependencia>()
                .ToTable("BomberoDependencia");

            modelBuilder.Entity<Licencia>()
                .ToTable("Licencias");
            modelBuilder.Entity<Comunicacion>()
                .HasKey(c => c.EquipoId);
            modelBuilder.Entity<Comunicacion>()
                .ToTable("Comunicacion");

            modelBuilder.Entity<AscensoBombero>()
              .HasKey(a => a.AscensoId);
            modelBuilder.Entity<AscensoBombero>()
                .ToTable("AscensoBombero");

            modelBuilder.Entity<HorarioBombero>()
              .HasKey(h => h.HorarioId);
            modelBuilder.Entity<HorarioBombero>()
                .ToTable("HorariosBomberos");

            modelBuilder.Entity<Sancion>()
              .HasKey(a => a.SancionId);
            modelBuilder.Entity<Sancion>()
                .ToTable("Sanciones");


            modelBuilder.Entity<Persona>()
                .HasDiscriminator<int>("TipoPersona")
                .HasValue<Bombero>(1)
                .HasValue<Persona>(2);
            modelBuilder.Entity<Persona>()
                .ToTable("Personas");

            modelBuilder.Entity<Seguro>()
                .HasDiscriminator<int>("TipoSeguro")
                .HasValue<SeguroSalida>(1)
                .HasValue<SeguroVehiculo>(2);
            modelBuilder.Entity<Seguro>()
                .ToTable("Seguros");

            modelBuilder.Entity<Imagen>()
                .HasDiscriminator<int>("TipoImagenDiscriminador")
                .HasValue<ImagenBombero>(1)
                .HasValue<ImagenVehiculo>(2);
            modelBuilder.Entity<Imagen>()
                .ToTable("Imagenes");

            modelBuilder.Entity<Salida>()
                .HasDiscriminator<int>("TipoSalida")
                .HasValue<Accidente>(1)
                .HasValue<FactorClimatico>(2)
                .HasValue<MaterialPeligroso>(3)
                .HasValue<ServicioEspecialRepresentacion>(4)
                .HasValue<RescateAnimal>(5)
                .HasValue<RescatePersona>(6)
                .HasValue<IncendioComercio>(7)
                .HasValue<IncendioEstablecimientoEducativo>(8)
                .HasValue<IncendioEstablecimientoPublico>(9)
                .HasValue<IncendioForestal>(10)
                .HasValue<IncendioHospitalesYClinicas>(11)
                .HasValue<IncendioIndustria>(12)
                .HasValue<IncendioVivienda>(13)
                .HasValue<ServicioEspecialPrevencion>(14)
                .HasValue<Incendio>(15)
                .HasValue<ServicioEspecial>(16)
                .HasValue<IncendioAeronaves>(17)
                .HasValue<ServicioEspecialCapacitacion>(18)
                .HasValue<ServicioEspecialColocaciónDriza>(19)
                .HasValue<ServicioEspecialSuministroAgua>(20)
                .HasValue<ServicioEspecialFalsaAlarma>(21)
                .HasValue<ServicioEspecialRetiradoDeObito>(22)
                .HasValue<ServicioEspecialColaboraciónFuerzasSeguridad>(23);
            modelBuilder.Entity<Salida>()
                .ToTable("Salidas");

            modelBuilder.Entity<Vehiculo>()
                .HasDiscriminator<int>("TipoVehiculo")
                .HasValue<VehiculoAfectadoAccidente>(1)
                .HasValue<VehiculoAfectadoIncendio>(2)
                .HasValue<VehiculoDamnificado>(3)
                .HasValue<VehiculoPersonal>(4)
                .HasValue<Movil>(5)
                .HasValue<VehiculoAfectado>(6)
                .HasValue<Embarcacion>(7);

            modelBuilder.Entity<Comunicacion>()
                .HasOne(c => c.Bombero)
                .WithOne(b => b.Handie)
                .HasForeignKey<Bombero>(ei => ei.EquipoId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<ImagenVehiculo>()
                .HasOne(i => i.Vehiculo)
                .WithOne(v => v.Imagen)
                .HasForeignKey<VehiculoSalida>(v => v.ImagenId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<Brigada>()
                .HasMany(br => br.Bomberos)
                .WithOne(bo => bo.Brigada)
                .HasForeignKey(bo => bo.BrigadaId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<VehiculoSalida>()
                .HasMany(mo => mo.Incidentes)
                .WithOne(li => li.Vehiculo)
                .HasForeignKey(li => li.VehiculoId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<NovedadBase>()
                .HasOne(n => n.Personal)
                .WithMany(b => b.Novedades)
                .HasForeignKey(n => n.PersonalId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<NovedadVehiculo>()
                .HasOne(n => n.Vehiculo)
                .WithMany(b => b.Novedades)
                .HasForeignKey(n => n.VehiculoId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<Bombero>()
                .HasMany(bo => bo.Incidentes)
                .WithOne(li => li.Encargado)
                .HasForeignKey(li => li.PersonaId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<Bombero>()
                .HasMany(bo => bo.VehiculosEncargado)
                .WithOne(ve => ve.Encargado)
                .HasForeignKey(ve => ve.EncargadoId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            modelBuilder.Entity<Comunicacion>()
                .HasOne(c => c.Movil)
                .WithOne(b => b.HandieMovil)
                .HasForeignKey<Movil>(ei => ei.EquipoId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);



            // Configuración de la relación uno a muchos entre Salida y MovilSalida
            modelBuilder.Entity<MovilSalida>()
                .HasOne(ms => ms.Salida)
                .WithMany(s => s.Moviles)
                .HasForeignKey(ms => ms.SalidaId);

            // Configuración de la relación uno a muchos entre Salida y BomberoSalida
            modelBuilder.Entity<BomberoSalida>()
                .HasOne(bs => bs.Salida)
                .WithMany(s => s.CuerpoParticipante) // Asegúrate de que Salida tenga una colección BomberosSalidas
                .HasForeignKey(bs => bs.SalidaId);

            // Enum
            modelBuilder
                .Entity<Persona>()
                .Property(p => p.Sexo)
                .HasConversion<string>()
                .HasMaxLength(255);
            modelBuilder
               .Entity<MovimientoMaterial>()
               .Property(m => m.TipoMovimiento)
               .HasConversion<string>()
               .HasMaxLength(255);
            modelBuilder
                .Entity<HorarioBombero>()
                .Property(h => h.ModoRotativo)
                .HasConversion<string>()
                .HasMaxLength(255);
            modelBuilder
                .Entity<HorarioBombero>()
                .Property(h => h.DiaSemana)
                .HasConversion<string>()
                .HasMaxLength(255);
            modelBuilder
                .Entity<Bombero>()
                .Property(b => b.Grado)
                .HasConversion<string>()
                .HasMaxLength(255);
            modelBuilder
                .Entity<Licencia>()
                .Property(p => p.EstadoLicencia)
                .HasConversion<string>()
                .HasMaxLength(255);
            modelBuilder
                .Entity<Licencia>()
                .Property(p => p.TipoLicencia)
                .HasConversion<string>()
                .HasMaxLength(255);
            modelBuilder
                .Entity<Bombero>()
                .Property(b => b.Dotacion)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Persona>()
                .Property(b => b.GrupoSanguineo)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Bombero>()
                .Property(b => b.Estado)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<ServicioEspecialRepresentacion>()
                .Property(s => s.TipoRepresentacion)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<ServicioEspecialPrevencion>()
                .Property(s => s.TipoPrevencion)
                .HasConversion<string>()
                .HasMaxLength(255);

            //modelBuilder
              //  .Entity<ServicioEspecial>()
                //.Property(s => s.Tipo)
                //.HasConversion<string>()
                //.HasMaxLength(255);

            modelBuilder
                .Entity<ServicioEspecial>()
                .Property(s => s.TipoOrganizacion)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<RescateAnimal>()
                .Property(r => r.LugarRescateAnimal)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Salida>()
                .Property(s => s.TipoZona)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<RescatePersona>()
                .Property(r => r.LugarRescatePersona)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<IncendioVivienda>()
                .Property(i => i.TipoLugar)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
            .Entity<AscensoBombero>()
            .Property(b => b.GradoAntiguo)
            .HasConversion<string>()
            .HasMaxLength(255);

            modelBuilder
            .Entity<AscensoBombero>()
            .Property(b => b.GradoAscenso)
            .HasConversion<string>()
            .HasMaxLength(255);


            modelBuilder
                .Entity<IncendioIndustria>()
                .Property(i => i.TipoLugar)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<IncendioHospitalesYClinicas>()
                .Property(i => i.TipoLugar)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<IncendioForestal>()
                .Property(i => i.TipoLugar)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Sancion>()
                .Property(S => S.TipoSancion)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Sancion>()
                .Property(S => S.SacionArea)
                .HasConversion<string>()
                .HasMaxLength(255);


            modelBuilder
                .Entity<IncendioEstablecimientoPublico>()
                .Property(i => i.TipoLugar)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<IncendioEstablecimientoEducativo>()
                .Property(i => i.TipoLugar)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<IncendioComercio>()
                .Property(i => i.TipoLugar)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<IncendioVivienda>()
                .Property(i => i.TipoLugar)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Incendio>()
                .Property(i => i.TipoEvacuacion)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Incendio>()
                .Property(i => i.TipoSuperficieAfectada)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Incendio>()
                .Property(i => i.SuperficieAfectadaCausa)
                .HasConversion<string>()
                .HasMaxLength(255);

            //modelBuilder
              //  .Entity<Incendio>()
                //.Property(i => i.Tipo)
                //.HasConversion<string>()
                //.HasMaxLength(255);

            modelBuilder
                .Entity<Incendio>()
                .Property(i => i.TipoAbertura)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Incendio>()
                .Property(i => i.TipoTecho)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<FactorClimatico>()
                .Property(f => f.Tipo)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<FactorClimatico>()
                .Property(f => f.Evacuacion)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<FactorClimatico>()
                .Property(f => f.Superficie)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Accidente>()
                .Property(a => a.Tipo)
                .HasConversion<string>()
                .HasMaxLength(255);


            modelBuilder
                .Entity<BomberoSalida>()
                .Property(b => b.Grado)
                .HasConversion<string>()
                .HasMaxLength(255);
            modelBuilder
                .Entity<Limpieza>()
                .Property(t => t.Incidente)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Incidente>()
                .Property(t => t.Tipo)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Damnificado>()
                .Property(d => d.Sexo)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Damnificado>()
                .Property(d => d.Estado)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<VehiculoSalida>()
                .Property(m => m.Estado)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<MaterialPeligroso>()
                .Property(m => m.Tipo)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Accidente>()
                .Property(a => a.CondicionesClimaticas)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<Comunicacion>()
                .Property(c => c.Estado)
                .HasConversion<string>()
                .HasMaxLength(255);

            modelBuilder
                .Entity<MovilSalida>()
                .Property(d => d.DotacionSalida)
                .HasConversion<string>()
                .HasMaxLength(255);
        }
    }
}

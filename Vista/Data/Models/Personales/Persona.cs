﻿using Vista.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Models.Personales
{
    public class Persona
    {
        public int PersonaId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public TipoSexo Sexo { get; set; }
        public TipoGrupoSanguineo GrupoSanguineo { get; set; }
        public int? Altura { get; set; }
        public int? Peso { get; set; }
        [Required, StringLength(255)]
        public string Nombre { get; set; }
        [Required, StringLength(255)]
        public string Apellido { get; set; }
        [StringLength(255)]
        public string? Direccion { get; set; }
        [StringLength(255)]
        public string? LugarNacimiento { get; set; }
        [StringLength(255)]
        public string? Documento { get; set; }
        [StringLength(255)]
        public string? Observaciones { get; set; }
        [StringLength(255)]
        public string? Profesion { get; set; }
        [StringLength(255)]
        public string? NivelEstudios { get; set; }
        [StringLength(255)]
        public string? NumeroIoma { get; set; }
        public Contacto? Contacto { get; set; }
    }
}

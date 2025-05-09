﻿using Vista.Data.Enums;
using Vista.Data.Models.Salidas.Planillas;
using System.ComponentModel.DataAnnotations;
using Vista.Data.Models.Salidas.Componentes;

namespace Vista.Data.Models.Personas
{
    /// <summary>
    /// Representa a un damnificado.  
    /// </summary>
    public class Damnificado_Salida
    {
        /// <summary>
        /// Identificador único del damnificado.
        /// </summary>
        public int Damnificado_SalidaId { get; set; }

        /// <summary>
        /// Nombre del damnificado.
        /// Campo obligatorio con una longitud máxima de 255 caracteres.
        /// </summary>
        [Required, StringLength(255)]
        public string Nombre { get; set; } = null!;

        /// <summary>
        /// Apellido del damnificado.
        /// Campo obligatorio con una longitud máxima de 255 caracteres.
        /// </summary>
        [Required, StringLength(255)]
        public string Apellido { get; set; } = null!;

        /// <summary>
        /// Sexo del damnificado.
        /// </summary>
        public TipoSexo Sexo { get; set; }

        /// <summary>
        /// Lugar de nacimiento del damnificado.
        /// </summary>
        public string? LugarNacimiento { get; set; }

        /// <summary>
        /// Número de documento de identidad del damnificado.
        /// </summary>
        public int Documento { get; set; }

        /// <summary>
        /// Edad del damnificado.
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Estado del damnificado.
        /// Herido, Fallecido o Desaparecido.
        /// </summary>
        public TipoDamnificado Estado { get; set; }

        /// <summary>
        /// Vehículo del damnificado.
        /// Representa el vehículo involucrado en el incidente.
        /// Relacionado con la clase VehiculoDamnificado.
        /// No es obligatorio.
        /// </summary>
        public VehiculoDamnificado? VehiculoDamnificado { get; set; }

        /// <summary>
        /// Identificador único de la salida a la que pertenece el damnificado.
        /// </summary>
        public int SalidaId { get; set; }

        /// <summary>
        /// Referencia a la salida a la que pertenece el damnificado.
        /// </summary>
        public Salida Salida { get; set; } = null!;
    }
}

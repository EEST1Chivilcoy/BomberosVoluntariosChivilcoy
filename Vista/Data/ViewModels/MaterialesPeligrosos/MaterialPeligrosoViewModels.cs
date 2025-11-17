using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using Vista.Data.Enums;

namespace Vista.Data.ViewModels.MaterialesPeligrosos
{
    public class MaterialPeligrosoViewModels : SalidasViewModels
    {
        // --- Información general del accidente del material peligroso ---

        /// <summary>
        /// Categoría del material peligroso.
        /// </summary>
        [Required]
        public CategoriaMaterialPeligroso? Tipo { get; set; }

        // --- Información general de la sustancia involucrada ---

        /// <summary>
        /// Cantidad de sustancias involucradas en el incidente.
        /// </summary>
        public int CantidadSustancias { get; set; }

        /// <summary>
        /// Sustancias involucradas en el incidente.
        /// </summary>
        public string? Sustancias { get; set; }

        // --- Acciones sobre los materiales peligrosos ---

        /// <summary>
        /// Si estuvo controlada la fuga o escape.
        /// </summary>
        public bool Controlada { get; set; }

        /// <summary>
        /// Si se utilizó venteo como acción sobre los materiales.
        /// </summary>
        public bool Venteo { get; set; }

        /// <summary>
        /// Si hubo dilución de vapores como acción sobre los materiales.
        /// </summary>
        public bool DilucionDeVapores { get; set; }

        /// <summary>
        /// Si se realizó neutralización como acción sobre los materiales.
        /// </summary>
        public bool Neutralizacion { get; set; }

        /// <summary>
        /// Si se efectuó trasvase como acción sobre los materiales.
        /// </summary>
        public bool Trasvase { get; set; }

        /// <summary>
        /// Si se tomaron otras acciones materiales.
        /// </summary>
        public bool OtraAccionesMateriales { get; set; }

        /// <summary>
        /// Detalles sobre las acciones tomadas sobre los materiales peligrosos.
        /// </summary>
        [Required]
        public string? DetallesAccionesMateriales { get; set; }

        // --- Acciones sobre las personas ---

        /// <summary>
        /// Tipo de evacuación realizada. (Si se realizó alguna)
        /// </summary>
        public TipoEvacuacion? Evacuacion { get; set; }

        /// <summary>
        /// Si se realizó descontaminación de personas.
        /// </summary>
        public bool Descontaminacion { get; set; }

        /// <summary>
        /// Si se realizó confinamiento de personas.
        /// </summary>
        public bool Confinamiento { get; set; }

        /// <summary>
        /// Si no se tomaron acciones sobre las personas.
        /// </summary>
        public bool SinAccion { get; set; }

        /// <summary>
        /// Si se tomaron otras acciones sobre las personas.
        /// </summary>
        public bool OtraAccionesPersonas { get; set; }

        /// <summary>
        /// Detalles sobre las acciones tomadas sobre las personas.
        /// </summary>
        [Required]
        public string? DetallesAccionesPersonas { get; set; }

        // --- Detalles de la superficie afectada ---

        public TipoSuperficie? TipoSuperficie { get; set; }
        public int? Cantidad { get; set; }

        // --- Detalles de la situación de explosión ---

        /// <summary>
        /// Situación anterior a la explosión.
        /// </summary>
        public TipoSituacionExplosion? TipoSituacion { get; set; }
    }
}

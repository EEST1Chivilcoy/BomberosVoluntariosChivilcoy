using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using Vista.Data.Models.Personas.Personal.Componentes;
using Vista.Pages;

namespace Vista.Data.Models.Imagenes
{
    /// <summary>
    /// Representa un certificado médico asociado a una licencia médica.
    /// </summary>
    public class CertificadoMedico : Imagen
    {
        /// <summary>
        /// La licencia médica asociada a este certificado médico.
        /// </summary>
        public Licencia Licencia { get; set; } = null!;

        /// <summary>
        /// Identificador único de la licencia médica asociada a este certificado médico.
        /// </summary>
        [Required(ErrorMessage = "El campo 'LicenciaId' es obligatorio. Debe especificar el identificador único de la licencia asociada a la imagen.")]
        public int LicenciaId { get; set; }
    }
}

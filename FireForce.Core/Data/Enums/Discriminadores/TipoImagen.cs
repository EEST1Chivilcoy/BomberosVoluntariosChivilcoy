using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums.Discriminadores
{
    public enum TipoImagen
    {
        /// <summary>
        /// Imagen asociada a personal.
        /// </summary>
        [Display(Name = "Imagen de Personal")]
        ImagenPersonal = 1,

        /// <summary>
        /// Imagen asociada a un vehículo o salida.
        /// </summary>
        [Display(Name = "Imagen de Vehículo/Salida")]
        ImagenVehiculoSalida = 2,

        /// <summary>
        /// Imagen de un certificado médico. Asociada a una licencia médica.
        /// </summary>
        [Display(Name = "Imagen de Certificado Médico")]
        ImagenCertificadoMedico = 3,

        /// <summary>
        /// Imagen de un comprobante bancario.
        /// </summary>
        [Display(Name = "Imagen de Comprobante")]
        ImagenComprobanteBancario = 4
    }
}

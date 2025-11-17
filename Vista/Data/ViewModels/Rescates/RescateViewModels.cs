using System.ComponentModel.DataAnnotations;

namespace Vista.Data.ViewModels.Rescates
{
    public class RescateViewModels: SalidasViewModels
    {
        /// <summary>
        /// Indica si el rescate se realizó en otro lugar no especificado.
        /// </summary>
        public bool OtroLugar { get; set; } = false;
    }
}

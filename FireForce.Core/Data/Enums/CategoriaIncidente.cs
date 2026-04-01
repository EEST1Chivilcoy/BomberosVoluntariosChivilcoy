using System.ComponentModel.DataAnnotations;

namespace Vista.Data.Enums
{
    public enum CategoriaIncidente
    {
        [Display(Name = "Gestión")] // Solo tendria que llegar al Encargado de la Unidad o Departamento (con aprobación)
        Gestion = 1,

        [Display(Name = "Limpieza")] // --> Solo tendria que llegar al Encargado de la Unidad o Departamento (con aprobación)
        Limpieza = 2,

        [Display(Name = "Novedad")] // ---> Tendria que llegar al Encargado Unidad y Departamento de Parque Automotor ---> Derivar a Taller o Material (Tipo aprobación distinta)
        Novedad = 3,

        [Display(Name = "Mantenimiento")] // ---> Tendria que llegar al Departamento de Parque Automotor / Mantenimiento (Sin aprobación) (Es una notificación directa) --> Es una solución directa de x
        Mantenimiento = 4
    }
}

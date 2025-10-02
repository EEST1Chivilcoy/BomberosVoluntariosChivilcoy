using Vista.Data.Models.Personas.Personal;
using Vista.Data.ViewModels.Personal;
using System.Linq;

namespace Vista.Data.Mappers
{
    public static class ComisionDirectivaMapper
    {
        /// <summary>
        /// Convierte un objeto ComisionDirectiva a su ViewModel correspondiente.
        /// </summary>
        /// <param name="comisionDirectiva">El objeto ComisionDirectiva a convertir</param>
        /// <returns>ComisionDirectivaViewModel con los datos mapeados</returns>
        public static ComisionDirectivaViewModel ToComisionDirectivaViewModel(this ComisionDirectiva comisionDirectiva)
        {
            if (comisionDirectiva == null)
                return null;

            var viewModel = new ComisionDirectivaViewModel
            {
                // Propiedades de Persona base
                Id = comisionDirectiva.PersonaId,
                Nombre = comisionDirectiva.Nombre,
                Apellido = comisionDirectiva.Apellido,
                Documento = comisionDirectiva.Documento,
                Sexo = comisionDirectiva.Sexo,
                Direccion = comisionDirectiva.Direccion,

                // Propiedades de Personal
                FechaNacimiento = comisionDirectiva.FechaNacimiento,
                FechaAceptacion = comisionDirectiva.FechaAceptacion,
                GrupoSanguineo = comisionDirectiva.GrupoSanguineo,
                LugarNacimiento = comisionDirectiva.LugarNacimiento,
                EntraID = comisionDirectiva.EntraId,

                // Mapeo de Contacto (si existe)
                TelefonoCel = comisionDirectiva.Contacto?.TelefonoCel,
                TelefonoLaboral = comisionDirectiva.Contacto?.TelefonoLaboral,
                TelefonoFijo = comisionDirectiva.Contacto?.TelefonoFijo,
                Email = comisionDirectiva.Contacto?.Email,

                // Mapeo de Imagen (si existe)
                UrlImagen = comisionDirectiva.Imagen != null ? $"/api/imagenes/{comisionDirectiva.Imagen.ImagenId}" : null,

                // Propiedades específicas de ComisionDirectiva
                Grado = comisionDirectiva.Grado,
                Estado = comisionDirectiva.Estado
            };

            return viewModel;
        }

        /// <summary>
        /// Convierte una lista de ComisionDirectiva a una lista de ComisionDirectivaViewModel.
        /// </summary>
        /// <param name="comisionesDirectivas">Lista de comisiones directivas a convertir</param>
        /// <returns>Lista de ComisionDirectivaViewModel</returns>
        public static List<ComisionDirectivaViewModel> ToComisionDirectivaViewModelList(this List<ComisionDirectiva> comisionesDirectivas)
        {
            if (comisionesDirectivas == null || !comisionesDirectivas.Any())
                return new List<ComisionDirectivaViewModel>();

            return comisionesDirectivas.Select(cd => cd.ToComisionDirectivaViewModel()).ToList();
        }
    }
}
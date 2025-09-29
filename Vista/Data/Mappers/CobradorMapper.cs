using Vista.Data.Models.Personas.Personal;
using Vista.Data.ViewModels.Personal;
using System.Linq;

namespace Vista.Data.Mappers
{
    public static class CobradorMapper
    {
        /// <summary>
        /// Convierte un objeto Cobrador a su ViewModel correspondiente.
        /// </summary>
        /// <param name="cobrador">El objeto Cobrador a convertir</param>
        /// <returns>CobradorViewModel con los datos mapeados</returns>
        public static CobradorViewModel ToCobradorViewModel(this Cobrador cobrador)
        {
            if (cobrador == null)
                return null;

            var viewModel = new CobradorViewModel
            {
                // Propiedades de Persona base
                Id = cobrador.PersonaId,
                Nombre = cobrador.Nombre,
                Apellido = cobrador.Apellido,
                Documento = cobrador.Documento,
                Sexo = cobrador.Sexo,
                Direccion = cobrador.Direccion,

                // Propiedades de Personal
                FechaNacimiento = cobrador.FechaNacimiento,
                FechaAceptacion = cobrador.FechaAceptacion,
                GrupoSanguineo = cobrador.GrupoSanguineo,
                LugarNacimiento = cobrador.LugarNacimiento,

                // Mapeo de Contacto (si existe)
                TelefonoCel = cobrador.Contacto?.TelefonoCel,
                TelefonoLaboral = cobrador.Contacto?.TelefonoLaboral,
                TelefonoFijo = cobrador.Contacto?.TelefonoFijo,
                Email = cobrador.Contacto?.Email,

                // Mapeo de Imagen (si existe)
                UrlImagen = cobrador.Imagen != null ? $"/api/imagenes/{cobrador.Imagen.ImagenId}" : null,

                // Propiedades específicas de Cobrador
                ZonasAsignadas = cobrador.ZonasAsignadas,
                Estado = cobrador.Estado
            };

            return viewModel;
        }

        /// <summary>
        /// Convierte una lista de Cobrador a una lista de CobradorViewModel.
        /// </summary>
        /// <param name="cobradores">Lista de cobradores a convertir</param>
        /// <returns>Lista de CobradorViewModel</returns>
        public static List<CobradorViewModel> ToCobradorViewModelList(this List<Cobrador> cobradores)
        {
            if (cobradores == null || !cobradores.Any())
                return new List<CobradorViewModel>();

            return cobradores.Select(c => c.ToCobradorViewModel()).ToList();
        }
    }
}
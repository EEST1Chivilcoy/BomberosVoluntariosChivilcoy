using Vista.Data.Models.Personas.Personal;
using Vista.Data.ViewModels.Personal;
using System.Linq;

namespace Vista.Data.Mappers
{
    public static class BomberoMapper
    {
        /// <summary>
        /// Convierte un objeto Bombero a su ViewModel correspondiente.
        /// </summary>
        /// <param name="bombero">El objeto Bombero a convertir</param>
        /// <returns>BomberoViweModel con los datos mapeados</returns>
        public static BomberoViweModel ToBomberoViewModel(this Bombero bombero)
        {
            if (bombero == null)
                return null;

            var viewModel = new BomberoViweModel
            {
                // Propiedades de Persona base
                Id = bombero.PersonaId,
                Nombre = bombero.Nombre,
                Apellido = bombero.Apellido,
                Documento = bombero.Documento,
                Sexo = bombero.Sexo,
                Direccion = bombero.Direccion,

                // Propiedades de Personal
                FechaNacimiento = bombero.FechaNacimiento,
                FechaAceptacion = bombero.FechaAceptacion,
                GrupoSanguineo = bombero.GrupoSanguineo,
                LugarNacimiento = bombero.LugarNacimiento,
                Observaciones = bombero.Observaciones,
                Profesion = bombero.Profesion,
                NivelEstudios = bombero.NivelEstudios,
                NumeroIoma = bombero.NumeroIoma,

                // Propiedades específicas de Bombero
                NumeroLegajo = bombero.NumeroLegajo,
                Estado = bombero.Estado,
                Dotacion = bombero.Dotacion,
                Grado = bombero.Grado,
                Altura = bombero.Altura,
                Peso = bombero.Peso,
                EsChofer = bombero.Chofer,
                VencimientoRegistro = bombero.VencimientoRegistro,

                // Mapeo de Contacto (si existe)
                TelefonoCel = bombero.Contacto?.TelefonoCel,
                TelefonoLaboral = bombero.Contacto?.TelefonoLaboral,
                TelefonoFijo = bombero.Contacto?.TelefonoFijo,
                Email = bombero.Contacto?.Email,

                // Mapeo de Imagen (si existe)
                Base64 = bombero.Imagen?.Base64Imagen,
                TipoImagen = bombero.Imagen?.TipoImagen,
                NombreImagen = bombero.Imagen?.NombreImagen,

                // Mapeo de Brigada (primera brigada si existe)
                Brigada = bombero.Brigadas?.FirstOrDefault()?.Brigada,
                BrigadaId = bombero.Brigadas?.FirstOrDefault()?.BrigadaId ?? 0,
                NombreBrigada = bombero.Brigadas?.FirstOrDefault()?.Brigada?.NombreBrigada ?? string.Empty,

                // Propiedad por defecto
                Selected = false
            };

            return viewModel;
        }

        /// <summary>
        /// Convierte una lista de Bomberos a una lista de BomberoViewModels.
        /// </summary>
        /// <param name="bomberos">Lista de bomberos a convertir</param>
        /// <returns>Lista de BomberoViweModel</returns>
        public static List<BomberoViweModel> ToBomberoViewModelList(this List<Bombero> bomberos)
        {
            if (bomberos == null || !bomberos.Any())
                return new List<BomberoViweModel>();

            return bomberos.Select(b => b.ToBomberoViewModel()).ToList();
        }

        /// <summary>
        /// Convierte un objeto Bombero a su ViewModel con una brigada específica seleccionada.
        /// </summary>
        /// <param name="bombero">El objeto Bombero a convertir</param>
        /// <param name="brigadaId">ID de la brigada a marcar como seleccionada</param>
        /// <returns>BomberoViweModel con la brigada específica</returns>
        public static BomberoViweModel ToBomberoViewModel(this Bombero bombero, int brigadaId)
        {
            var viewModel = bombero.ToBomberoViewModel();

            if (viewModel != null && bombero.Brigadas?.Any() == true)
            {
                var brigadaSeleccionada = bombero.Brigadas.FirstOrDefault(bb => bb.BrigadaId == brigadaId);
                if (brigadaSeleccionada != null)
                {
                    viewModel.Brigada = brigadaSeleccionada.Brigada;
                    viewModel.BrigadaId = brigadaSeleccionada.BrigadaId ?? 0;
                    viewModel.NombreBrigada = brigadaSeleccionada.Brigada?.NombreBrigada ?? string.Empty;
                }
            }

            return viewModel;
        }
    }
}
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
                EntraID = bombero.EntraId,
                UPN = bombero.UPN,

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
                Email = bombero.Contacto?.Email,

                // Mapeo de Imagen (si existe)
                UrlImagen = bombero.Imagen != null ? $"/api/imagenes/{bombero.Imagen.ImagenId}" : null,

                // Mapeo de Brigada (hacer que mapee solo los nombres)
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
    }
}
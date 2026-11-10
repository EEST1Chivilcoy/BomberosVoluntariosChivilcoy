using FireForce.Shared.DTOs;
using FireForce.Shared.Services;
using FireForce.Shared.ViewModels.Personal;
using Microsoft.Graph;

namespace FireForce.Core.Services
{
    /// <summary>
    /// Implementación de <see cref="IEntraIDService"/> para trabajar con la autenticación
    /// deshabilitada (<c>Autenticacion:Deshabilitada</c>). No llama a Microsoft Graph: con el
    /// esquema de desarrollo no hay token que adquirir y Microsoft.Identity.Web falla con
    /// IDW10503. Devuelve datos sintéticos para que las altas de personal se puedan cargar a
    /// mano en desarrollo; en producción (bandera en false) se registra el servicio real.
    /// </summary>
    public class EntraIDServiceDeshabilitado : IEntraIDService
    {
        /// <summary>Siempre disponible: el formulario de alta sigue su flujo normal.</summary>
        public Task<bool> CheckDisponibilidadAsync() => Task.FromResult(true);

        /// <summary>
        /// En lugar de buscar en Graph, arma un alta manual con un EntraID sintético (la base
        /// local no habla con Entra ID, así que solo tiene que ser único). Los formularios de
        /// alta dejan Nombre y Apellido deshabilitados porque los trae Graph, así que acá se
        /// deducen del UPN con la convención <c>nombre.apellido</c>; sin eso no habría forma de
        /// cargar personal con nombre en desarrollo.
        /// </summary>
        public Task<(PersonalViewModel? personal, ImagenResultado? foto)> BuscarPorUPNAsync(string upn, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(upn))
                throw new ArgumentException("El UPN no puede estar vacío.");

            var alias = upn.Contains('@') ? upn.Split('@')[0] : upn;
            var partes = alias.Split('.', StringSplitOptions.RemoveEmptyEntries);

            var personal = new PersonalViewModel
            {
                Nombre = Capitalizar(partes.ElementAtOrDefault(0)),
                Apellido = Capitalizar(partes.ElementAtOrDefault(1)),
                EntraID = Guid.NewGuid(),
                UPN = alias
            };

            return Task.FromResult<(PersonalViewModel?, ImagenResultado?)>((personal, null));
        }

        /// <summary>Pasa "maria" a "Maria" y "de-la-torre" a "De La Torre".</summary>
        private static string Capitalizar(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            var palabras = texto.Split('-', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => char.ToUpperInvariant(p[0]) + p[1..].ToLowerInvariant());

            return string.Join(' ', palabras);
        }

        /// <summary>Sin Graph no hay dónde buscar: se responde "no encontrado".</summary>
        public Task<(PersonalViewModel? personal, ImagenResultado? foto)> BuscarPorIDAsync(string id, CancellationToken token) =>
            Task.FromResult<(PersonalViewModel?, ImagenResultado?)>((null, null));

        public Task<User> GetUserAsync() =>
            throw new InvalidOperationException(
                "La autenticación de Entra ID está deshabilitada (Autenticacion:Deshabilitada): " +
                "no hay usuario de Graph. Esta pantalla necesita esa identidad; probala con la " +
                "autenticación real habilitada.");

        public Task<string> GetUserIdAsync() => GetUserAsync().ContinueWith(t => t.Result.Id);
    }
}

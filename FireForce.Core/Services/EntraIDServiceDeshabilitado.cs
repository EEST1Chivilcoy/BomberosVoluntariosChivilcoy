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
        /// En lugar de buscar en Graph, arma un alta manual: nombre y apellido vacíos para
        /// completar en el formulario, y un EntraID sintético (la base local no habla con
        /// Entra ID, así que solo tiene que ser único).
        /// </summary>
        public Task<(PersonalViewModel? personal, ImagenResultado? foto)> BuscarPorUPNAsync(string upn, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(upn))
                throw new ArgumentException("El UPN no puede estar vacío.");

            var personal = new PersonalViewModel
            {
                Nombre = string.Empty,
                Apellido = string.Empty,
                EntraID = Guid.NewGuid(),
                UPN = upn.Contains('@') ? upn.Split('@')[0] : upn
            };

            return Task.FromResult<(PersonalViewModel?, ImagenResultado?)>((personal, null));
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

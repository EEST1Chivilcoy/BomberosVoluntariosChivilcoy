using FireForce.Shared.DTOs;
using FireForce.Shared.ViewModels.Personal;
using Microsoft.Graph;

namespace FireForce.Shared.Services
{
    public interface IEntraIDService
    {
        Task<(PersonalViewModel? personal, ImagenResultado? foto)> BuscarPorUPNAsync(string upn, CancellationToken token);
        Task<(PersonalViewModel? personal, ImagenResultado? foto)> BuscarPorIDAsync(string id, CancellationToken token);
        Task<bool> CheckDisponibilidadAsync();
        Task<User> GetUserAsync();
        Task<string> GetUserIdAsync();
    }
}

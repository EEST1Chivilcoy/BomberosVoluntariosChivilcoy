using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Vista.Services
{
    public class CircuitHandlerService : CircuitHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CircuitHandlerService> _logger;

        public CircuitHandlerService(
            IHttpContextAccessor httpContextAccessor,
            ILogger<CircuitHandlerService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            // Capturar el HttpContext cuando se establece la conexión
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext != null)
            {
                _logger.LogInformation("Circuit establecido para usuario: {User}",
                    httpContext.User?.Identity?.Name ?? "Anónimo");
            }
            else
            {
                _logger.LogWarning("HttpContext no disponible en OnConnectionUpAsync");
            }

            return base.OnConnectionUpAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Circuit cerrado: {CircuitId}", circuit.Id);
            return base.OnConnectionDownAsync(circuit, cancellationToken);
        }
    }
}
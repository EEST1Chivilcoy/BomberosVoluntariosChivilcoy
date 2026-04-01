using Microsoft.EntityFrameworkCore;
using Vista.Data;

namespace Vista.Services
{
    public class InitData : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InitData(IServiceProvider serviceProvider, IWebHostEnvironment webHostEnvironment)
        {
            _serviceProvider = serviceProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ApplyMigrations(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task ApplyMigrations(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<BomberosDbContext>();
            await context.Database.MigrateAsync(cancellationToken);
        }
    }
}

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sharpbox.Graceful.Services
{
    public class ImportantService : BackgroundService, IImportantService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public ImportantService(ILogger<ImportantService> logger,
                                IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _applicationLifetime = applicationLifetime;

            _applicationLifetime.ApplicationStarted.Register(OnStarted);
            _applicationLifetime.ApplicationStopping.Register(OnStopping);
            _applicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Doing very important stuff...");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            _logger.LogInformation("Oops, cancellation was requested!");
        }

        private void OnStarted()
        {
            _logger.LogInformation($"Executing: {nameof(OnStarted)}, I should get ready to work!");
        }

        private void OnStopping()
        {
            _logger.LogInformation($"Executing: {nameof(OnStopping)}, I should block stopping and clean up!");
        }

        private void OnStopped()
        {
            _logger.LogInformation($"Executing: {nameof(OnStopped)}, I should stop!");
        }
    }
}
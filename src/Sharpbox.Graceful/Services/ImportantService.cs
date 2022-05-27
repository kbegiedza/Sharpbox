using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Sharpbox.Graceful.Services
{
    public sealed class ImportantService : BackgroundService, IImportantService
    {
        private readonly ILogger _logger;

        private readonly CancellationTokenRegistration _onStartedRegistration;
        private readonly CancellationTokenRegistration _onStoppedRegistration;
        private readonly CancellationTokenRegistration _onStoppingRegistration;


        public ImportantService(ILogger<ImportantService> logger,
                                IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;

            _onStartedRegistration = applicationLifetime.ApplicationStarted.Register(OnStarted);
            _onStoppedRegistration = applicationLifetime.ApplicationStopped.Register(OnStopped);
            _onStoppingRegistration = applicationLifetime.ApplicationStopping.Register(OnStopping);
        }

        public override void Dispose()
        {
            _onStartedRegistration.Dispose();
            _onStoppedRegistration.Dispose();
            _onStoppingRegistration.Dispose();

            base.Dispose();
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
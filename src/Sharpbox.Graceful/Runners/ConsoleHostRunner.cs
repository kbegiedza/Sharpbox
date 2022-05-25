using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sharpbox.Graceful.Services;

namespace Sharpbox.Graceful.Runners
{
    public class ConsoleHostRunner : Runner
    {
        protected override async Task ExecuteAsync()
        {
            using var host = Host.CreateDefaultBuilder()
                                 .ConfigureServices(services =>
                                 {
                                     services.AddHostedService<ImportantService>();
                                 })
                                 .Build();

            await host.RunAsync();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sharpbox.Client;

using var host = Host.CreateDefaultBuilder()
                     .UseConsoleLifetime()
                     .ConfigureServices((builder, services) =>
                     {
                         services.AddHostedService<SendMessageService>();
                     })
                     .Build();

await host.RunAsync();
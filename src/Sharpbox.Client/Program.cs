using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Hosting;
using Orleans.Streams;
using Sharpbox.Client;

using var host = Host.CreateDefaultBuilder()
                     .UseConsoleLifetime()
                     .UseOrleansClient(clientBuilder =>
                     {
                         clientBuilder.UseLocalhostClustering();
                         clientBuilder.AddMemoryStreams("MSStream");
                     })
                     .ConfigureServices((builder, services) =>
                     {
                         services.AddHostedService<SendMessageService>();
                     })
                     .Build();

await host.RunAsync();
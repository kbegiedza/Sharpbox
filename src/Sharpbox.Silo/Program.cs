using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;

using var host = Host.CreateDefaultBuilder()
                     .UseOrleans(siloBuilder =>
                     {
                         siloBuilder.UseLocalhostClustering();
                         siloBuilder.UseDashboard(options =>
                         {
                             options.HostSelf = true;
                         });

                         siloBuilder.AddStreaming();
                         siloBuilder.AddMemoryStreams("MSStream");

                         siloBuilder.AddMemoryGrainStorage("PubSubStore");
                     })
                     .UseConsoleLifetime()
                     .Build();

await host.RunAsync();
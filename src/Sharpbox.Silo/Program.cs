using Orleans.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans;

using var host = Host.CreateDefaultBuilder()
                     .UseOrleans(siloBuilder =>
                     {
                         siloBuilder.UseLocalhostClustering();
                         siloBuilder.UseDashboard(options =>
                         {
                             options.HostSelf = true;
                         });

                         siloBuilder.AddSimpleMessageStreamProvider("SMSProvider")
                                    .AddMemoryGrainStorage("PubSubStore");

                         siloBuilder.ConfigureApplicationParts(p => p.AddFromApplicationBaseDirectory());
                     })
                     .UseConsoleLifetime()
                     .Build();

await host.RunAsync();
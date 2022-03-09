using Orleans.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans;

using var host = Host.CreateDefaultBuilder()
                     .UseOrleans(siloBuilder =>
                     {
                         siloBuilder.UseLocalhostClustering();
                         siloBuilder.UseDashboard(options =>
                         {
                             options.Username = "Sharpbox";
                             options.Password = "Sharpbox123!";
                             options.Host = "*";
                             options.Port = 8080;
                             options.HostSelf = true;
                         });

                         siloBuilder.ConfigureApplicationParts(p => p.AddFromApplicationBaseDirectory());
                     })
                     .UseConsoleLifetime()
                     .Build();

await host.RunAsync();
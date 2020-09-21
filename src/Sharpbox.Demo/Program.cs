using Microsoft.Extensions.DependencyInjection;
using Sharpbox.Demo;

var services = new ServiceCollection();

services.AddSingleton<IDemo, RecordsDemo>();
services.AddSingleton<IDemo, SerializerDemo>();

var provider = services.BuildServiceProvider();

foreach (var demo in provider.GetServices<IDemo>())
{
    await demo.RunAsync();
}
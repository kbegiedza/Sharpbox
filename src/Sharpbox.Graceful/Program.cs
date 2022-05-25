using System.Reflection;
using CommandLine;
using Sharpbox.Graceful.Options;
using Sharpbox.Graceful.Runners;

await CommandLine.Parser.Default.ParseArguments(args, FindAllVerbs())
                                .WithParsedAsync(RunAsync);

async Task RunAsync(object option)
{
    var runner = GetRunner(option);

    if (runner != null)
    {
        await runner.RunAsync();
    }
}

IRunner? GetRunner(object option)
{
    switch (option)
    {
        case ConsoleEventsOption:
            return new ConsoleEventsRunner();
        case ConsoleHostOptions:
            return new ConsoleHostRunner();
        default:
            Console.WriteLine($"Option of type: '{option.GetType().Name}' is not supported");
            return null;
    }
}

Type[] FindAllVerbs()
{
    return Assembly.GetExecutingAssembly()
                   .GetTypes()
                   .Where(t => t.GetCustomAttribute<VerbAttribute>() != null)
                   .ToArray();
}
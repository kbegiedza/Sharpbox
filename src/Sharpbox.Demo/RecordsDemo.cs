using System;
using System.Text.Json;

namespace Sharpbox.Demo
{
    public class RecordsDemo : IDemo
    {
        public void Run()
        {
            var firstPerson = new Detection("person", 0.9);
            var secondPerson = new Detection("person", 0.9);
            var newPerson = secondPerson with { Probability = 0.5 };

            Console.WriteLine($"Record: {firstPerson.Name} | {firstPerson.Probability}");
            Console.WriteLine($"Are 1 and 2 records equals? {firstPerson == secondPerson}");
            Console.WriteLine($"Are 2 and 3 records equals? {secondPerson == newPerson}");

            var firstExtended = new ExtendedDetection("person", 0.9, "area-42");
            Console.WriteLine($"Are firstExtended and first records equals? {firstExtended == secondPerson}");
        }

        public record Detection(string Name, double Probability);

        public record ExtendedDetection(string Name, double Probability, string DisplayName) : Detection(Name, Probability);

        public record FullDetection(string Name, double Probability, Guid Location)
            : Detection(Name, Probability)
        {
            DateTimeOffset Date { get; init; }
        }
    }
}
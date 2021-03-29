using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sharpbox.Demo
{
    public class RecordsDemo : IDemo
    {
        public Task RunAsync()
        {
            ProcessRecords();
            ProcessRecordsSerialization();

            return Task.CompletedTask;
        }

        private static void ProcessRecordsSerialization()
        {
            var detections = new[]
            {
                new Detection("cat", 0.6),
                new Detection("dog", 0.94),
                new Detection("person", 0.9),
            };

            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var serializedDetections = JsonSerializer.Serialize(detections, serializeOptions);
            var deserializedDetections = JsonSerializer.Deserialize<Detection[]>(serializedDetections);

            if (deserializedDetections != null)
            {
                for (var i = 0; i < deserializedDetections.Length; ++i)
                {
                    if (deserializedDetections[i] != detections[i])
                    {
                        throw new InvalidOperationException($"Wrong item at {i} position");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException($"Cannot parse? huh...");
            }

            Console.WriteLine("Serialized & deserialized records");
        }

        private static void ProcessRecords()
        {
            var firstPerson = new Detection("person", 0.9);
            var secondPerson = new Detection("person", 0.9);
            var newPerson = secondPerson with { Probability = 0.5 };

            Console.WriteLine($"Record: {firstPerson.Name} | {firstPerson.Probability}");
            Console.WriteLine($"Are 1 and 2 records equals? {firstPerson == secondPerson}");
            Console.WriteLine($"Are 2 and 3 records equals? {secondPerson == newPerson}");

            var firstExtended = new ExtendedDetection("person", 0.9, "area-42");
            Console.WriteLine($"Are firstExtended and second record equals? {firstExtended == secondPerson}");

            var firstFull = new FullDetection("name", 1.0, Guid.NewGuid())
            {
                Date = DateTimeOffset.UtcNow
            };

            Console.WriteLine($"Are {nameof(firstFull)} and first record equals? {firstFull == firstPerson}");
        }

        public record Detection(string Name, double Probability);

        public record ExtendedDetection(string Name, double Probability, string DisplayName) : Detection(Name, Probability);

        public record FullDetection(string Name, double Probability, Guid Location)
            : Detection(Name, Probability)
        {
            public DateTimeOffset Date { get; init; }
        }
    }
}
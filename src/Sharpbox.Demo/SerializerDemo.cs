using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sharpbox.Demo
{
    public class SerializerDemo : IDemo
    {
        public Task RunAsync()
        {
            ProcessImmutableSerialization();

            return Task.CompletedTask;
        }

        private static void ProcessImmutableSerialization()
        {
            var data = new ImmutableData
            {
                SomeData = 42
            };

            var serializedData = JsonSerializer.Serialize(data);
            var deserializedData = JsonSerializer.Deserialize<ImmutableData>(serializedData);

            if (!data.Equals(deserializedData))
            {
                throw new Exception("Immutable struct serialization / deserialization is broken :(");
            }

            Console.WriteLine("Immutable struct serialization / deserialization works!");
        }

        public struct ImmutableData
        {
            public int SomeData { get; init; }
        }
    }
}
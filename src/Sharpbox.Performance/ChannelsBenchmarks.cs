using System.Threading.Channels;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Sharpbox.Demo;
using Sharpbox.Demo.Entities;

namespace Sharpbox.Performance
{
    [MemoryDiagnoser]
    public class ChannelsBenchmark
    {
        [Params(100)]
        public int N { get; set; }

        [Benchmark]
        public async Task Channel_Write()
        {
            var options = new BoundedChannelOptions(1)
            {
                FullMode = BoundedChannelFullMode.DropOldest,
            };

            var channel = Channel.CreateBounded<BigContext>(options);
            var reader = channel.Reader;
            var writer = channel.Writer;

            var item = new BigContext();

            for (var i = 0; i < N; i++)
            {
                await writer.WriteAsync(item);
                await reader.ReadAsync();
            }
        }
    }
}
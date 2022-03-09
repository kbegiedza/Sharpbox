using Microsoft.Extensions.Logging;
using Orleans;

namespace Sharpbox.Grains
{
    public class MessageGrain : Grain, IMessageGrain
    {
        private ulong _counter;
        private ILogger _logger;

        public MessageGrain(ILogger<MessageGrain> logger)
        {
            _logger = logger;
        }

        public Task<string> SendMessageAsync(string message)
        {
            _logger.LogInformation($"{_counter++}");

            return Task.FromResult($"{RuntimeIdentity} got message: {message}");
        }
    }
}
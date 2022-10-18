using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Streams;

namespace Sharpbox.Grains
{
    [ImplicitStreamSubscription("ChatRoomStream")]
    public class ScribeGrain : Grain, IScribeGrain, IAsyncObserver<string>
    {
        private readonly ILogger _logger;

        private IAsyncStream<string>? _stream;
        private StreamSubscriptionHandle<string>? _handle;

        public ScribeGrain(ILogger<ScribeGrain> logger)
        {
            _logger = logger;
        }

        public override async Task OnActivateAsync()
        {
            var provider = GetStreamProvider("SMSProvider");
            _stream = provider.GetStream<string>(this.GetPrimaryKey(), "ChatRoomStream");

            if (_stream != null)
            {
                _handle = await _stream.SubscribeAsync(OnNextAsync);
            }

            await base.OnActivateAsync();
        }

        public override async Task OnDeactivateAsync()
        {
            if (_handle != null)
            {
                await _handle.UnsubscribeAsync();
            }

            await base.OnDeactivateAsync();
        }


        public Task OnCompletedAsync()
        {
            _logger.LogInformation(nameof(OnCompletedAsync));

            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            _logger.LogError(ex, nameof(OnErrorAsync));

            return Task.CompletedTask;
        }

        public async Task OnNextAsync(string item, StreamSequenceToken token)
        {
            _logger.LogInformation($"Scribe: {item}");

            await Task.Delay(5000);
        }
    }
}
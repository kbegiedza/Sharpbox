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

        public override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);

            var streamProvider = this.GetStreamProvider("MSStream");
            _stream = streamProvider.GetStream<string>("ChatRoomStream", this.GetPrimaryKey());

            if (_stream != null)
            {
                _handle = await _stream.SubscribeAsync(OnNextAsync);
            }
        }

        public override async Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
        {
            if (_handle != null)
            {
                await _handle.UnsubscribeAsync();
            }

            await base.OnDeactivateAsync(reason, cancellationToken);
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

        public async Task OnNextAsync(string item, StreamSequenceToken? token)
        {
            _logger.LogInformation($"Scribe: {item}");

            await Task.Delay(5000);
        }
    }
}
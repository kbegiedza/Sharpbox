using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using Sharpbox.Grains;

namespace Sharpbox.Client
{
    public class SendMessageService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IClusterClient _clusterClient;

        public SendMessageService(ILogger<SendMessageService> logger, IClusterClient clusterClient)
        {
            _logger = logger;
            _clusterClient = clusterClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var chatGrain = _clusterClient.GetGrain<IChatRoomGrain>(Guid.Empty);

                var provider = _clusterClient.GetStreamProvider("MSStream");

                var streamId = StreamId.Create("ChatRoomStream", Guid.Empty);

                var stream = provider.GetStream<string>(streamId);

                await stream.SubscribeAsync(new ChatObserver());

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.5));

                    var tasks = new List<Task>();

                    for (var i = 0; i < 5; ++i)
                    {
                        _logger.LogInformation($"Sending message from {i}");

                        tasks.Add(chatGrain.SendMessage(i.ToString()));
                    }

                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
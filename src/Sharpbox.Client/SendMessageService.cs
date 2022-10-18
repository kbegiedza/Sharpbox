using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Sharpbox.Grains;

namespace Sharpbox.Client
{
    public class SendMessageService : BackgroundService
    {
        private readonly ILogger _logger;

        public SendMessageService(ILogger<SendMessageService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var client = await ConnectAsync();

                var chatGrain = client.GetGrain<IChatRoomGrain>(Guid.Empty);

                var provider = client.GetStreamProvider("SMSProvider");

                var stream = provider.GetStream<string>(Guid.Empty, "ChatRoomStream");

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

        private static async Task<IClusterClient> ConnectAsync()
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "dev";
                })
                .AddSimpleMessageStreamProvider("SMSProvider")
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();

            return client;
        }
    }
}
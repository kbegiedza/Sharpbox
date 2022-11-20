using Orleans.Streams;

namespace Sharpbox.Client
{
    public class ChatObserver : IAsyncObserver<string>
    {
        public Task OnCompletedAsync()
        {
            Console.WriteLine("OnCompletedAsync");

            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            Console.WriteLine("OnErrorAsync");

            return Task.CompletedTask;
        }

        public Task OnNextAsync(string item, StreamSequenceToken? token)
        {
            Console.WriteLine(item);

            return Task.CompletedTask;
        }
    }
}
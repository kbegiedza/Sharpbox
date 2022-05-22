namespace Sharpbox.Graceful.Runners
{
    public abstract class Runner : IRunner
    {
        public async Task RunAsync()
        {
            Console.WriteLine($"Starting: '{GetType().Name}'.");

            await ExecuteAsync();

            Console.WriteLine($"Finished: '{GetType().Name}'.");
        }

        protected abstract Task ExecuteAsync();
    }
}
namespace Sharpbox.Graceful.Runners
{
    public class ConsoleEventsRunner : Runner
    {
        private readonly TaskCompletionSource _completionSource;

        public ConsoleEventsRunner()
        {
            _completionSource = new TaskCompletionSource();
        }

        protected override async Task ExecuteAsync()
        {
            Console.CancelKeyPress += OnCancelKeyPress;
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            await _completionSource.Task;

            await Task.Delay(TimeSpan.FromSeconds(10));

            Console.CancelKeyPress -= OnCancelKeyPress;
            AppDomain.CurrentDomain.ProcessExit -= OnProcessExit;
        }

        private void OnProcessExit(object? sender, EventArgs e)
        {
            Console.WriteLine($"Got {nameof(OnProcessExit)}");

            _completionSource.SetResult();
        }

        private void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine($"Got {nameof(OnCancelKeyPress)}");

            _completionSource.SetResult();
        }
    }
}
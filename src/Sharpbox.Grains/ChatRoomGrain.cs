using Orleans;
using Orleans.Streams;

namespace Sharpbox.Grains
{
    public class ChatRoomGrain : Grain, IChatRoomGrain
    {
        private readonly HashSet<Guid> _users;
        private IAsyncStream<string>? _stream;

        public ChatRoomGrain()
        {
            _users = new HashSet<Guid>();
        }

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();

            var streamProvider = GetStreamProvider("SMSProvider");
            _stream = streamProvider.GetStream<string>(Guid.Empty, "ChatRoomStream");
        }

        public Task JoinAsync(Guid user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
            }

            return Task.CompletedTask;
        }

        public Task LeaveAsync(Guid user)
        {
            _users.Remove(user);

            return Task.CompletedTask;
        }

        public async Task SendMessage(string message)
        {
            if (_stream != null)
            {
                await _stream.OnNextAsync(message);
            }
        }
    }
}
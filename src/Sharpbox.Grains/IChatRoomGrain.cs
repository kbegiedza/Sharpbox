using Orleans;

namespace Sharpbox.Grains
{
    public interface IChatRoomGrain : IGrainWithGuidKey
    {
        public Task JoinAsync(Guid user);

        public Task LeaveAsync(Guid user);

        public Task SendMessage(string message);
    }
}
using Orleans;

namespace Sharpbox.Grains
{
    public interface IMessageGrain : IGrainWithStringKey
    {
        Task<string> SendMessageAsync(string message);
    }
}
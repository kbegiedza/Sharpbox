using Sharpbox.Api.Leaderboards.Entities;

namespace Sharpbox.Api.Leaderboards.Persistence
{
    public interface ILeaderboardsRepository
    {
        Task<Leaderboard> GetAsync(Guid id);

        Task<Leaderboard> CreateAsync(Leaderboard leaderboard);

        Task<Leaderboard> UpdateAsync(Leaderboard leaderboard);

        Task DeleteAsync(Guid id);
    }
}
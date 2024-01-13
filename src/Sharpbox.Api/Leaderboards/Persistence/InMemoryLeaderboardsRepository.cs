using Sharpbox.Api.Leaderboards.Core;

namespace Sharpbox.Api.Leaderboards.Persistence;

public class InMemoryLeaderboardsRepository : ILeaderboardsRepository
{
    public Task<Leaderboard> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Leaderboard> CreateAsync(Leaderboard leaderboard)
    {
        throw new NotImplementedException();
    }

    public Task<Leaderboard> UpdateAsync(Leaderboard leaderboard)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
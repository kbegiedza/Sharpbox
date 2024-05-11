using Sharpbox.Api.Core.Persistence;
using Sharpbox.Api.Leaderboards.Entities;

namespace Sharpbox.Api.Leaderboards.Persistence
{
    public class InMemoryLeaderboardsRepository : InMemoryRepository<Guid, Leaderboard>, ILeaderboardsRepository
    {
    }
}
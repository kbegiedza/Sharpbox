namespace Sharpbox.Api.Leaderboards.Core
{
    public interface ILeaderboardsRepository
    {
        Task<Leaderboard> GetAsync(Guid id);

        Task<Leaderboard> CreateAsync(Leaderboard leaderboard);
        
        Task<Leaderboard> UpdateAsync(Leaderboard leaderboard);
        
        Task DeleteAsync(Guid id);
    }
}
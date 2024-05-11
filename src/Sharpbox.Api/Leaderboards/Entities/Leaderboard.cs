using Sharpbox.Api.Core.Persistence;

namespace Sharpbox.Api.Leaderboards.Entities
{
    public class Leaderboard : IEntity<Guid>
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }
    }
}
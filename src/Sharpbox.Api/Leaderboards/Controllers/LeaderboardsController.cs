using Microsoft.AspNetCore.Mvc;

namespace Sharpbox.Api.Leaderboards.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LeaderboardsController : ControllerBase
    {
        private readonly ILogger _logger;

        public LeaderboardsController(ILogger<LeaderboardsController> logger)
        {
            _logger = logger;
        }
    }
}
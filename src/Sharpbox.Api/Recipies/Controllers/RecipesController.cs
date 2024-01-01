using Microsoft.AspNetCore.Mvc;

namespace Sharpbox.Api.Recipies.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ILogger _logger;

        public RecipesController(ILogger<RecipesController> logger)
        {
            _logger = logger;
        }
    }
}
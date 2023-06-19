using Sharpbox.Api.Recipies.Entities;

namespace Sharpbox.Api.Recipies.Persistence
{
    public class IRecipesRepository
    {
        public IAsyncEnumerable<Recipe> GetRecipesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetRecipesEnumerableAsync()
        {
            throw new NotImplementedException();
        }
    }
}
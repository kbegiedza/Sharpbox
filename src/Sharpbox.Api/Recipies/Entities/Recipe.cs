namespace Sharpbox.Api.Recipies.Entities
{
    public class Recipe
    {
        required public Guid Id { get; set; }

        required public string Name { get; set; }

        required public string Description { get; set; }

        required public List<Ingredient> Ingredients { get; set; }
    }
}
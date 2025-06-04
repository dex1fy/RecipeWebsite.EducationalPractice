namespace RecipeWebsiteBackend.Models.DTOs.Recipe
{
    public class RecipeDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CookingTime { get; set; }
        public string Steps { get; set; }
        public string Image { get; set; }
        public string ShortDescription { get; set; }
        public int Proteins { get; set; }
        public int Fats { get; set; }
        public int Carbohydrates { get; set; }
        public int Calories { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }

    public class IngredientDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
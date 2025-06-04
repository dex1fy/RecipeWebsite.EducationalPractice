namespace RecipeWebsiteBackend.Models.DTOs.Recipe
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CookingTime { get; set; }

        public string imgUrl { get; set; }
    }
}

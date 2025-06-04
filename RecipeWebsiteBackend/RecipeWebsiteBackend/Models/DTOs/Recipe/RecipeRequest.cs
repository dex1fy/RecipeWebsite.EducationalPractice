namespace RecipeWebsiteBackend.Models.DTOs.Recipe
{
    public class RecipeRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        public string? Name { get; set; }


        public string? CookingTime { get; set; }

        public string? Steps { get; set; }


        public string? Image { get; set; }


        public Guid CatKey { get; set; }  // Ссылается на CategoryDishModel.id


        public Guid CatCuisineKey { get; set; } // Ссылается на CategoryDishCuisineModel.id


        public Guid CatMenuKey { get; set; } // Ссылается на CategoryDishMenuModel.id


        public string? ShortDescription { get; set; }


        public int Squirrels { get; set; }

        public int Fats { get; set; }


        public int Carbohydrates { get; set; }

        public int Calories { get; set; }
    }

}

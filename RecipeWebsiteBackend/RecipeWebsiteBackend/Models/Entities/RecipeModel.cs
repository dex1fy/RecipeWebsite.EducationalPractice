using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace RecipeWebsiteBackend.Models.Entities
{
    [Table("recipe")]
    public class Recipe : BaseModel
    {
        [PrimaryKey("id")]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("cooking_time")]
        public string CookingTime { get; set; }

        [Column("steps")]
        public string Steps { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("cat_key")]
        public Guid CatKey { get; set; }  // Ссылается на CategoryDishModel.id

        [Column("cat_cuisine_key")]
        public Guid CatCuisineKey { get; set; } // Ссылается на CategoryDishCuisineModel.id

        [Column("cat_menu_key")] 
        public Guid CatMenuKey { get; set; } // Ссылается на CategoryDishMenuModel.id

        [Column("short_descrip")]
        public string ShortDescription { get; set; }

        [Column("squirrels")]
        public int Squirrels { get; set; }

        [Column("fats")]
        public int Fats { get; set; }

        [Column("carbohydrates")]
        public int Carbohydrates { get; set; }

        [Column("calories")]
        public int Calories { get; set; }

        //// Навигационные свойства (для внешних ключей)
        //[Reference(typeof(CategoryDish), "cat_key", "id")]
        //public CategoryDish Category { get; set; }

        //[Reference(typeof(CategoryDishCuisineModel), "cat_cuisine_key", "id")]
        //public CategoryDishCuisineModel CategoryCuisine { get; set; }

        //[Reference(typeof(CategoryDishMenuModel), "cat_menu_key", "id")]
        //public CategoryDishMenuModel CategoryMenu { get; set; }
        
    }
}

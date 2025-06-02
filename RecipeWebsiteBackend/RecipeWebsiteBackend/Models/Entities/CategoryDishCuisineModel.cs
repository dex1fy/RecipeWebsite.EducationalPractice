using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace RecipeWebsiteBackend.Models.Entities
{
    [Table("category_dish_cuisine")]
    public class CategoryDishCuisineModel : BaseModel
    {
        [PrimaryKey("id")]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}

using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace RecipeWebsiteBackend.Models.Entities
{
    [Table("category_dish_menu")]
    public class CategoryDishMenuModel : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}

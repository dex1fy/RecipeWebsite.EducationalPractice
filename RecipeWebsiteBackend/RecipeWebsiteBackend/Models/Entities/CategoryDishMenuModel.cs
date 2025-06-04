using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace RecipeWebsiteBackend.Models.Entities
{
    [Table("category_dish_menu")]
    public class CategoryDishMenuModel : BaseModel
    {
        [PrimaryKey("id")]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}

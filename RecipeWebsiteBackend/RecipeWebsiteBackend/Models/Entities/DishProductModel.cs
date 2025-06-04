using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace RecipeWebsiteBackend.Models.Entities
{
    [Table("dish_product")]
    public class DishProductModel : BaseModel
    {
        [PrimaryKey("id")]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("dish_id")]
        public Guid DishId { get; set; }

        [Column("product_id")]
        public Guid ProductId { get; set; }

    }
}

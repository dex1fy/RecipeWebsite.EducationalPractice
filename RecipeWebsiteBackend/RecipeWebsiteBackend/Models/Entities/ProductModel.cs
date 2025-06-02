using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace RecipeWebsiteBackend.Models.Entities
{
    [Table("product")]
    public class ProductModel : BaseModel
    {
        [PrimaryKey("id")]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string name { get; set; }
    }
}

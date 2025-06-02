using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace RecipeWebsiteBackend.Models.Entities
{
    [Table("users")]
    public class UserModel : BaseModel
    {
        [PrimaryKey("id")]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("username")]
        public string? UserName { get; set; }

        [Column("useremail")]
        public string? UserEmail { get; set; }
    }
}

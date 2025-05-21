using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace RecipeWebsiteBackend.Models
{
    /// <summary>
    /// тестовая модель для проверки подключения к базе (потом удалить)
    /// </summary>
    [Table("ForTest")]
    public class TestModel : BaseModel
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("title")]
        public string Title { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} TITLE: {Title}";
        }
    }
}

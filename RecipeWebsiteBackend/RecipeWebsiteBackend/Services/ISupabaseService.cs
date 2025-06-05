using Supabase;

namespace RecipeWebsiteBackend.Services
{
    public interface ISupabaseService
    {
        Task<Client> InitSupabase();
    }
}

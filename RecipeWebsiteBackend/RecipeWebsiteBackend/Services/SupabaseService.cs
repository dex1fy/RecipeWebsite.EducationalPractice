using RecipeWebsiteBackend.Models.Entities;

namespace RecipeWebsiteBackend.Services
{
    /// <summary>
    /// Сервис (класс) для подключения к базе данных supabase
    /// </summary>
    public class SupabaseService
    {
        /// <summary>
        /// объявление приватных переменных класса
        /// </summary>
        private readonly IConfiguration _configuration;
        private string _supabaseUrl;
        private string _supabaseKey;
        private Supabase.Client _supabaseClient;

        /// <summary>
        /// конструктор класса с инициализацией 
        /// </summary>
        /// <param name="configuration"></param>
        public SupabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            _supabaseUrl = _configuration["Supabase:Url"]; // ссылка и ключ закрыты и берутся из appsettings.Development.json.
            _supabaseKey = _configuration["Supabase:Key"];
        }

        /// <summary>
        /// асинхронный метод инициализации, для получения экземпляра класса Supabase.Client. В нем задаются различные параметры
        /// </summary>
        /// <returns></returns>
        public async Task<Supabase.Client> InitSupabase()
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true,
                AutoRefreshToken = true,
            };

            // создается экземпляр Supabase.Client
            _supabaseClient = new Supabase.Client(_supabaseUrl, _supabaseKey, options);
            await _supabaseClient.InitializeAsync();

            // возвращается экземпляр Supabase.Client
            return _supabaseClient;
        }
    
 
    }
}

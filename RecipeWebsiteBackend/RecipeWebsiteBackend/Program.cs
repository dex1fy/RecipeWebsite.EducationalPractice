
using RecipeWebsiteBackend.Models;
using RecipeWebsiteBackend.Services;
using System.Diagnostics;

namespace RecipeWebsiteBackend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            var testModel = new TestModel
            {
                Title = "Test title!!!",
            };

            var supabaseService = new SupabaseService(builder.Configuration); 
            var supabaseClient = await supabaseService.InitSupabase();
            supabaseClient.From<TestModel>().Insert(testModel);
            var data = await supabaseClient.From<TestModel>().Get();
            var result = data.Model;
            Debug.WriteLine("----------" + result.ToString());

            app.Run();
        }
    }
}

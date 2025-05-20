using RecipeWebsite.Models;
using RecipeWebsite.Services;
using Supabase.Interfaces;
using System.Diagnostics;

namespace RecipeWebsite
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            // ������ ��������� �������� ������ 
            var testModel = new TestModel
            {
                Title = "Test title!!!",
            };

            // ��������, �������� �� ���� 
            var supabaseService = new SupabaseService(builder.Configuration); // ������� ������
            var supabaseClient = await supabaseService.InitSupabase(); // ������� ������

            //  ��� ���������
            supabaseClient.From<TestModel>().Insert(testModel);

            // ��� ��������
            var data = await supabaseClient.From<TestModel>().Get();
            var result = data.Model;

            // ����� � ���� ������
            Debug.WriteLine("----------" + result.ToString());

            app.Run();
        }
    }
}

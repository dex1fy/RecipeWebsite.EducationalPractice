
using Microsoft.IdentityModel.Tokens;
using RecipeWebsiteBackend.Models;
using RecipeWebsiteBackend.Services;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

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
            var bytes = Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtSecret"]);
            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://bwvendrvjgleqymbzfec.supabase.co/auth/v1";
                    options.MetadataAddress = "https://bwvendrvjgleqymbzfec.supabase.co/auth/v1/.well-known/openid-configuration";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(bytes),
                        ValidAudience = builder.Configuration["Authentication:ValidAudience"],
                        ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],

                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                  
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            builder.Services.AddScoped<SupabaseService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

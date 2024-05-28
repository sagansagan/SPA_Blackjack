
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SPAgame.Server.Data;
using SPAgame.Server.Services;
using Swashbuckle.AspNetCore.Filters;

namespace SPAgame.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SPAConnectionString")));

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddScoped<DeckService>();

            builder.Services.AddAuthorization();
            //activates identity APIs
            builder.Services.AddIdentityApiEndpoints<AppUser>()
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:5173")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    "oauth2",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    }
                );

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapIdentityApi<AppUser>();

            app.MapPost("/logout", async (SignInManager<AppUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
                
            })
            .RequireAuthorization();//runs only if user is logged in

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowSpecificOrigins");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}

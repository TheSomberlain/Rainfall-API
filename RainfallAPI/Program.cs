using Microsoft.OpenApi.Models;
using RainfallAPI.Extensions;
using RainfallAPI.Interfaces;
using RainfallAPI.Services;

namespace RainfallAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string url = Utils.Environment.GetFirstUrlFromLaunchSettings();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IRainfallDataService, RainfallDataService>();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rainfall Api",
                    Version = "1.0",
                    Description = "An API which provides rainfall reading data"
                });
                c.AddServer(new OpenApiServer
                {
                    Url = url,
                    Description = "Rainfall Api"
                });
                c.DocumentFilter<AddTagsDocumentFilter>();
                c.EnableAnnotations();
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rainfall Api V1.0");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
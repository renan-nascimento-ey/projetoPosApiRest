using Microsoft.EntityFrameworkCore;
using ProjetoFinalApi.Context;
using ProjetoFinalApi.Extensions;
using TinyHelpers.Json.Serialization;

namespace ProjetoFinalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
                    options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            //builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApiDbContext>(options => 
                        options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // tratamento de erro global
            app.ConfigureExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
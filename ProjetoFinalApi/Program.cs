using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using ProjetoFinalApi.Context;
using ProjetoFinalApi.DTOs.Mappings;
using ProjetoFinalApi.Extensions;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Models.Data.Validations;
using ProjetoFinalApi.Repository;
using ProjetoFinalApi.Repository.Interfaces;
using ProjetoFinalApi.Services;
using ProjetoFinalApi.Services.Configs;
using TinyHelpers.Json.Serialization;

namespace ProjetoFinalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // The following line enables Application Insights telemetry collection.
            builder.Services.AddApplicationInsightsTelemetry();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
                    options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddScoped<IValidator<Time>, TimeValidator>();
            builder.Services.AddScoped<IValidator<Jogador>, JogadorValidator>();
            builder.Services.AddScoped<IValidator<Transferencia>, TransferenciaValidator>();
            builder.Services.AddScoped<IValidator<Torneio>, TorneioValidator>();
            builder.Services.AddScoped<IValidator<Partida>, PartidaValidator>();
            builder.Services.AddScoped<IValidator<EventoPartida>, EventoPartidaValidator>();          

            //builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApiDbContext>(options => 
                        options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

            builder.Services.Configure<AzureServiceBusConfig>(builder.Configuration.GetSection("AzureServiceBus"));
            string serviceBusConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddAzureClients(builderAzureClient =>
            {
                builderAzureClient.AddServiceBusClient(builder.Configuration["AzureServiceBus:ConnectionString"]);
            });
            builder.Services.AddTransient<AzureServiceBusPublisher>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Tratamento de erro global
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
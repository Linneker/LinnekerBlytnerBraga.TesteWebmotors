using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using System.IO.Compression;
using System.Net;
using System.Text.Json.Serialization;
using TesteWebmotors.Api.Configurations;
using TesteWebmotors.Infraestrutura.Config;
using TesteWebmotors.InjecaoDependencia;
using TesteWebmotors.Dominio.Entidade;
using TesteWebmotors.Servico.Validadores;
using TesteWebmotors.Api.Configurations.Filtler;

namespace TesteWebmotors.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(_ => _.LowercaseUrls = true);

            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));


            services.AddDbContext<Context>(op => op.UseMySql(Configuration.GetConnectionString("MySqlLocal"), serverVersion)
            .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

            // Add services to the container.
            services.RegistrarDependencia();

            services.AddControllers()
                .AddFluentValidation(_ => {
                _.RegisterValidatorsFromAssemblyContaining<AnuncioWebmotorsValidador>();
                _.DisableDataAnnotationsValidation = true;
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }
        public void Configure(WebApplication app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            logger.CreateLogger($"log_testewebmotors_${DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}");
            app.UseResponseCompression();

            app.MapControllers();

        }

        internal static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.ClearProviders();
            builder.SetMinimumLevel(LogLevel.Trace);

            builder.AddNLog();
            builder.AddJsonConsole();
            builder.AddConsole();
        });
    }
}

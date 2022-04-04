using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using System.IO.Compression;
using System.Net;
using System.Text.Json.Serialization;
using TesteWebmotors.Web.Configurations;
using TesteWebmotors.Infraestrutura.Config;
using TesteWebmotors.InjecaoDependencia;

namespace TesteWebmotors.Web
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

            services.AddControllersWithViews()
                  .ConfigureApiBehaviorOptions(options => {
                      options.InvalidModelStateResponseFactory = context => {
                          
                          return new ObjectResult(new ErrorResponse("entity_validation", "The following inconsistencies were found in the sent entity",
                              "Model", new Exception("Model")))
                          {
                              StatusCode = (int)HttpStatusCode.UnprocessableEntity
                          };
                      };
                  })
               .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddEndpointsApiExplorer();

        }
        public void Configure(WebApplication app, IWebHostEnvironment env, ILoggerFactory logger)
        {

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=AnuncioWebmotors}/{action=Index}/{id?}");

            logger.CreateLogger($"log_testewebmotors_${DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}");
            app.UseResponseCompression();

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

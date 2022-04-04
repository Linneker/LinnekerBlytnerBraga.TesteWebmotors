using NLog.Web;
using TesteWebmotors.Web;

var builder = WebApplication.CreateBuilder(args);

var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
logger.Debug("Inicio");

var staurtup = new Startup(builder.Configuration);
staurtup.ConfigureServices(builder.Services);

// Add services to the container.

var app = builder.Build();

staurtup.Configure(app, app.Environment, Startup.loggerFactory);



app.Run();
logger.Debug("Fim");
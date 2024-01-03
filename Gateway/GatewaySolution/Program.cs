using GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using GatewaySolution.Logging;
using GatewaySolution.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppAuthetication();
if (builder.Environment.EnvironmentName.ToString().ToLower().Equals("production"))
{
    builder.Configuration.AddJsonFile("ocelot.Production.json", optional: false, reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
}
builder.Services.AddOcelot(builder.Configuration);

builder.Host.UseSerilog(Logging.ConfigureLogger);

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();
app.Run();
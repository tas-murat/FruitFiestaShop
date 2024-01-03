using MessageBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using ShoppingCart.API.Extensions;
using ShoppingCart.API.Logging;
using ShoppingCart.Application.Extensions;
using ShoppingCart.Infrastructure.Data;
using ShoppingCart.Infrastructure.Extensions;
using ShoppingCart.API.Middleware;
using MessageBus.ConfigurationModel;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ServiceBusConfiguration>(builder.Configuration.GetSection("AzureServiceBus"));
builder.Services.AddScoped<IMessageBus, AzureMessageBus>();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfraServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});
builder.AddAppAuthetication();

builder.Services.AddAuthorization();
builder.Host.UseSerilog(Logging.ConfigureLogger);
var app = builder.Build();

//app.MigrateDbContext<ShoppingCartDbContext>();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingCart.API v1"));
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

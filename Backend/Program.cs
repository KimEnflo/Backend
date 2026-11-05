using Backend.config;
using Backend.Services;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var app = BuildApp(args);
await app.RunAsync();
return;

static WebApplication BuildApp(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);

    ConfigureServices(builder);

    var app = builder.Build();

    ConfigureMiddleWare(app);
    ConfigureEndpoints(app);

    return app;
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;

    services.AddProblemDetails();
    services.AddControllers();
    services.AddScoped<SupportService>();
}




static void ConfigureEndpoints(WebApplication app)
{
    app.MapControllers();
}

static void ConfigureMiddleWare(WebApplication app)
{
    app.UseHttpsRedirection();
}
using Backend.config;
using Backend.Services;
using Microsoft.AspNetCore.Http.Json;

var app = BuildApp(args);
await app.RunAsync();

static WebApplication BuildApp(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    
    ConfigureMongo(builder);
    
    var app = builder.Build();
    
    return app;
}


static void ConfigureMongo(WebApplicationBuilder builder)
{
    builder.Services.Configure<MongoConfig>(
        builder.Configuration.GetSection("SupportInfoDatabase"));

    builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.PropertyNamingPolicy = null; });
}

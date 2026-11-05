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
    ConfigureMongo(builder);

    var app = builder.Build();
    
    app.UseCors("Frontend");
    app.UseExceptionHandler();
    
    ConfigureMiddleWare(app);
    ConfigureEndpoints(app);

    return app;
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    var services = builder.Services;
    
    services.AddCors(options =>
    {
        options.AddPolicy("Frontend", policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
    
    services.AddHealthChecks();
    services.AddProblemDetails();
    services.AddControllers();
    services.AddScoped<SupportService>();
}

static void ConfigureMongo(WebApplicationBuilder builder)
{
    builder.Services.Configure<MongoConfig>(
        builder.Configuration.GetSection("SupportInfoDatabase"));

    builder.Services.Configure<JsonOptions>(options => { options.SerializerOptions.PropertyNamingPolicy = null; });
    
    builder.Services.AddSingleton<IMongoClient>(sp =>
    {
        var config = sp.GetRequiredService<IOptions<MongoConfig>>().Value;
        
        return new MongoClient(config.ConnectionString);
    });

    builder.Services.AddSingleton<IMongoDatabase>(sp =>
    {
        var config = sp.GetRequiredService<IOptions<MongoConfig>>().Value;
        var client = sp.GetRequiredService<IMongoClient>();

        return client.GetDatabase(config.DatabaseName);
    });
}


static void ConfigureEndpoints(WebApplication app)
{
    app.MapHealthChecks("/health");
    app.MapControllers();
}

static void ConfigureMiddleWare(WebApplication app)
{
    app.UseHttpsRedirection();
}
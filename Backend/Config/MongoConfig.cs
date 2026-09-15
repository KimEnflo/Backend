namespace Backend.config;

public class MongoConfig
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ContactCollectionName { get; set; } = null!;
    
}
namespace Backend.config;

/// <summary>
/// Config file for connecting to the database.
/// </summary>
public class MongoConfig
{
    /// <summary>
    /// Mongo DB connection String.
    /// </summary>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    /// The name of the database.
    /// </summary>
    public string DatabaseName { get; set; } = null!;

    /// <summary>
    /// The collection name in the database.
    /// </summary>
    public string ContactCollectionName { get; set; } = null!;
    
}
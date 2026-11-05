using Backend.config;
using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services;
/// <summary>
/// Service class used by ContactInfoController to provide REST functionality
/// </summary>
/// <param name="database"> The database</param>
/// <param name="config">Configuration for the database</param>
public class SupportService(IMongoDatabase database,IOptions<MongoConfig> config)
{
    private readonly IMongoCollection<ContactInfo> _contactInfoCollection =
        database.GetCollection<ContactInfo>(
            config.Value.ContactCollectionName);
    
    /// <summary>
    /// Retrieves all ContactInfo objects from the database. 
    /// </summary>
    /// <returns>
    /// All objects in the database
    /// </returns> 
    public async Task<List<ContactInfo>> GetAsync() =>
        await _contactInfoCollection.Find(_ => true).ToListAsync();

}
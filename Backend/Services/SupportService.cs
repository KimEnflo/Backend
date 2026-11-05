using Backend.config;
using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Services;

/// <summary>
///     Service class used by ContactInfoController to provide REST functionality
/// </summary>
/// <param name="database"> The database</param>
/// <param name="config">Configuration for the database</param>
public class SupportService(IMongoDatabase database, IOptions<MongoConfig> config)
{
    private readonly IMongoCollection<ContactInfo> _contactInfoCollection =
        database.GetCollection<ContactInfo>(
            config.Value.ContactCollectionName);

    /// <summary>
    ///     Retrieves all ContactInfo objects from the database.
    /// </summary>
    /// <returns>
    ///     All objects in the database
    /// </returns>
    public async Task<List<ContactInfo>> GetAsync()
    {
        return await _contactInfoCollection.Find(_ => true).ToListAsync();
    }

    /// <summary>
    ///     Retrieves the specified company by name from the database.
    /// </summary>
    /// <param name="companyName">Name of the company to retrieve</param>
    /// <returns>Object containing all the company support information</returns>
    public async Task<ContactInfo?> GetAsync(string companyName)
    {
        return await _contactInfoCollection.Find(x => x.Name == companyName).FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Creates a new object to insert into the database.
    /// </summary>
    /// <param name="newInfo">Object containing all the information of the company to insert</param>
    public async Task CreateAsync(ContactInfo newInfo)
    {
        await _contactInfoCollection.InsertOneAsync(newInfo);
    }


    /// <summary>
    ///     Updated the information of the specified company's information.
    /// </summary>
    /// <param name="id">id of the entry to update</param>
    /// <param name="updatedInfo">The new information to insert into the database</param>
    public async Task<ReplaceOneResult?> UpdateAsync(string id, ContactInfo updatedInfo)
    {
        var existingInfo = await _contactInfoCollection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
        if (existingInfo == null) return null;
        var replacement = new ContactInfo
        {
            Id = existingInfo.Id,
            Name = updatedInfo.Name,
            PhoneNumber = updatedInfo.PhoneNumber,
            Email = updatedInfo.Email,
            Website = updatedInfo.Website,
            SupportHours = updatedInfo.SupportHours
        };

        return await _contactInfoCollection.ReplaceOneAsync(
            x => x.Id == id,
            replacement);
    }
}
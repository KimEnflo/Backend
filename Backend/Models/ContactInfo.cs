using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

///
/// Model used when interacting with the database
/// 
[BsonIgnoreExtraElements]
public class ContactInfo
{
    /// <summary>
    /// Default ID.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }
    
    /// <summary>
    /// Region field for potential expansion to more regions later on.
    /// </summary>
    [BsonElement("region")]
    [JsonPropertyName("region")]
    public string Region { get; init; } = null!;
    
    /// <summary>
    /// Name of the company.
    /// </summary>
    [BsonElement("name")]
    [JsonPropertyName("name")]
    [BsonRequired]
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Support number to the company.
    /// </summary>
    [BsonElement("phonenumber")]
    [JsonPropertyName("phonenumber")] 
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Support email to the company.
    /// </summary>
    [BsonElement("email")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;
    
    /// <summary>
    /// Support website to the company.
    /// </summary>
    [BsonElement("website")]
    [JsonPropertyName("website")]
    public string Website { get; set; } = null!;
    
    /// <summary>
    /// Time of the day the support number is open for use.
    /// </summary>
    [BsonElement("supporthours")]
    [JsonPropertyName("supporthours")]
    public SupportHours SupportHours { get; set; } = new();
}
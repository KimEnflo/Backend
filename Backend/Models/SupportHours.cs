using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

///
/// Model used by the ContactInfo model.
/// 
public class SupportHours
{
    /// <summary>
    /// Object for the day containing the opening and closing hours.
    /// </summary>
    [BsonElement("monday")]
    [JsonPropertyName("monday")]
    public OpeningHours? Monday { get; set; }
    
    /// <inheritdoc cref="Monday"/>
    [BsonElement("tuesday")]
    [JsonPropertyName("tuesday")]
    public OpeningHours? Tuesday { get; set; }
    
    /// <inheritdoc cref="Monday"/>
    [BsonElement("wednesday")]
    [JsonPropertyName("wednesday")]
    public OpeningHours? Wednesday { get; set; }
    
    /// <inheritdoc cref="Monday"/>
    [BsonElement("thursday")]
    [JsonPropertyName("thursday")]
    public OpeningHours? Thursday { get; set; }
    
    /// <inheritdoc cref="Monday"/>
    [BsonElement("friday")]
    [JsonPropertyName("friday")]
    public OpeningHours? Friday { get; set; }
    
    /// <inheritdoc cref="Monday"/>
    [BsonElement("saturday")]
    [JsonPropertyName("saturday")]
    public OpeningHours? Saturday { get; set; }
    
    /// <inheritdoc cref="Monday"/>
    [BsonElement("sunday")]
    [JsonPropertyName("sunday")]
    public OpeningHours? Sunday { get; set; }
}
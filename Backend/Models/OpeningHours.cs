using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

///
/// Model used by the SupportHours model.
/// 
public class OpeningHours
{
    /// <summary>
    /// Opening hours for the day.
    /// </summary>
    [BsonElement("open")]
    [JsonPropertyName("open")]
    public string Open { get; set; } = string.Empty;

    /// <summary>
    /// Closing hours for the day.
    /// </summary>
    [BsonElement("close")]
    [JsonPropertyName("close")]
    public string Close { get; set; } = string.Empty;
}

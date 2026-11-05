using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

public class OpeningHours
{
    [BsonElement("open")]
    [JsonPropertyName("open")]
    public string Open { get; set; } = string.Empty;

    [BsonElement("close")]
    [JsonPropertyName("close")]
    public string Close { get; set; } = string.Empty;
}

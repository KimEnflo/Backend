using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;

public class SupportHours
{
    [BsonElement("monday")]
    [JsonPropertyName("monday")]
    public OpeningHours? Monday { get; set; }

    [BsonElement("tuesday")]
    [JsonPropertyName("tuesday")]
    public OpeningHours? Tuesday { get; set; }

    [BsonElement("wednesday")]
    [JsonPropertyName("wednesday")]
    public OpeningHours? Wednesday { get; set; }

    [BsonElement("thursday")]
    [JsonPropertyName("thursday")]
    public OpeningHours? Thursday { get; set; }

    [BsonElement("friday")]
    [JsonPropertyName("friday")]
    public OpeningHours? Friday { get; set; }

    [BsonElement("saturday")]
    [JsonPropertyName("saturday")]
    public OpeningHours? Saturday { get; set; }

    [BsonElement("sunday")]
    [JsonPropertyName("sunday")]
    public OpeningHours? Sunday { get; set; }
}
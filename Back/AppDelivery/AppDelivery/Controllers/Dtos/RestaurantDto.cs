using System.Text.Json.Serialization;

public class RestaurantDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("distance")]
    public double Distance { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }
}
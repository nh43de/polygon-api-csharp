using System.Text.Json.Serialization;

namespace PolygonApi.Models.Daily;

public class DailyOpenCloseResponse
{
    [JsonPropertyName("afterHours")]
    public double AfterHours { get; set; }

    [JsonPropertyName("close")]
    public double Close { get; set; }

    [JsonPropertyName("from")]
    public string From { get; set; }

    [JsonPropertyName("high")]
    public double High { get; set; }

    [JsonPropertyName("low")]
    public double Low { get; set; }

    [JsonPropertyName("open")]
    public double Open { get; set; }

    [JsonPropertyName("preMarket")]
    public double PreMarket { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("volume")]
    public long Volume { get; set; }
}
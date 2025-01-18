using System.Text.Json.Serialization;
using PolygonApi.Converters;

namespace PolygonApi.Models.Aggregates;

public class AggregateResult
{
    [JsonPropertyName("T")]
    public string Ticker { get; set; }

    [JsonPropertyName("c")]
    public double Close { get; set; }

    [JsonPropertyName("h")]
    public double High { get; set; }

    [JsonPropertyName("l")]
    public double Low { get; set; }

    [JsonPropertyName("o")]
    public double Open { get; set; }

    [JsonPropertyName("t")]
    [JsonConverter(typeof(UnixTimestampConverter))]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("v")]
    public double Volume { get; set; }

    [JsonPropertyName("vw")]
    public double VWAP { get; set; }
}
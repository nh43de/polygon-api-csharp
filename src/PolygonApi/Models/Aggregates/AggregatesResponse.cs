using System.Text.Json.Serialization;

namespace PolygonApi.Models.Aggregates;

public class AggregatesResponse
{
    [JsonPropertyName("adjusted")]
    public bool Adjusted { get; set; }

    [JsonPropertyName("queryCount")]
    public int QueryCount { get; set; }

    [JsonPropertyName("request_id")]
    public string RequestId { get; set; }

    [JsonPropertyName("results")]
    public List<AggregateResult> Results { get; set; }

    [JsonPropertyName("resultsCount")]
    public int ResultsCount { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("ticker")]
    public string Ticker { get; set; }
}
namespace PolygonApi.Models.Financials;

public class FinancialsResponse
{
    public int Count { get; set; } // Total number of results
    public string RequestId { get; set; } // A unique request ID
    public string NextUrl { get; set; } // URL for the next page, if applicable
    public List<FinancialResult> Results { get; set; } // List of financial results
}
namespace PolygonApi.Models.Financials;

public class FinancialItem
{
    public string Label { get; set; } // Human-readable label
    public string Unit { get; set; } // Unit of measurement (e.g., USD)
    public double Value { get; set; } // Numeric value of the item
}
namespace PolygonApi.Models.Financials;

public class FinancialResult
{
    public string Ticker { get; set; } // The stock ticker (e.g., "AAPL")
    public string CIK { get; set; } // Central Index Key
    public string CompanyName { get; set; } // The company name
    public string FilingDate { get; set; } // Date of filing
    public string FiscalYear { get; set; } // Fiscal year of the report
    public string FiscalPeriod { get; set; } // Fiscal period (e.g., Q1, Q2, etc.)
    public string StartDate { get; set; } // Start date of the reporting period
    public string EndDate { get; set; } // End date of the reporting period
    public FinancialData Financials { get; set; } // Financial data
}
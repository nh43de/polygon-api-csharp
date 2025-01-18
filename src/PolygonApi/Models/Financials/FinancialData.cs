namespace PolygonApi.Models.Financials;

public class FinancialData
{
    public Dictionary<string, FinancialItem> BalanceSheet { get; set; }
    public Dictionary<string, FinancialItem> IncomeStatement { get; set; }
    public Dictionary<string, FinancialItem> CashFlowStatement { get; set; }
}
using PolygonApi.Models.Aggregates;
using PolygonApi.Models.Daily;
using PolygonApi.Models.Financials;
using Refit;

namespace PolygonApi;

public interface IPolygonApi
{
    [Get("/v1/open-close/{stocksTicker}/{date}")]
    Task<DailyOpenCloseResponse> GetDailyOpenCloseAsync(
        [AliasAs("stocksTicker")] string stocksTicker,
        [AliasAs("date")] string date,
        [Query] bool adjusted = true);

    [Get("/vX/reference/financials")]
    Task<FinancialsResponse> GetFinancialsAsync([Query] string ticker, [Query] int limit = 10);

    [Get("/v2/aggs/ticker/{stocksTicker}/range/{multiplier}/{timespan}/{from}/{to}")]
    Task<AggregatesResponse> GetAggregatesAsync(
        [AliasAs("stocksTicker")] string stocksTicker,
        [AliasAs("multiplier")] int multiplier,
        [AliasAs("timespan")] string timespan,
        [AliasAs("from")] string from,
        [AliasAs("to")] string to,
        [Query] bool adjusted = true,
        [Query] string sort = "asc",
        [Query] int limit = 5000);
}
// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using PolygonApi;

Console.WriteLine("Hello, World!");


var services = new ServiceCollection();

// Register the Polygon API client with the API key and base URL
services.AddPolygonApiClient("https://api.polygon.io", "your_api_key_here");

var serviceProvider = services.BuildServiceProvider();
var polygonApi = serviceProvider.GetRequiredService<IPolygonApi>();

// Example usage
var response = await polygonApi.GetDailyOpenCloseAsync("AAPL", "2023-01-09");
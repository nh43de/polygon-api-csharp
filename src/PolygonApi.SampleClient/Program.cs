// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PolygonApi;
using PolygonApi.Extensions;

Console.WriteLine("Hello, World!");

var logServices = new ServiceCollection();

// Add logging
logServices.AddLogging(builder =>
{
    builder.AddConsole(); // Add console logging
});

var logger = logServices.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

var services = new ServiceCollection();

// Add logging
services.AddLogging(builder =>
{
    builder.AddConsole(); // Add console logging
});

// Register the Polygon API client with the API key and base URL
services.AddPolygonApiClient("", true, logger);

var serviceProvider = services.BuildServiceProvider();
var polygon = serviceProvider.GetRequiredService<PolygonApiService>();

var polygonApi = polygon.PolygonApi;

// Example usage
var response = await polygonApi.GetDailyOpenCloseAsync("AAPL", "2023-01-09");


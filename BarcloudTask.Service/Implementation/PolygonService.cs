using BarcloudTask.DataBase.Models;
using BarcloudTask.DTO.DTOs;
using BarcloudTask.Service.Interface;
using Newtonsoft.Json;

namespace BarcloudTask.Service.Implementation;

public class PolygonService : IPolygonService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public PolygonService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<StockData> GetMarketDataAsync(string symbol)
    {
        long fromUnix = ((DateTimeOffset)DateTime.UtcNow.AddHours(-6)).ToUnixTimeMilliseconds();
        long toUnix = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

        var response = await _httpClient.GetAsync($"/aggs/ticker/{symbol}/range/6/hour/{fromUnix}/{toUnix}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var marketDataResponse = JsonConvert.DeserializeObject<MarketDataResponse>(content);

            var stockDataList = new StockData()
            {
                Ticker = symbol,
                Results = []
            };
            if (marketDataResponse != null)
            {
                foreach (var result in marketDataResponse.Results)
                {
                    stockDataList.Results.Add(new StockDataResults
                    {
                        Symbol = symbol,
                        Open = result.O,
                        High = result.H,
                        Low = result.L,
                        Close = result.C,
                        Volume = result.V,
                        Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(result.T).UtcDateTime
                    });
                }
            }
            return stockDataList;
        }
        else
        {
            throw new Exception("Failed to fetch data from Polygon.io");
        }
    }
}

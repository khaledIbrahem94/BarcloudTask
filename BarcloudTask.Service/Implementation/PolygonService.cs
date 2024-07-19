using BarcloudTask.DataBase.Models;
using BarcloudTask.DTO.DTOs;
using BarcloudTask.Service.Interface;
using Newtonsoft.Json;

namespace BarcloudTask.Service.Implementation;

public class PolygonService(HttpClient _httpClient) : IPolygonService
{
    public async Task<StockData> GetMarketDataAsync(string symbol)
    {
        //Get Last Year to Avoid Premium Upgrade
        long fromUnix = ((DateTimeOffset)DateTime.UtcNow.AddHours(-6).AddYears(-1)).ToUnixTimeMilliseconds();
        long toUnix = ((DateTimeOffset)DateTime.UtcNow.AddYears(-1)).ToUnixTimeMilliseconds();

        var response = await _httpClient.GetAsync($"/v2/aggs/ticker/{symbol}/range/6/hour/{fromUnix}/{toUnix}");

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

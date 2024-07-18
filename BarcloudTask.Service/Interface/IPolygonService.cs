using BarcloudTask.DataBase.Models;

namespace BarcloudTask.Service.Interface;

public interface IPolygonService
{
    Task<StockData> GetMarketDataAsync(string symbol);
}

using BarcloudTask.DataBase.Models;
using BarcloudTask.Service.Interface;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace BarcloudTask.Service.Implementation;

public class FetchDataAsync
{
    private readonly IServiceProvider _serviceProvider;

    public FetchDataAsync(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task FetchData()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var polygonService = scope.ServiceProvider.GetRequiredService<IPolygonService>();
            var stockDataRepo = scope.ServiceProvider.GetRequiredService<IRepository<StockData>>();
            var stockSymboleRepo = scope.ServiceProvider.GetRequiredService<IRepository<StockSymbol>>();
            var errorLog = scope.ServiceProvider.GetRequiredService<IRepository<ErrorsLog>>();

            var tasks = new List<Task>();
            List<StockData> stockDatas = new();
            Parallel.ForEach(await stockSymboleRepo.GetAllAsync(), symbol =>
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var data = await polygonService.GetMarketDataAsync(symbol.Symbol);
                        stockDatas.Add(data);
                    }
                    catch (Exception ex)
                    {
                        await errorLog.InsertAsync(new ErrorsLog { Message = $"Error fetching data for {symbol}: {ex.Message}", Function = "FetchData" }).ConfigureAwait(false);
                    }
                }));
            });

            await Task.WhenAll(tasks);

            BackgroundJob.Enqueue(() => stockDataRepo.InsertAsync(stockDatas));
            BackgroundJob.Enqueue(() => SendEmail(stockDatas));
        }
    }
    void SendEmail(List<StockData> data)
    {

    }
}

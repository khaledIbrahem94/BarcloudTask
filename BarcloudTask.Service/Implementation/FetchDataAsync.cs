using BarcloudTask.DataBase.Models;
using BarcloudTask.Service.Interface;
using Hangfire;
using Microsoft.Extensions.Configuration;
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
            var clientRepo = scope.ServiceProvider.GetRequiredService<IRepository<Client>>();

            List<string> emails = (await clientRepo.GetAllAsync()).Select(x => x.Email).ToList();
            if (emails.Any())
            {
                var polygonService = scope.ServiceProvider.GetRequiredService<IPolygonService>();
                var stockDataRepo = scope.ServiceProvider.GetRequiredService<IRepository<StockData>>();
                var stockSymboleRepo = scope.ServiceProvider.GetRequiredService<IRepository<StockSymbol>>();

                var tasks = new List<Task>();
                List<StockData> stockDatas = new();

                Parallel.ForEach(await stockSymboleRepo.GetAllAsync(), symbol =>
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        var data = await polygonService.GetMarketDataAsync(symbol.Symbol);
                        stockDatas.Add(data);
                    }));
                });

                await Task.WhenAll(tasks);

                BackgroundJob.Enqueue(() => stockDataRepo.InsertAsync(stockDatas));
                BackgroundJob.Enqueue(() => SendEmail(stockDatas, emails));
            }



        }
    }
    async Task SendEmail(List<StockData> data, List<string> emails)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var html = new System.Text.StringBuilder();

            var email = scope.ServiceProvider.GetRequiredService<IEmailSender>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            foreach (var tickerData in data)
            {
                html.AppendLine($"<h2>{tickerData.Ticker}</h2>");
                html.AppendLine("<table>");
                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th>Timestamp</th>");
                html.AppendLine("<th>Open</th>");
                html.AppendLine("<th>High</th>");
                html.AppendLine("<th>Low</th>");
                html.AppendLine("<th>Close</th>");
                html.AppendLine("<th>Volume</th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");
                html.AppendLine("<tbody>");

                foreach (var res in tickerData.Results ?? [])
                {
                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{res.Timestamp}</td>");
                    html.AppendLine($"<td>{res.Open}</td>");
                    html.AppendLine($"<td>{res.High}</td>");
                    html.AppendLine($"<td>{res.Low}</td>");
                    html.AppendLine($"<td>{res.Close}</td>");
                    html.AppendLine($"<td>{res.Volume}</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
            }

            await email.SendEmailWithCCAsync(new() { configuration["ReviewerEmail:Emails"] ?? "" }, "Polygon Symbols Results", html.ToString(), [], emails, []);
        }

    }
}

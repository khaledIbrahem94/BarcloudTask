using BarcloudTask.Service.Implementation;
using Hangfire;

namespace BarcloudTask.API.Extensions;

public static class HangFireService
{
    public static IHostApplicationBuilder AddHandFire(this IHostApplicationBuilder builder, string? connectionString)
    {
        builder.Services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
        builder.Services.AddHangfireServer();

        return builder;
    }

    public static void UseHangFire(IServiceProvider serviceProvider)
    {
        var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
        var job = new FetchDataAsync(serviceProvider);
        recurringJobManager.AddOrUpdate("FetchPolygonData", () => job.FetchData(), "0 */6 * * *"); // Every 6 hours
    }
}

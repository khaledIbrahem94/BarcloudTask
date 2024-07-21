using BarcloudTask.Service.Implementation;
using Hangfire;

namespace BarcloudTask.API.Extensions;

public static class HangFireService
{
    public static IHostApplicationBuilder AddHandFire(this IHostApplicationBuilder builder, string? connectionString)
    {
        builder.Services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
        builder.Services.AddHangfireServer();
        builder.Services.AddSingleton<HangfireExceptionFilter>();
        return builder;
    }

    public static void UseHangFire(IServiceProvider serviceProvider)
    {
        GlobalJobFilters.Filters.Add(serviceProvider.GetRequiredService<HangfireExceptionFilter>());

        var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
        var job = new FetchDataAsync(serviceProvider);
        recurringJobManager.AddOrUpdate("FetchPolygonData", () => job.FetchData(), "0 */6 * * *");
        // Every 1 min for test  "*/1 * * * *"
    }
}

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
        recurringJobManager.AddOrUpdate("FetchPolygonData", () => job.FetchData(), "*/1 * * * *");
        // Every 6 hours "0 */6 * * *"
    }
}

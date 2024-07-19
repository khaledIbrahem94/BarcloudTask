
using BarcloudTask.Service.Implementation;
using BarcloudTask.Service.Interface;
using System.Net.Http.Headers;
namespace BarcloudTask.API.Extensions
{
    public static class DependencyInjectionService
    {
        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder, ConfigurationManager configuration)
        {
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddSingleton<ICommonService, CommonService>();

            builder.Services.AddScoped<IClientsService, ClientsService>();
            builder.Services.AddScoped<IPolygonService, PolygonService>();


            string apiKey = configuration["PolygonSettings:ApiKey"] ?? "";
            builder.Services.AddHttpClient<IPolygonService, PolygonService>(client =>
            {
                client.BaseAddress = new Uri("https://api.polygon.io");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            });

            return builder;
        }
    }
}

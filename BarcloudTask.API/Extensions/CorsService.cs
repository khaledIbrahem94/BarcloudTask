namespace BarcloudTask.API.Extensions
{
    public static class CorsService
    {
        public static IHostApplicationBuilder AddCorsService(this IHostApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                IConfigurationSection CorsWebsite = builder.Configuration.GetSection("AllowOrigin");
                string[] itemArray = (CorsWebsite.AsEnumerable().ToList().Where(x => !string.IsNullOrEmpty(x.Value)).
                Select(x => x.Value ?? "")?.ToArray()) ?? [];

                options.AddPolicy("AllowOrigin", builder => builder
                               .WithOrigins(itemArray)
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials());
            });
            return builder;
        }
    }
}

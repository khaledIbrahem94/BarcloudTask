using BarcloudTask.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BarcloudTask.API.Extensions
{
    public static class DBConnection
    {
        public static IHostApplicationBuilder AddDBConnection(this IHostApplicationBuilder builder, string? connectionString)
        {
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(connectionString));

            return builder;
        }
    }
}

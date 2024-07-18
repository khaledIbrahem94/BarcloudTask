using BarcloudTask.API.Extensions;
using BarcloudTask.DataBase;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

namespace BarcloudTask.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddControllers();


            builder.AddDBConnection(connectionString);
            builder.AddHandFire(connectionString);
            builder.AddCrosService();
            builder.AddEmailService();
            builder.AddAutoMapper();
            builder.AddDependencyInjection(builder.Configuration);

            var app = builder.Build();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors("AllowOrigin");

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            HangFireService.UseHangFire(app.Services);

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();
                dbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}

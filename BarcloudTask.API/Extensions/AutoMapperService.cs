using BarcloudTask.API.Extensions;
using BarcloudTask.Service.AutoMapper;

namespace BarcloudTask.API.Extensions;

public static class AutoMapperService
{
    public static IHostApplicationBuilder AddAutoMapper(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        return builder;
    }
}

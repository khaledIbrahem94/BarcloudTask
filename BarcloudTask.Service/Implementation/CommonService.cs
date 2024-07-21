using BarcloudTask.Core;
using BarcloudTask.DataBase.Models;
using BarcloudTask.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace BarcloudTask.Service.Implementation;

public class CommonService(IServiceProvider _serviceProvider) : ICommonService
{
    public SaveAction Fail(string Msg = "Fail")
    {
        return new SaveAction
        {
            Success = false,
            Message = Msg
        };
    }

    public ResultListDTO<T> ResultList<T>(int Total, IQueryable<T> List, GridRequestParamters GridParamters)
    {
        return new ResultListDTO<T>
        {

            Data = List,
            Total = Total,
        };
    }

    public SaveAction Success(string Msg = "Done")
    {
        return new SaveAction
        {
            Success = true,
            Message = Msg
        };
    }
    public async Task AddError(ErrorsLog error)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            error.Date = DateTime.UtcNow;
            var repoErrorLog = scope.ServiceProvider.GetRequiredService<IRepository<ErrorsLog>>();
            await repoErrorLog.InsertAsync(error);
        }
    }
}

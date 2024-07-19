using BarcloudTask.Core;
using BarcloudTask.DataBase.Models;

namespace BarcloudTask.Service.Interface;

public interface ICommonService
{
    SaveAction Success(string Msg = "Done");
    SaveAction Fail(string Msg = "Fail");
    ResultListDTO<T> ResultList<T>(int Total, IQueryable<T> List, GridParamters GridParamters);
    Task AddError(ErrorsLog error);
}

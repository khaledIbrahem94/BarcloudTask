using BarcloudTask.Core;

namespace BarcloudTask.Service.Interface;

public interface ICommonService
{
    SaveAction Success(string Msg = "Done");
    SaveAction Fail(string Msg = "Fail");
    ResultListDTO<T> ResultList<T>(int Total, List<T> List, GridParamters GridParamters);
    ResultListDTO<T> ResultList<T>(List<T> List);
}

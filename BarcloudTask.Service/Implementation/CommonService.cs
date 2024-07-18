using BarcloudTask.Core;
using BarcloudTask.Service.Interface;

namespace BarcloudTask.Service.Implementation;

public class CommonService : ICommonService
{
    public SaveAction Fail(string Msg = "Fail")
    {
        return new SaveAction
        {
            Success = false,
            Message = Msg
        };
    }

    public ResultListDTO<T> ResultList<T>(int Total, List<T> List, GridParamters GridParamters)
    {
        return new ResultListDTO<T>
        {

            List = List,
            ResultPaging = new GridResult
            {
                PageRows = List.Count,
                TotalRows = Total,
                NumberOfPages = Total / GridParamters.RowsNumber,
            }
        };
    }

    public ResultListDTO<T> ResultList<T>(List<T> List)
    {
        throw new NotImplementedException();
    }

    public SaveAction Success(string Msg = "Done")
    {
        return new SaveAction
        {
            Success = true,
            Message = Msg
        };
    }
}

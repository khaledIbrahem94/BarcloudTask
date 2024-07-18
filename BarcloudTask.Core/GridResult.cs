namespace BarcloudTask.Core;

public class GridResult
{
    //count of only coming records
    public int PageRows { get; set; }
    // count of all records 
    public int TotalRows { get; set; }
    public int NumberOfPages { get; set; }
}

public class ResultListDTO<T>
{
    public required List<T> List { get; set; }
    public required GridResult ResultPaging { get; set; }
}
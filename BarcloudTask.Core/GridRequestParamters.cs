namespace BarcloudTask.Core;

public class GridRequestParamters
{
    public string? Filter { get; set; } = "";
    public string? OrderBy { get; set; } = "Id";
    public int Take { get; set; } = 10;
    public int Skip { get; set; } = 0;
    public SortDirection OrderEnum { get; set; } = SortDirection.asc;
}

namespace BarcloudTask.Core;

public class GridParamters
{
    public string? Filter { get; set; } = "";
    public string? OrderBy { get; set; } = "Id";
    public int RowsNumber { get; set; } = 10;
    public int Skip { get; set; } = 0;
    public int PageIndex { get; set; } = 1;
    public SortDirection OrderEnum { get; set; } = SortDirection.asc;
}

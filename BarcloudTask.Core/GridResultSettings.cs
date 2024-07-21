namespace BarcloudTask.Core;


public class ResultListDTO<T>
{
    public required IQueryable<T> Data { get; set; }
    public int Total { get; set; }
}
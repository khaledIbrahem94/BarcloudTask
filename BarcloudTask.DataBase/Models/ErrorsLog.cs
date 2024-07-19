namespace BarcloudTask.DataBase.Models;

public class ErrorsLog
{
    public int Id { get; set; }
    public required string Message { get; set; }
    public required string Function { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}

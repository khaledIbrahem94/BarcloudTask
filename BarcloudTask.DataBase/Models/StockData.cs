using System.ComponentModel.DataAnnotations;

namespace BarcloudTask.DataBase.Models;

public class StockData
{
    [Key]
    public int Id { get; set; }
    public required string Ticker { get; set; }
    public int? QueryCount { get; set; }
    public int? ResultsCount { get; set; }
    public bool? Adjusted { get; set; }
    public string? Status { get; set; }
    public string? Request_id { get; set; }
    public int? Count { get; set; }
    public string? Error { get; set; }
    public virtual ICollection<StockDataResults>? Results { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarcloudTask.DataBase.Models;

public class StockDataResults
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Symbol { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public long Volume { get; set; }
    public DateTime Timestamp { get; set; }
    [ForeignKey(nameof(StockData))]
    public int StockDataId { get; set; }
    public virtual StockData? StockData { get; set; }
}

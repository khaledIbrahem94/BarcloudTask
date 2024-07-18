using System.ComponentModel.DataAnnotations;

namespace BarcloudTask.DataBase.Models;

public class StockSymbol
{
    [Key]
    public int Id { get; set; }
    public required string Symbol { get; set; }
}

namespace BarcloudTask.DTO.DTOs;

public class MarketDataResponse
{
    public required string Ticker { get; set; }
    public int? QueryCount { get; set; }
    public int? ResultsCount { get; set; }
    public bool? Adjusted { get; set; }
    public string? Status { get; set; }
    public string? Request_id { get; set; }
    public int? Count { get; set; }
    public string? Error { get; set; }
    public required List<MarketDataDTO> Results { get; set; }
}
public class MarketDataDTO
{
    public long T { get; set; } // Unix timestamp in milliseconds
    public decimal O { get; set; } // Open price
    public decimal H { get; set; } // High price
    public decimal L { get; set; } // Low price
    public decimal C { get; set; } // Close price
    public long V { get; set; } // Volume
}

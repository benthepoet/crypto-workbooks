using System.Text.Json.Serialization;

namespace CryptoWorkbooks.Services.Models;

public class MarketData
{
    [JsonPropertyName("current_price")]
    public Dictionary<string, decimal> CurrentPrice { get; set; } = null!;
}
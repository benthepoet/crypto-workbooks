using System.Text.Json.Serialization;

namespace CryptoWorkbooks.Services.Models;

public class CoinHistory
{
    [JsonPropertyName("market_data")]
    public MarketData MarketData { get; set; } = null!;
}

using Microsoft.EntityFrameworkCore;
using CryptoWorkbooks.Data;
using CryptoWorkbooks.Services.Models;
using CryptoWorkbooks.Data.Models;

namespace CryptoWorkbooks.Services;

public class PriceService
{
    private readonly Context _context;
    private readonly HttpClient _httpClient;

    private readonly Dictionary<string, string> _idCache = new();

    public PriceService(Context context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.coingecko.com");
    }

    public async Task<SymbolPrice> GetSymbolPrice(string symbolName, DateTime date)
    {
        var symbolPrice = await _context.SymbolPrice
            .Include(x => x.Symbol)
            .FirstOrDefaultAsync(x => x.Symbol!.Name == symbolName && x.SnapshotAt == date);

        if (symbolPrice == null)
        {
            var symbol = await _context.Symbol.FirstAsync(x => x.Name == symbolName.ToUpper());

            if (_idCache.Keys.Count == 0)
            {
                await this.GetCoinList();
            }

            if (!_idCache.TryGetValue(symbolName, out var coinId))
            {
                throw new Exception("Unable to find coin ID.");
            }

            var dateString = date.ToString("dd-MM-yyyy");
            var coinHistory = await _httpClient.GetFromJsonAsync<CoinHistory>($"api/v3/coins/{coinId}/history?date={dateString}&localization=false");
            if (coinHistory == null)
            {
                throw new Exception("Unable to get coin history.");
            }

            symbolPrice = new SymbolPrice
            {
                SnapshotAt = date,
                Symbol = symbol,
                UsdPrice = coinHistory.MarketData.CurrentPrice["usd"]
            };

            _context.Add(symbolPrice);
            await _context.SaveChangesAsync();
        }

        return symbolPrice;
    }

    private async Task GetCoinList()
    {
        var coinList = await _httpClient.GetFromJsonAsync<CoinListItem[]>("api/v3/coins/list");
        if (coinList == null)
        {
            return;
        }

        foreach (var coin in coinList)
        {
            _idCache[coin.Symbol] = coin.Id;
        }
    }
}

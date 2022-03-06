using CryptoWorkbooks.Data;
using CryptoWorkbooks.Data.Models;
using CryptoWorkbooks.Resources;
using CryptoWorkbooks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoWorkbooks.Controllers;

[ApiController]
[Route("withdrawals")]
public class WithdrawalsController : ControllerBase
{
    private readonly Context _context;
    private readonly PriceService _priceService;

    public WithdrawalsController(Context context, PriceService priceService)
    {
        _context = context;
        _priceService = priceService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateWithdrawalRequest request)
    {
        var symbol = await _context.Symbol.FirstOrDefaultAsync(x => x.Name == request.Symbol.ToUpper());
        if (symbol == null)
        {
            symbol = new Symbol { Name = request.Symbol.ToUpper() };
            _context.Add(symbol);
            await _context.SaveChangesAsync();
        }

        var usdProceeds = request.UsdProceeds;
        if (usdProceeds == null)
        {
            var symbolPrice = await _priceService.GetSymbolPrice(request.Symbol, request.PerformedAt.Date);
            usdProceeds = request.Amount * symbolPrice.UsdPrice;
        }

        var withdrawal = new Withdrawal
        {
            WithdrawalTypeId = request.WithdrawalType,
            Symbol = symbol,
            Amount = request.Amount,
            UsdProceeds = usdProceeds.Value,
            PerformedAt = request.PerformedAt,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Add(withdrawal);
        await _context.SaveChangesAsync();

        return this.Accepted();
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var withdrawals = await _context.Withdrawal
            .Include(x => x.WithdrawalType)
            .Include(x => x.Symbol)
            .Include(x => x.ProceedsSymbol)
            .ToListAsync();

        return this.Ok(withdrawals);
    }
}

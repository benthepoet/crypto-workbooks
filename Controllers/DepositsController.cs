using CryptoWorkbooks.Data;
using CryptoWorkbooks.Data.Models;
using CryptoWorkbooks.Resources;
using CryptoWorkbooks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoWorkbooks.Controllers;

[ApiController]
[Route("deposits")]
public class DepositsController : ControllerBase
{
    private readonly Context _context;
    private readonly PriceService _priceService;

    public DepositsController(Context context, PriceService priceService)
    {
        _context = context;
        _priceService = priceService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateDepositRequest request)
    {
        var symbol = await _context.Symbol.FirstOrDefaultAsync(x => x.Name == request.Symbol.ToUpper());
        if (symbol == null)
        {
            symbol = new Symbol { Name = request.Symbol.ToUpper() };
            _context.Add(symbol);
            await _context.SaveChangesAsync();
        }

        var usdCostBasis = request.UsdCostBasis;
        if (usdCostBasis == null)
        {
            var symbolPrice = await _priceService.GetSymbolPrice(request.Symbol, request.PerformedAt.Date);
            usdCostBasis = request.Amount * symbolPrice.UsdPrice;
        }

        var deposit = new Deposit
        {
            DepositTypeId = request.DepositTypeId,
            Symbol = symbol,
            Amount = request.Amount,
            UsdCostBasis = usdCostBasis.Value,
            PerformedAt = request.PerformedAt,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Add(deposit);
        await _context.SaveChangesAsync();

        return this.Accepted();
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var deposits = await _context.Deposit
            .Include(x => x.DepositType)
            .Include(x => x.Symbol)
            .ToListAsync();

        return this.Ok(deposits);
    }
}

using CryptoWorkbooks.Data;
using CryptoWorkbooks.Data.Models;
using CryptoWorkbooks.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoWorkbooks.Controllers;

[ApiController]
[Route("[controller]")]
public class DepositsController : ControllerBase
{
    private readonly DataContext _context;
    private readonly ILogger<DepositsController> _logger;

    public DepositsController(DataContext context, ILogger<DepositsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateDepositRequest request)
    {
        var symbol = await _context.Symbol.FirstOrDefaultAsync(x => x.Name == request.Symbol.ToUpper());
        if (symbol == null)
        {
            symbol = new Symbol { Name = request.Symbol.ToUpper() };
        }

        var deposit = new Deposit
        {
            DepositTypeId = request.DepositTypeId,
            Symbol = symbol,
            Amount = request.Amount,
            UsdCostBasis = request.UsdCostBasis,
            PerformedAt = DateTimeOffset.UtcNow,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Add(deposit);
        await _context.SaveChangesAsync();

        return this.Accepted();
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var deposits = await _context.Deposit.ToListAsync();

        return this.Ok(deposits);
    }
}

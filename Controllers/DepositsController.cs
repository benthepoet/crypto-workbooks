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
    private readonly TransactionService _transactionService;

    public DepositsController(Context context, TransactionService transactionService)
    {
        _context = context;
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateDepositRequest request)
    {
        await _transactionService.CreateDeposit(request);

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

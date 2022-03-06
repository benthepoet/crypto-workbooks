using CryptoWorkbooks.Data;
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
    private readonly TransactionService _transactionService;

    public WithdrawalsController(Context context, TransactionService transactionService)
    {
        _context = context;
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateWithdrawalRequest request)
    {
        await _transactionService.CreateWithdrawal(request);

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

    [HttpPost("reconcile")]
    public async Task<IActionResult> ReconcileAsync()
    {
        await _transactionService.Reconcile();
        return this.Ok();
    }
}

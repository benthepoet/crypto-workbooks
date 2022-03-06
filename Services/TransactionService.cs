using CryptoWorkbooks.Data;
using CryptoWorkbooks.Data.Models;
using CryptoWorkbooks.Resources;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CryptoWorkbooks.Services;

public class TransactionService
{
    private readonly Context _context;
    private readonly PriceService _priceService;

    public TransactionService(Context context, PriceService priceService)
    {
        _context = context;
        _priceService = priceService;
    }

    public async Task CreateDeposit(CreateDepositRequest request)
    {
        var symbol = await GetSymbol(request.Symbol);

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
            FromWithdrawalId = request.FromWithdrawalId,
            PerformedAt = request.PerformedAt,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Add(deposit);
        await _context.SaveChangesAsync();
    }

    public async Task CreateWithdrawal(CreateWithdrawalRequest request)
    {
        var symbol = await GetSymbol(request.Symbol);

        var usdProceeds = request.UsdProceeds;
        if (usdProceeds == null)
        {
            var symbolPrice = await _priceService.GetSymbolPrice(request.Symbol, request.PerformedAt.Date);
            usdProceeds = request.Amount * symbolPrice.UsdPrice;
        }

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
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

            if (request.Proceeds != null && request.ProceedsSymbol != null)
            {
                await this.CreateDeposit(new CreateDepositRequest
                {
                    Amount = request.Proceeds.Value,
                    Symbol = request.ProceedsSymbol,
                    DepositTypeId = 1,
                    FromWithdrawalId = withdrawal.Id,
                    UsdCostBasis = usdProceeds.Value,
                    PerformedAt = request.PerformedAt
                });
            }

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
        }
    }

    public Task<Dictionary<string, decimal>> GetCapitalGains()
    {
        return Task.FromResult(new Dictionary<string, decimal>());
    }

    public async Task Reconcile()
    {
        var withdrawalSummary = await _context.WithdrawalCostBasis
            .Where(x => x.Unknown > 0m)
            .ToListAsync();

        foreach (var b in withdrawalSummary)
        {
            var unknownAmount = b.Unknown;
            
            var remainingDeposits = await _context.RemainingDeposit.ToListAsync();

            foreach (var deposit in remainingDeposits)
            {
                var amountToAllocate = Math.Min(unknownAmount, deposit.Remaining);

                var withdrawalTransaction = new WithdrawalTransaction
                {
                    Amount = amountToAllocate,
                    CreatedAt = DateTimeOffset.UtcNow,
                    DepositId = deposit.Id,
                    WithdrawalId = b.Id
                };
                _context.Add(withdrawalTransaction);
                await _context.SaveChangesAsync();

                if ((unknownAmount -= amountToAllocate) == 0)
                {
                    break;
                }
            }
        }

        return;
    }

    private async Task<Symbol> GetSymbol(string symbolName)
    {
        var symbol = await _context.Symbol.FirstOrDefaultAsync(x => x.Name == symbolName.ToUpper());
        if (symbol == null)
        {
            symbol = new Symbol { Name = symbolName.ToUpper() };
            _context.Add(symbol);
            await _context.SaveChangesAsync();
        }
        return symbol;
    }
}

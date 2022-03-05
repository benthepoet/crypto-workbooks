namespace CryptoWorkbooks.Data.Models;
public class Withdrawal
{
    public int Id { get; set; }

    public int WithdrawalTypeId { get; set; }
    public WithdrawalType? WithdrawalType { get; set; }

    public int SymbolId { get; set; }
    public Symbol? Symbol { get; set; }

    public decimal Amount { get; set; }

    public decimal? Proceeds { get; set; }
    
    public int? ProceedsSymbolId { get; set; }
    public Symbol? ProceedsSymbol { get; set; }

    public decimal UsdProceeds { get; set; }

    public DateTimeOffset PerformedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

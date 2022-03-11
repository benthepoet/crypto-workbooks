using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWorkbooks.Data.Models;

public class Withdrawal
{
    public int Id { get; set; }

    public int WithdrawalTypeId { get; set; }
    public WithdrawalType? WithdrawalType { get; set; }

    public int SymbolId { get; set; }
    public Symbol? Symbol { get; set; }

    [Column(TypeName = "decimal(24, 24)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(24, 24)")]
    public decimal? Proceeds { get; set; }
    
    public int? ProceedsSymbolId { get; set; }
    public Symbol? ProceedsSymbol { get; set; }

    [Column(TypeName = "decimal(16, 2)")]
    public decimal UsdProceeds { get; set; }

    public DateTimeOffset PerformedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

namespace CryptoWorkbooks.Data.Models;
public class Deposit
{
    public int Id { get; set; }

    public int DepositTypeId { get; set; }
    public DepositType? DepositType { get; set; }

    public int SymbolId { get; set; }
    public Symbol? Symbol { get; set; }

    public decimal Amount { get; set; }

    public decimal UsdCostBasis { get; set; }

    public int? FromWithdrawalId { get; set; }
    public Withdrawal? FromWithdrawal { get; set; }

    public DateTimeOffset PerformedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

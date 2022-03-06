namespace CryptoWorkbooks.Resources;
public class CreateDepositRequest
{
    public int DepositTypeId { get; set; }

    public string Symbol { get; set; } = null!;

    public decimal Amount { get; set; }

    public decimal? UsdCostBasis { get; set; }

    public int? FromWithdrawalId { get; set; }

    public DateTimeOffset PerformedAt { get; set; }
}
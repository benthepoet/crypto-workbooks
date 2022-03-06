namespace CryptoWorkbooks.Resources;

public class CreateWithdrawalRequest
{
    public int WithdrawalType { get; set; }

    public string Symbol { get; set; } = null!;

    public decimal Amount { get; set; }

    public decimal? Proceeds { get; set; }
    public string? ProceedsSymbol { get; set; }

    public decimal? UsdProceeds { get; set; }

    public DateTimeOffset PerformedAt { get; set; }
}

namespace CryptoWorkbooks.Data.Queries;

public class RemainingDeposit
{
    public int Id { get; set; }
    
    public decimal Amount { get; set; }

    public decimal UsdCostBasis { get; set; }

    public decimal Remaining { get; set; }

    public decimal RemainingUsdCostBasis { get; set; }
}

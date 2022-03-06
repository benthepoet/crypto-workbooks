namespace CryptoWorkbooks.Data.Queries;

public class WithdrawalCostBasis
{
    public int Id { get; set; }

    public decimal Known { get; set; }

    public decimal KnownUsdCostBasis { get; set; }

    public decimal Unknown { get; set; }
}

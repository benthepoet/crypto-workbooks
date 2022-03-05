namespace CryptoWorkbooks.Data;

public class DataContextOptions
{
    public const string SectionName = "Persistence";

    public string FirebirdConnectionString { get; set; } = null!;
}

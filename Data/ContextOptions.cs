namespace CryptoWorkbooks.Data;

public class ContextOptions
{
    public const string SectionName = "Persistence";

    public string FirebirdConnectionString { get; set; } = null!;
}

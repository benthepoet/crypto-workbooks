using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CryptoWorkbooks.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        string? connectionString = null;
        for (var i = 0; i < args.Length; i++)
        {
            if (args[i] == "--connectionString")
            {
                connectionString = args[i + 1];
            }
        }

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Connection string is missing.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseFirebird(connectionString);

        return new DataContext(optionsBuilder.Options);
    }
}

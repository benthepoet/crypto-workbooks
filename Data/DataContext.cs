using CryptoWorkbooks.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWorkbooks.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Deposit> Deposit { get; set; } = null!;
    public DbSet<DepositType> DepositType { get; set; } = null!;
    public DbSet<Symbol> Symbol { get; set; } = null!;
    public DbSet<SymbolPrice> SymbolPrice { get; set; } = null!;
    public DbSet<Withdrawal> Withdrawal { get; set; } = null!;
    public DbSet<WithdrawalType> WithdrawalType { get; set; } = null!;
}

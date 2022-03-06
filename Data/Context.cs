using CryptoWorkbooks.Data.Models;
using CryptoWorkbooks.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace CryptoWorkbooks.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<RemainingDeposit>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("View_RemainingDeposit");
                });

        modelBuilder
            .Entity<WithdrawalCostBasis>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("View_WithdrawalSummary");
                });
    }

    public DbSet<Deposit> Deposit { get; set; } = null!;
    public DbSet<DepositType> DepositType { get; set; } = null!;
    public DbSet<Symbol> Symbol { get; set; } = null!;
    public DbSet<SymbolPrice> SymbolPrice { get; set; } = null!;
    public DbSet<Withdrawal> Withdrawal { get; set; } = null!;
    public DbSet<WithdrawalTransaction> WithdrawalTransaction { get; set; } = null!;
    public DbSet<WithdrawalType> WithdrawalType { get; set; } = null!;

    public DbSet<RemainingDeposit> RemainingDeposit { get; set; } = null!;
    public DbSet<WithdrawalCostBasis> WithdrawalCostBasis { get; set; } = null!;
}

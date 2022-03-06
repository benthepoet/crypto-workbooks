using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWorkbooks.Data.Models;

public class SymbolPrice
{
    public int Id { get; set; }

    public int SymbolId { get; set; }
    public Symbol? Symbol { get; set; }

    [Column(TypeName = "decimal(24, 24)")]
    public decimal UsdPrice { get; set; }

    public DateTimeOffset SnapshotAt { get; set; }
}
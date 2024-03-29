﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWorkbooks.Data.Models;

public class WithdrawalTransaction
{
    public int Id { get; set; }

    public int WithdrawalId { get; set; }
    public Withdrawal? Withdrawal { get; set; }

    public int DepositId { get; set; }
    public Deposit? Deposit { get; set; }

    [Column(TypeName = "decimal(24, 24)")]
    public decimal Amount { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}

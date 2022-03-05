﻿// <auto-generated />
using System;
using CryptoWorkbooks.Data;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CryptoWorkbooks.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 31)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.Deposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("VARCHAR(48)");

                    b.Property<int>("DepositTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FromWithdrawalId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PerformedAt")
                        .IsRequired()
                        .HasColumnType("VARCHAR(48)");

                    b.Property<int>("SymbolId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UsdCostBasis")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("DepositTypeId");

                    b.HasIndex("FromWithdrawalId");

                    b.HasIndex("SymbolId");

                    b.ToTable("Deposit");
                });

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.DepositType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.HasKey("Id");

                    b.ToTable("DepositType");
                });

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.Symbol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.HasKey("Id");

                    b.ToTable("Symbol");
                });

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.SymbolPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SnapshotAt")
                        .IsRequired()
                        .HasColumnType("VARCHAR(48)");

                    b.Property<int>("SymbolId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UsdPrice")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("SymbolId");

                    b.ToTable("SymbolPrice");
                });

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.Withdrawal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("VARCHAR(48)");

                    b.Property<string>("PerformedAt")
                        .IsRequired()
                        .HasColumnType("VARCHAR(48)");

                    b.Property<decimal?>("Proceeds")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int?>("ProceedsSymbolId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SymbolId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UsdProceeds")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("WithdrawalTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProceedsSymbolId");

                    b.HasIndex("SymbolId");

                    b.HasIndex("WithdrawalTypeId");

                    b.ToTable("Withdrawal");
                });

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.WithdrawalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("BLOB SUB_TYPE TEXT");

                    b.HasKey("Id");

                    b.ToTable("WithdrawalType");
                });

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.Deposit", b =>
                {
                    b.HasOne("CryptoWorkbooks.Data.Models.DepositType", "DepositType")
                        .WithMany()
                        .HasForeignKey("DepositTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoWorkbooks.Data.Models.Withdrawal", "FromWithdrawal")
                        .WithMany()
                        .HasForeignKey("FromWithdrawalId");

                    b.HasOne("CryptoWorkbooks.Data.Models.Symbol", "Symbol")
                        .WithMany()
                        .HasForeignKey("SymbolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepositType");

                    b.Navigation("FromWithdrawal");

                    b.Navigation("Symbol");
                });

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.SymbolPrice", b =>
                {
                    b.HasOne("CryptoWorkbooks.Data.Models.Symbol", "Symbol")
                        .WithMany()
                        .HasForeignKey("SymbolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Symbol");
                });

            modelBuilder.Entity("CryptoWorkbooks.Data.Models.Withdrawal", b =>
                {
                    b.HasOne("CryptoWorkbooks.Data.Models.Symbol", "ProceedsSymbol")
                        .WithMany()
                        .HasForeignKey("ProceedsSymbolId");

                    b.HasOne("CryptoWorkbooks.Data.Models.Symbol", "Symbol")
                        .WithMany()
                        .HasForeignKey("SymbolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptoWorkbooks.Data.Models.WithdrawalType", "WithdrawalType")
                        .WithMany()
                        .HasForeignKey("WithdrawalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProceedsSymbol");

                    b.Navigation("Symbol");

                    b.Navigation("WithdrawalType");
                });
#pragma warning restore 612, 618
        }
    }
}

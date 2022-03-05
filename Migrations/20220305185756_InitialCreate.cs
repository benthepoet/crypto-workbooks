using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoWorkbooks.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepositType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Symbol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "BLOB SUB_TYPE TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SymbolPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsdPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    SnapshotAt = table.Column<string>(type: "VARCHAR(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SymbolPrice_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    WithdrawalTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Proceeds = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    ProceedsSymbolId = table.Column<int>(type: "INTEGER", nullable: true),
                    UsdProceeds = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PerformedAt = table.Column<string>(type: "VARCHAR(48)", nullable: false),
                    CreatedAt = table.Column<string>(type: "VARCHAR(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdrawal_Symbol_ProceedsS~",
                        column: x => x.ProceedsSymbolId,
                        principalTable: "Symbol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Withdrawal_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Withdrawal_WithdrawalType_W~",
                        column: x => x.WithdrawalTypeId,
                        principalTable: "WithdrawalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    DepositTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    SymbolId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    UsdCostBasis = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    FromWithdrawalId = table.Column<int>(type: "INTEGER", nullable: true),
                    PerformedAt = table.Column<string>(type: "VARCHAR(48)", nullable: false),
                    CreatedAt = table.Column<string>(type: "VARCHAR(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposit_DepositType_Deposit~",
                        column: x => x.DepositTypeId,
                        principalTable: "DepositType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposit_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposit_Withdrawal_FromWith~",
                        column: x => x.FromWithdrawalId,
                        principalTable: "Withdrawal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_DepositTypeId",
                table: "Deposit",
                column: "DepositTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_FromWithdrawalId",
                table: "Deposit",
                column: "FromWithdrawalId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_SymbolId",
                table: "Deposit",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_SymbolPrice_SymbolId",
                table: "SymbolPrice",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_ProceedsSymbolId",
                table: "Withdrawal",
                column: "ProceedsSymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_SymbolId",
                table: "Withdrawal",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_WithdrawalTypeId",
                table: "Withdrawal",
                column: "WithdrawalTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "SymbolPrice");

            migrationBuilder.DropTable(
                name: "DepositType");

            migrationBuilder.DropTable(
                name: "Withdrawal");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "WithdrawalType");
        }
    }
}

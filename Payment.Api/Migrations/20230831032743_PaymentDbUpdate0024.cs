using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.Api.Migrations
{
    /// <inheritdoc />
    public partial class PaymentDbUpdate0024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anticipations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnalysisStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnalysisEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnalysisResult = table.Column<int>(type: "int", nullable: false),
                    RequestedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnticipatedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anticipations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnticipationTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnticipatedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnticipationId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnticipationTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnticipationTransactions_Anticipations_AnticipationId",
                        column: x => x.AnticipationId,
                        principalTable: "Anticipations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnticipationTransactions_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnticipationTransactions_AnticipationId",
                table: "AnticipationTransactions",
                column: "AnticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_AnticipationTransactions_TransactionId",
                table: "AnticipationTransactions",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnticipationTransactions");

            migrationBuilder.DropTable(
                name: "Anticipations");
        }
    }
}

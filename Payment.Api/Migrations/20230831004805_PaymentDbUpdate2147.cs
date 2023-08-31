using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.Api.Migrations
{
    /// <inheritdoc />
    public partial class PaymentDbUpdate2147 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedFee",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FixedFee",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

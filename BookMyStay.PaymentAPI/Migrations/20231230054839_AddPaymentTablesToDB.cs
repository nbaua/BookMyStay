using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStay.PaymentAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentTablesToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentItem",
                columns: table => new
                {
                    BookingItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<double>(type: "float", nullable: true),
                    BookingTotal = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StripeSessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentItem", x => x.BookingItemId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentItemDetails",
                columns: table => new
                {
                    PaymentItemDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentItemId = table.Column<int>(type: "int", nullable: false),
                    ListingId = table.Column<int>(type: "int", nullable: false),
                    BookingPrice = table.Column<double>(type: "float", nullable: false),
                    DayOfStay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentItemDetails", x => x.PaymentItemDetailId);
                    table.ForeignKey(
                        name: "FK_PaymentItemDetails_PaymentItem_PaymentItemId",
                        column: x => x.PaymentItemId,
                        principalTable: "PaymentItem",
                        principalColumn: "BookingItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItemDetails_PaymentItemId",
                table: "PaymentItemDetails",
                column: "PaymentItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentItemDetails");

            migrationBuilder.DropTable(
                name: "PaymentItem");
        }
    }
}

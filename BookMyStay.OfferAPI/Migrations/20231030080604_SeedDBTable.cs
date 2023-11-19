using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookMyStay.OfferAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedDBTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "OfferId", "OfferCode", "OfferDetail", "OfferDiscountPerc" },
                values: new object[,]
                {
                    { 1, "FLAT05P", "Get flat 5 percent discount on your stay.", 5.0 },
                    { 2, "FLAT10P", "Get flat 10 percent discount on your stay.", 10.0 },
                    { 3, "FLAT15P", "Get flat 15 percent discount on your stay.", 15.0 },
                    { 4, "FLAT20P", "Get flat 20 percent discount on your stay.", 20.0 },
                    { 5, "FLAT25P", "Get flat 25 percent discount on your stay.", 25.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 5);
        }
    }
}

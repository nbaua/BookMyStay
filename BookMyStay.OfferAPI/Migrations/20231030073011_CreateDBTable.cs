using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStay.OfferAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateDBTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferDiscountPerc = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");
        }
    }
}

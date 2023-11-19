using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookMyStay.ListingAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateListingTableWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    ListingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ListingId);
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingId", "Category", "Description", "ImageUrl", "ListingPrice", "Name" },
                values: new object[,]
                {
                    { 1, "Small Villa", "Stay away from the city nuisance in midst of green lush. Choose from 5 small vintage houses in middle of the Bawana Jungle, which welcomes you for a cozy stay.", "https://i.ibb.co/C5x9cS4/pic1.jpg", 599.99000000000001, "Vintage Houses In Jungle" },
                    { 2, "Small Cabin", "Pick a nice wooden cabin for your next camping on top of the Hidler mountain range. Enough for 4 people's stay with lots of fun time for youe camping without a tent.", "https://i.ibb.co/0XgMrWj/pic2.jpg", 499.99000000000001, "Wooden Cabins On Mountain" },
                    { 3, "Large Villa", "Beautiful raw villa houses for a pleasant weekend spend near the Kilana lake, comes with all modern amenities and yet gives a rural feeling on your stay. Ideal for a family of 2-6 people.", "https://i.ibb.co/9NMM4vz/pic3.jpg", 1299.99, "Villa Houses On Rent" },
                    { 4, "Large Villa", "Beautiful modern villa houses for a lavish stay during your business trip, comes with all modern amenities and comfort. Very accessible from the Arila and Garmdos airports and Firana city railway stations. Ideal for corporate meetings and small events", "https://i.ibb.co/cNgwSyw/pic4.jpg", 1999.99, "Lush Houses For Business People" },
                    { 5, "Large Villa", "Beautiful modern bunglow for throwing lavish parties or arrange team events.Very accessible from the Arila and Garmdos airports and Firana city railway stations. Best choice for large corporate or family events.", "https://i.ibb.co/JtVSssw/pic5.jpg", 3499.9899999999998, "Bunglow For Events" },
                    { 6, "Small Villa", "Beautiful modern guest houses with all modern equipments, suitable for both business and pleasure of the guests. Accessible and affordable providing best value for money.", "https://i.ibb.co/sKv6Sr9/pic6.jpg", 899.99000000000001, "Corporate Guest Houses" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listings");
        }
    }
}

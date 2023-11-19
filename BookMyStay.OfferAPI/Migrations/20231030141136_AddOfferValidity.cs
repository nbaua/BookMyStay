using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStay.OfferAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddOfferValidity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OfferValidTill",
                table: "Offers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 1,
                column: "OfferValidTill",
                value: new DateTime(2023, 11, 24, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3796));

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 2,
                column: "OfferValidTill",
                value: new DateTime(2023, 11, 19, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3861));

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 3,
                column: "OfferValidTill",
                value: new DateTime(2023, 11, 14, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3875));

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 4,
                column: "OfferValidTill",
                value: new DateTime(2023, 11, 9, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3888));

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferId",
                keyValue: 5,
                column: "OfferValidTill",
                value: new DateTime(2023, 11, 4, 19, 41, 36, 803, DateTimeKind.Local).AddTicks(3901));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferValidTill",
                table: "Offers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class PickupAddressUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupDateTime",
                table: "DonationPickups");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDate",
                table: "DonationPickups",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupTime",
                table: "DonationPickups",
                type: "time",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BottomFloor",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "DonationPickups");

            migrationBuilder.DropColumn(
                name: "PickupTime",
                table: "DonationPickups");

            migrationBuilder.DropColumn(
                name: "BottomFloor",
                table: "Addresses");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDateTime",
                table: "DonationPickups",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class PickupOrDeliveryUpdateDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "PickupsDeliveries");

            migrationBuilder.DropColumn(
                name: "PickupTime",
                table: "PickupsDeliveries");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDateTime",
                table: "PickupsDeliveries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FurnitureDonationPickups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupDateTime",
                table: "PickupsDeliveries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FurnitureDonationPickups");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDate",
                table: "PickupsDeliveries",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupTime",
                table: "PickupsDeliveries",
                type: "time",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class Delivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FurnitureDonationPickups_DonationPickups_DonationPickupID",
                table: "FurnitureDonationPickups");

            migrationBuilder.DropTable(
                name: "DonationPickups");

            migrationBuilder.CreateTable(
                name: "PickupsDeliveries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: false),
                    CallEnRoute = table.Column<bool>(nullable: false),
                    Delivery = table.Column<bool>(nullable: false),
                    PickupDate = table.Column<DateTime>(type: "date", nullable: false),
                    PickupTime = table.Column<DateTime>(type: "time", nullable: false),
                    ScheduleDateTime = table.Column<DateTime>(nullable: false),
                    SpecialInstructions = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupsDeliveries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PickupsDeliveries_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PickupsDeliveries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PickupsDeliveries_AddressID",
                table: "PickupsDeliveries",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_PickupsDeliveries_UserId",
                table: "PickupsDeliveries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FurnitureDonationPickups_PickupsDeliveries_DonationPickupID",
                table: "FurnitureDonationPickups",
                column: "DonationPickupID",
                principalTable: "PickupsDeliveries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FurnitureDonationPickups_PickupsDeliveries_DonationPickupID",
                table: "FurnitureDonationPickups");

            migrationBuilder.DropTable(
                name: "PickupsDeliveries");

            migrationBuilder.CreateTable(
                name: "DonationPickups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: false),
                    CallEnRoute = table.Column<bool>(nullable: false),
                    PickupDate = table.Column<DateTime>(type: "date", nullable: false),
                    PickupTime = table.Column<DateTime>(type: "time", nullable: false),
                    ScheduleDateTime = table.Column<DateTime>(nullable: false),
                    SpecialInstructions = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationPickups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DonationPickups_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationPickups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationPickups_AddressID",
                table: "DonationPickups",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_DonationPickups_UserId",
                table: "DonationPickups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FurnitureDonationPickups_DonationPickups_DonationPickupID",
                table: "FurnitureDonationPickups",
                column: "DonationPickupID",
                principalTable: "DonationPickups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

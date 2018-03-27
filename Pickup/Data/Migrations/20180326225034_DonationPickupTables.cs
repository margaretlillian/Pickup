using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class DonationPickupTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonorID = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberTwo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Donors_Donors_DonorID",
                        column: x => x.DonorID,
                        principalTable: "Donors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Furnitures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnitures", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    DonorID = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    ZIP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Addresses_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Donors_DonorID",
                        column: x => x.DonorID,
                        principalTable: "Donors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonationPickups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: false),
                    CallEnRoute = table.Column<bool>(nullable: false),
                    PickupDateTime = table.Column<DateTime>(nullable: false),
                    ScheduleDateTime = table.Column<DateTime>(nullable: false),
                    SpecialInstructions = table.Column<string>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "FurnitureDonationPickups",
                columns: table => new
                {
                    DonationPickupID = table.Column<int>(nullable: false),
                    FurnitureID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureDonationPickups", x => new { x.DonationPickupID, x.FurnitureID });
                    table.ForeignKey(
                        name: "FK_FurnitureDonationPickups_DonationPickups_DonationPickupID",
                        column: x => x.DonationPickupID,
                        principalTable: "DonationPickups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurnitureDonationPickups_Furnitures_FurnitureID",
                        column: x => x.FurnitureID,
                        principalTable: "Furnitures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressID",
                table: "Addresses",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DonorID",
                table: "Addresses",
                column: "DonorID");

            migrationBuilder.CreateIndex(
                name: "IX_DonationPickups_AddressID",
                table: "DonationPickups",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_DonorID",
                table: "Donors",
                column: "DonorID");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureDonationPickups_FurnitureID",
                table: "FurnitureDonationPickups",
                column: "FurnitureID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FurnitureDonationPickups");

            migrationBuilder.DropTable(
                name: "DonationPickups");

            migrationBuilder.DropTable(
                name: "Furnitures");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Donors");
        }
    }
}

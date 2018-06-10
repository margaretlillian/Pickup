using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class Rename_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FurnitureDonationPickups");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "FurnitureCategories");

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemsDonatedSold",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsDonatedSold", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemsDonatedSold_ItemCategories_ItemCategoryID",
                        column: x => x.ItemCategoryID,
                        principalTable: "ItemCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPickupsDeliveries",
                columns: table => new
                {
                    PickupDeliveryID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ItemsDonatedSoldID = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPickupsDeliveries", x => new { x.PickupDeliveryID, x.ItemID });
                    table.ForeignKey(
                        name: "FK_ItemsPickupsDeliveries_ItemsDonatedSold_ItemsDonatedSoldID",
                        column: x => x.ItemsDonatedSoldID,
                        principalTable: "ItemsDonatedSold",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsPickupsDeliveries_PickupsDeliveries_PickupDeliveryID",
                        column: x => x.PickupDeliveryID,
                        principalTable: "PickupsDeliveries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsDonatedSold_ItemCategoryID",
                table: "ItemsDonatedSold",
                column: "ItemCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPickupsDeliveries_ItemsDonatedSoldID",
                table: "ItemsPickupsDeliveries",
                column: "ItemsDonatedSoldID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsPickupsDeliveries");

            migrationBuilder.DropTable(
                name: "ItemsDonatedSold");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.CreateTable(
                name: "FurnitureCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FurnitureCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Furniture_FurnitureCategories_FurnitureCategoryID",
                        column: x => x.FurnitureCategoryID,
                        principalTable: "FurnitureCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureDonationPickups",
                columns: table => new
                {
                    DonationPickupID = table.Column<int>(nullable: false),
                    FurnitureID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureDonationPickups", x => new { x.DonationPickupID, x.FurnitureID });
                    table.ForeignKey(
                        name: "FK_FurnitureDonationPickups_PickupsDeliveries_DonationPickupID",
                        column: x => x.DonationPickupID,
                        principalTable: "PickupsDeliveries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurnitureDonationPickups_Furniture_FurnitureID",
                        column: x => x.FurnitureID,
                        principalTable: "Furniture",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_FurnitureCategoryID",
                table: "Furniture",
                column: "FurnitureCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureDonationPickups_FurnitureID",
                table: "FurnitureDonationPickups",
                column: "FurnitureID");
        }
    }
}

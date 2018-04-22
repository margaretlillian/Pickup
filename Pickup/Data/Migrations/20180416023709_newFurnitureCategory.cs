using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class newFurnitureCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FurnitureCategoryID",
                table: "Furnitures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "FurnitureDonationPickups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FurnitureCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureCategory", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Furnitures_FurnitureCategoryID",
                table: "Furnitures",
                column: "FurnitureCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Furnitures_FurnitureCategory_FurnitureCategoryID",
                table: "Furnitures",
                column: "FurnitureCategoryID",
                principalTable: "FurnitureCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furnitures_FurnitureCategory_FurnitureCategoryID",
                table: "Furnitures");

            migrationBuilder.DropTable(
                name: "FurnitureCategory");

            migrationBuilder.DropIndex(
                name: "IX_Furnitures_FurnitureCategoryID",
                table: "Furnitures");

            migrationBuilder.DropColumn(
                name: "FurnitureCategoryID",
                table: "Furnitures");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "FurnitureDonationPickups");
        }
    }
}

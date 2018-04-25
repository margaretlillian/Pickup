using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class FurnituretoDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FurnitureDonationPickups_Furnitures_FurnitureID",
                table: "FurnitureDonationPickups");

            migrationBuilder.DropForeignKey(
                name: "FK_Furnitures_FurnitureCategory_FurnitureCategoryID",
                table: "Furnitures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Furnitures",
                table: "Furnitures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FurnitureCategory",
                table: "FurnitureCategory");

            migrationBuilder.RenameTable(
                name: "Furnitures",
                newName: "Furniture");

            migrationBuilder.RenameTable(
                name: "FurnitureCategory",
                newName: "FurnitureCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Furnitures_FurnitureCategoryID",
                table: "Furniture",
                newName: "IX_Furniture_FurnitureCategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Furniture",
                table: "Furniture",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FurnitureCategories",
                table: "FurnitureCategories",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Furniture_FurnitureCategories_FurnitureCategoryID",
                table: "Furniture",
                column: "FurnitureCategoryID",
                principalTable: "FurnitureCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FurnitureDonationPickups_Furniture_FurnitureID",
                table: "FurnitureDonationPickups",
                column: "FurnitureID",
                principalTable: "Furniture",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furniture_FurnitureCategories_FurnitureCategoryID",
                table: "Furniture");

            migrationBuilder.DropForeignKey(
                name: "FK_FurnitureDonationPickups_Furniture_FurnitureID",
                table: "FurnitureDonationPickups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FurnitureCategories",
                table: "FurnitureCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Furniture",
                table: "Furniture");

            migrationBuilder.RenameTable(
                name: "FurnitureCategories",
                newName: "FurnitureCategory");

            migrationBuilder.RenameTable(
                name: "Furniture",
                newName: "Furnitures");

            migrationBuilder.RenameIndex(
                name: "IX_Furniture_FurnitureCategoryID",
                table: "Furnitures",
                newName: "IX_Furnitures_FurnitureCategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FurnitureCategory",
                table: "FurnitureCategory",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Furnitures",
                table: "Furnitures",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FurnitureDonationPickups_Furnitures_FurnitureID",
                table: "FurnitureDonationPickups",
                column: "FurnitureID",
                principalTable: "Furnitures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Furnitures_FurnitureCategory_FurnitureCategoryID",
                table: "Furnitures",
                column: "FurnitureCategoryID",
                principalTable: "FurnitureCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

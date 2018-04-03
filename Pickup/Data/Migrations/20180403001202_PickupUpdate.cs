using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class PickupUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonationPickups_AspNetUsers_ApplicationUserId",
                table: "DonationPickups");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "DonationPickups",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DonationPickups_ApplicationUserId",
                table: "DonationPickups",
                newName: "IX_DonationPickups_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationPickups_AspNetUsers_UserId",
                table: "DonationPickups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonationPickups_AspNetUsers_UserId",
                table: "DonationPickups");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DonationPickups",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DonationPickups_UserId",
                table: "DonationPickups",
                newName: "IX_DonationPickups_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationPickups_AspNetUsers_ApplicationUserId",
                table: "DonationPickups",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class ForeignKeytoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DonationPickups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonationPickups_ApplicationUserId",
                table: "DonationPickups",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonationPickups_AspNetUsers_ApplicationUserId",
                table: "DonationPickups",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonationPickups_AspNetUsers_ApplicationUserId",
                table: "DonationPickups");

            migrationBuilder.DropIndex(
                name: "IX_DonationPickups_ApplicationUserId",
                table: "DonationPickups");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DonationPickups");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class DonorsCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_DonorsCustomers_DonorID",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "DonorID",
                table: "Addresses",
                newName: "DonorCustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_DonorID",
                table: "Addresses",
                newName: "IX_Addresses_DonorCustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_DonorsCustomers_DonorCustomerID",
                table: "Addresses",
                column: "DonorCustomerID",
                principalTable: "DonorsCustomers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_DonorsCustomers_DonorCustomerID",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "DonorCustomerID",
                table: "Addresses",
                newName: "DonorID");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_DonorCustomerID",
                table: "Addresses",
                newName: "IX_Addresses_DonorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_DonorsCustomers_DonorID",
                table: "Addresses",
                column: "DonorID",
                principalTable: "DonorsCustomers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

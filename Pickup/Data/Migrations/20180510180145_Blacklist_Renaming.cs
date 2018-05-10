using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pickup.Data.Migrations
{
    public partial class Blacklist_Renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Donors_DonorID",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.CreateTable(
                name: "DonorsCustomers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonorCustomerID = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberTwo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorsCustomers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DonorsCustomers_DonorsCustomers_DonorCustomerID",
                        column: x => x.DonorCustomerID,
                        principalTable: "DonorsCustomers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlacklistedDonors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DonorCustomerID = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlacklistedDonors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlacklistedDonors_DonorsCustomers_DonorCustomerID",
                        column: x => x.DonorCustomerID,
                        principalTable: "DonorsCustomers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlacklistedDonors_DonorCustomerID",
                table: "BlacklistedDonors",
                column: "DonorCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_DonorsCustomers_DonorCustomerID",
                table: "DonorsCustomers",
                column: "DonorCustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_DonorsCustomers_DonorID",
                table: "Addresses",
                column: "DonorID",
                principalTable: "DonorsCustomers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_DonorsCustomers_DonorID",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "BlacklistedDonors");

            migrationBuilder.DropTable(
                name: "DonorsCustomers");

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

            migrationBuilder.CreateIndex(
                name: "IX_Donors_DonorID",
                table: "Donors",
                column: "DonorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Donors_DonorID",
                table: "Addresses",
                column: "DonorID",
                principalTable: "Donors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class DeleteDesiredBuildingAddTargetLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesiredBuilding");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "DesiredPersonalities");

            migrationBuilder.AddColumn<int>(
                name: "TargetLocationFk",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TargetLocations",
                columns: table => new
                {
                    TargetLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetLocations", x => x.TargetLocationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TargetLocationFk",
                table: "Users",
                column: "TargetLocationFk",
                unique: true,
                filter: "[TargetLocationFk] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TargetLocations_TargetLocationFk",
                table: "Users",
                column: "TargetLocationFk",
                principalTable: "TargetLocations",
                principalColumn: "TargetLocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TargetLocations_TargetLocationFk",
                table: "Users");

            migrationBuilder.DropTable(
                name: "TargetLocations");

            migrationBuilder.DropIndex(
                name: "IX_Users_TargetLocationFk",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TargetLocationFk",
                table: "Users");

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "DesiredPersonalities",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "DesiredBuilding",
                columns: table => new
                {
                    DesiredBuildingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesiredBuilding", x => x.DesiredBuildingId);
                    table.ForeignKey(
                        name: "FK_DesiredBuilding_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesiredBuilding_UserId",
                table: "DesiredBuilding",
                column: "UserId");
        }
    }
}

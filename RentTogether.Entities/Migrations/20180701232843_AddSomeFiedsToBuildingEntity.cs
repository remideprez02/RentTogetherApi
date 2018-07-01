using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class AddSomeFiedsToBuildingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TargetLocations_TargetLocationFk",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TargetLocationFk",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TargetLocationFk",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Uri",
                table: "BuildingPicture",
                newName: "FileToBase64");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TargetLocations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbRenters",
                table: "Buildings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Buildings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TargetLocations_UserId",
                table: "TargetLocations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetLocations_Users_UserId",
                table: "TargetLocations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetLocations_Users_UserId",
                table: "TargetLocations");

            migrationBuilder.DropIndex(
                name: "IX_TargetLocations_UserId",
                table: "TargetLocations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TargetLocations");

            migrationBuilder.DropColumn(
                name: "NbRenters",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Buildings");

            migrationBuilder.RenameColumn(
                name: "FileToBase64",
                table: "BuildingPicture",
                newName: "Uri");

            migrationBuilder.AddColumn<int>(
                name: "TargetLocationFk",
                table: "Users",
                nullable: true);

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
    }
}

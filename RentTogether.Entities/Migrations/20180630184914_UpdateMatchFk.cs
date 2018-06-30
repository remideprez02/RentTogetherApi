using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class UpdateMatchFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserId1",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_UserId1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "MatchFk",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchFk",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserId1",
                table: "Matches",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserId1",
                table: "Matches",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

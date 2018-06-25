using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class UpdateMatchEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_InterestedUserUserId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_InterestingUserUserId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Status2",
                table: "Matches",
                newName: "StatusUser");

            migrationBuilder.RenameColumn(
                name: "Status1",
                table: "Matches",
                newName: "StatusTargetUser");

            migrationBuilder.RenameColumn(
                name: "InterestingUserUserId",
                table: "Matches",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "InterestedUserUserId",
                table: "Matches",
                newName: "TargetUserUserId");

            migrationBuilder.RenameColumn(
                name: "DateStatus2",
                table: "Matches",
                newName: "DateStatusUser");

            migrationBuilder.RenameColumn(
                name: "DateStatus1",
                table: "Matches",
                newName: "DateStatusTargetUser");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_InterestingUserUserId",
                table: "Matches",
                newName: "IX_Matches_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_InterestedUserUserId",
                table: "Matches",
                newName: "IX_Matches_TargetUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_TargetUserUserId",
                table: "Matches",
                column: "TargetUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserId1",
                table: "Matches",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_TargetUserUserId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserId1",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Matches",
                newName: "InterestingUserUserId");

            migrationBuilder.RenameColumn(
                name: "TargetUserUserId",
                table: "Matches",
                newName: "InterestedUserUserId");

            migrationBuilder.RenameColumn(
                name: "StatusUser",
                table: "Matches",
                newName: "Status2");

            migrationBuilder.RenameColumn(
                name: "StatusTargetUser",
                table: "Matches",
                newName: "Status1");

            migrationBuilder.RenameColumn(
                name: "DateStatusUser",
                table: "Matches",
                newName: "DateStatus2");

            migrationBuilder.RenameColumn(
                name: "DateStatusTargetUser",
                table: "Matches",
                newName: "DateStatus1");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_UserId1",
                table: "Matches",
                newName: "IX_Matches_InterestingUserUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_TargetUserUserId",
                table: "Matches",
                newName: "IX_Matches_InterestedUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_InterestedUserUserId",
                table: "Matches",
                column: "InterestedUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_InterestingUserUserId",
                table: "Matches",
                column: "InterestingUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

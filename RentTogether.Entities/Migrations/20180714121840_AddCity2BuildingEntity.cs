using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class AddCity2BuildingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City2",
                table: "Buildings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "BuildingHistories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuildingHistories_BuildingId",
                table: "BuildingHistories",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingHistories_Buildings_BuildingId",
                table: "BuildingHistories",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingHistories_Buildings_BuildingId",
                table: "BuildingHistories");

            migrationBuilder.DropIndex(
                name: "IX_BuildingHistories_BuildingId",
                table: "BuildingHistories");

            migrationBuilder.DropColumn(
                name: "City2",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "BuildingHistories");
        }
    }
}

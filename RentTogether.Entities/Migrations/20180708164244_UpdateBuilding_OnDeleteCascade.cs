using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class UpdateBuilding_OnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingMessages_Buildings_BuildingId",
                table: "BuildingMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_BuildingPictures_Buildings_BuildingId",
                table: "BuildingPictures");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingMessages_Buildings_BuildingId",
                table: "BuildingMessages",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingPictures_Buildings_BuildingId",
                table: "BuildingPictures",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingMessages_Buildings_BuildingId",
                table: "BuildingMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_BuildingPictures_Buildings_BuildingId",
                table: "BuildingPictures");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingMessages_Buildings_BuildingId",
                table: "BuildingMessages",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingPictures_Buildings_BuildingId",
                table: "BuildingPictures",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

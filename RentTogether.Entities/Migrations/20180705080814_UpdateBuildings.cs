using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class UpdateBuildings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingPicture_Buildings_BuildingId",
                table: "BuildingPicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingPicture",
                table: "BuildingPicture");

            migrationBuilder.RenameTable(
                name: "BuildingPicture",
                newName: "BuildingPictures");

            migrationBuilder.RenameIndex(
                name: "IX_BuildingPicture_BuildingId",
                table: "BuildingPictures",
                newName: "IX_BuildingPictures_BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingPictures",
                table: "BuildingPictures",
                column: "BuildingPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingPictures_Buildings_BuildingId",
                table: "BuildingPictures",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingPictures_Buildings_BuildingId",
                table: "BuildingPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingPictures",
                table: "BuildingPictures");

            migrationBuilder.RenameTable(
                name: "BuildingPictures",
                newName: "BuildingPicture");

            migrationBuilder.RenameIndex(
                name: "IX_BuildingPictures_BuildingId",
                table: "BuildingPicture",
                newName: "IX_BuildingPicture_BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingPicture",
                table: "BuildingPicture",
                column: "BuildingPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingPicture_Buildings_BuildingId",
                table: "BuildingPicture",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class UpdateFildsLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City2",
                table: "TargetLocations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InseeCode",
                table: "TargetLocations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City2",
                table: "TargetLocations");

            migrationBuilder.DropColumn(
                name: "InseeCode",
                table: "TargetLocations");
        }
    }
}

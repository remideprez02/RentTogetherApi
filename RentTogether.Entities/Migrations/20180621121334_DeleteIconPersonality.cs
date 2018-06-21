using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class DeleteIconPersonality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NegativeIcon",
                table: "PersonalityReferencials");

            migrationBuilder.DropColumn(
                name: "PositiveIcon",
                table: "PersonalityReferencials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NegativeIcon",
                table: "PersonalityReferencials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositiveIcon",
                table: "PersonalityReferencials",
                nullable: true);
        }
    }
}

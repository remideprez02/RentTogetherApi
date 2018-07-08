using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class UpdatePostalCodeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes",
                columns: new[] { "InseeCode", "PostalCodeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes",
                column: "InseeCode");
        }
    }
}

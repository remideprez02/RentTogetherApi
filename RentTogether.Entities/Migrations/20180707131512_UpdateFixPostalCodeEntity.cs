using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class UpdateFixPostalCodeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostalCodes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostalCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes",
                columns: new[] { "InseeCode", "PostalCodeId" });
        }
    }
}

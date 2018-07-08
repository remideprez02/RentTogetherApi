using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class AddPostCodeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostalCodes",
                columns: table => new
                {
                    InseeCode = table.Column<int>(nullable: false),
                    Gps = table.Column<string>(nullable: true),
                    Libelle = table.Column<string>(nullable: true),
                    Libelle2 = table.Column<string>(nullable: true),
                    PostalCodeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodes", x => x.InseeCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostalCodes");
        }
    }
}

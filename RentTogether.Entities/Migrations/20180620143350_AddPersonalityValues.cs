using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class AddPersonalityValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalityReferencials_Personnalities_PersonalityId",
                table: "PersonalityReferencials");

            migrationBuilder.DropIndex(
                name: "IX_PersonalityReferencials_PersonalityId",
                table: "PersonalityReferencials");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Personnalities");

            migrationBuilder.DropColumn(
                name: "PersonalityId",
                table: "PersonalityReferencials");

            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "PersonalityReferencials",
                newName: "PositiveIcon");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PersonalityReferencials",
                newName: "NegativeIcon");

            migrationBuilder.AddColumn<string>(
                name: "Description1",
                table: "PersonalityReferencials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "PersonalityReferencials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description3",
                table: "PersonalityReferencials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description4",
                table: "PersonalityReferencials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description5",
                table: "PersonalityReferencials",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalityValues",
                columns: table => new
                {
                    PersonalityValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonalityId = table.Column<int>(nullable: true),
                    PersonalityReferencialId = table.Column<int>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityValues", x => x.PersonalityValueId);
                    table.ForeignKey(
                        name: "FK_PersonalityValues_Personnalities_PersonalityId",
                        column: x => x.PersonalityId,
                        principalTable: "Personnalities",
                        principalColumn: "PersonalityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalityValues_PersonalityReferencials_PersonalityReferencialId",
                        column: x => x.PersonalityReferencialId,
                        principalTable: "PersonalityReferencials",
                        principalColumn: "PersonalityReferencialId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityValues_PersonalityId",
                table: "PersonalityValues",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityValues_PersonalityReferencialId",
                table: "PersonalityValues",
                column: "PersonalityReferencialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalityValues");

            migrationBuilder.DropColumn(
                name: "Description1",
                table: "PersonalityReferencials");

            migrationBuilder.DropColumn(
                name: "Description2",
                table: "PersonalityReferencials");

            migrationBuilder.DropColumn(
                name: "Description3",
                table: "PersonalityReferencials");

            migrationBuilder.DropColumn(
                name: "Description4",
                table: "PersonalityReferencials");

            migrationBuilder.DropColumn(
                name: "Description5",
                table: "PersonalityReferencials");

            migrationBuilder.RenameColumn(
                name: "PositiveIcon",
                table: "PersonalityReferencials",
                newName: "Icon");

            migrationBuilder.RenameColumn(
                name: "NegativeIcon",
                table: "PersonalityReferencials",
                newName: "Description");

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Personnalities",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PersonalityId",
                table: "PersonalityReferencials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityReferencials_PersonalityId",
                table: "PersonalityReferencials",
                column: "PersonalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalityReferencials_Personnalities_PersonalityId",
                table: "PersonalityReferencials",
                column: "PersonalityId",
                principalTable: "Personnalities",
                principalColumn: "PersonalityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class AddDetailMatchEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchDetails",
                columns: table => new
                {
                    MatchDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MatchId = table.Column<int>(nullable: true),
                    Percent = table.Column<int>(nullable: false),
                    PersonalityReferencialId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchDetails", x => x.MatchDetailId);
                    table.ForeignKey(
                        name: "FK_MatchDetails_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchDetails_PersonalityReferencials_PersonalityReferencialId",
                        column: x => x.PersonalityReferencialId,
                        principalTable: "PersonalityReferencials",
                        principalColumn: "PersonalityReferencialId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchDetails_MatchId",
                table: "MatchDetails",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchDetails_PersonalityReferencialId",
                table: "MatchDetails",
                column: "PersonalityReferencialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchDetails");
        }
    }
}

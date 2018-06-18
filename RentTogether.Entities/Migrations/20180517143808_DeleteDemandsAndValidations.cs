using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class DeleteDemandsAndValidations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Validations");

            migrationBuilder.DropTable(
                name: "Demands");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Demands",
                columns: table => new
                {
                    DemandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConversationId = table.Column<int>(nullable: true),
                    DemandDate = table.Column<DateTime>(nullable: false),
                    FromUserUserId = table.Column<int>(nullable: true),
                    ToUserUserId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demands", x => x.DemandId);
                    table.ForeignKey(
                        name: "FK_Demands_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demands_Users_FromUserUserId",
                        column: x => x.FromUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demands_Users_ToUserUserId",
                        column: x => x.ToUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Demands_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Validations",
                columns: table => new
                {
                    ValidationId = table.Column<int>(nullable: false),
                    IsValidate = table.Column<int>(nullable: false),
                    ValidationDate = table.Column<DateTime>(nullable: false),
                    VoteUserUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validations", x => x.ValidationId);
                    table.ForeignKey(
                        name: "FK_Validations_Demands_ValidationId",
                        column: x => x.ValidationId,
                        principalTable: "Demands",
                        principalColumn: "DemandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Validations_Users_VoteUserUserId",
                        column: x => x.VoteUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Demands_ConversationId",
                table: "Demands",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_FromUserUserId",
                table: "Demands",
                column: "FromUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_ToUserUserId",
                table: "Demands",
                column: "ToUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Demands_UserId",
                table: "Demands",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Validations_VoteUserUserId",
                table: "Validations",
                column: "VoteUserUserId");
        }
    }
}

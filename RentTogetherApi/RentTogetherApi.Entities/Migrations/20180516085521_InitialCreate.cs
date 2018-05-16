using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogetherApi.Entities.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    ConversationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.ConversationId);
                });

            migrationBuilder.CreateTable(
                name: "Personnalities",
                columns: table => new
                {
                    PersonalityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnalities", x => x.PersonalityId);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityReferencials",
                columns: table => new
                {
                    PersonalityReferencialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    PersonalityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityReferencials", x => x.PersonalityReferencialId);
                    table.ForeignKey(
                        name: "FK_PersonalityReferencials_Personnalities_PersonalityId",
                        column: x => x.PersonalityId,
                        principalTable: "Personnalities",
                        principalColumn: "PersonalityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuildingUsers",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingUsers", x => new { x.BuildingId, x.UserId });
                });

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
                });

            migrationBuilder.CreateTable(
                name: "Historics",
                columns: table => new
                {
                    HistoricId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConversationId = table.Column<int>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historics", x => x.HistoricId);
                    table.ForeignKey(
                        name: "FK_Historics_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConversationId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EditorUserId = table.Column<int>(nullable: true),
                    MessageText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
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
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    VoteId = table.Column<int>(nullable: false),
                    PersonalityReferencialId = table.Column<int>(nullable: true),
                    Score = table.Column<double>(nullable: false),
                    VoteUserUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_Votes_PersonalityReferencials_PersonalityReferencialId",
                        column: x => x.PersonalityReferencialId,
                        principalTable: "PersonalityReferencials",
                        principalColumn: "PersonalityReferencialId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsActive = table.Column<int>(nullable: false),
                    IsAdmin = table.Column<int>(nullable: false),
                    IsOwner = table.Column<int>(nullable: false),
                    IsRoomer = table.Column<int>(nullable: false),
                    IsValideUser = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PersonalityFk = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    TokenExpirationDate = table.Column<DateTime>(nullable: false),
                    Vote1Fk = table.Column<int>(nullable: true),
                    Vote2Fk = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Personnalities_PersonalityFk",
                        column: x => x.PersonalityFk,
                        principalTable: "Personnalities",
                        principalColumn: "PersonalityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Votes_Vote2Fk",
                        column: x => x.Vote2Fk,
                        principalTable: "Votes",
                        principalColumn: "VoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    OwnerUserId = table.Column<int>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                    table.ForeignKey(
                        name: "FK_Buildings_Users_OwnerUserId",
                        column: x => x.OwnerUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InterestedUserUserId = table.Column<int>(nullable: true),
                    InterestingUserUserId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Matches_Users_InterestedUserUserId",
                        column: x => x.InterestedUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Users_InterestingUserUserId",
                        column: x => x.InterestingUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PotentialMatches",
                columns: table => new
                {
                    PotentialMatchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InterestedUserUserId = table.Column<int>(nullable: true),
                    InterestingUserUserId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotentialMatches", x => x.PotentialMatchId);
                    table.ForeignKey(
                        name: "FK_PotentialMatches_Users_InterestedUserUserId",
                        column: x => x.InterestedUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PotentialMatches_Users_InterestingUserUserId",
                        column: x => x.InterestingUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PotentialMatches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_OwnerUserId",
                table: "Buildings",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingUsers_UserId",
                table: "BuildingUsers",
                column: "UserId");

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
                name: "IX_Historics_ConversationId",
                table: "Historics",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Historics_UserId",
                table: "Historics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_InterestedUserUserId",
                table: "Matches",
                column: "InterestedUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_InterestingUserUserId",
                table: "Matches",
                column: "InterestingUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserId",
                table: "Matches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_EditorUserId",
                table: "Messages",
                column: "EditorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityReferencials_PersonalityId",
                table: "PersonalityReferencials",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_PotentialMatches_InterestedUserUserId",
                table: "PotentialMatches",
                column: "InterestedUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PotentialMatches_InterestingUserUserId",
                table: "PotentialMatches",
                column: "InterestingUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PotentialMatches_UserId",
                table: "PotentialMatches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalityFk",
                table: "Users",
                column: "PersonalityFk",
                unique: true,
                filter: "[PersonalityFk] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Vote2Fk",
                table: "Users",
                column: "Vote2Fk",
                unique: true,
                filter: "[Vote2Fk] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Validations_VoteUserUserId",
                table: "Validations",
                column: "VoteUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PersonalityReferencialId",
                table: "Votes",
                column: "PersonalityReferencialId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoteUserUserId",
                table: "Votes",
                column: "VoteUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingUsers_Users_UserId",
                table: "BuildingUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingUsers_Buildings_BuildingId",
                table: "BuildingUsers",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Users_FromUserUserId",
                table: "Demands",
                column: "FromUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Users_ToUserUserId",
                table: "Demands",
                column: "ToUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Users_UserId",
                table: "Demands",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Historics_Users_UserId",
                table: "Historics",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_EditorUserId",
                table: "Messages",
                column: "EditorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Validations_Users_VoteUserUserId",
                table: "Validations",
                column: "VoteUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_VoteId",
                table: "Votes",
                column: "VoteId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_VoteUserUserId",
                table: "Votes",
                column: "VoteUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_VoteId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_VoteUserUserId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "BuildingUsers");

            migrationBuilder.DropTable(
                name: "Historics");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PotentialMatches");

            migrationBuilder.DropTable(
                name: "Validations");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Demands");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "PersonalityReferencials");

            migrationBuilder.DropTable(
                name: "Personnalities");
        }
    }
}

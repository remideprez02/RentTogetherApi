using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RentTogether.Entities.Migrations
{
    public partial class Model20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PotentialMatches");

            migrationBuilder.AddColumn<int>(
                name: "DesiredPersonalityFk",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteUserId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserPictureFk",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DesiredPersonalityDesiredCaracteristicId",
                table: "PersonalityReferencials",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsReport",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStatus1",
                table: "Matches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStatus2",
                table: "Matches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status1",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status2",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Area",
                table: "Buildings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Buildings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteBuildingId",
                table: "Buildings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbPiece",
                table: "Buildings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NbRoom",
                table: "Buildings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Parking",
                table: "Buildings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Buildings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Buildings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Buildings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BuildingMessages",
                columns: table => new
                {
                    BuildingMessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuildingId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsReport = table.Column<int>(nullable: false),
                    MessageText = table.Column<string>(nullable: true),
                    WriterUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMessages", x => x.BuildingMessageId);
                    table.ForeignKey(
                        name: "FK_BuildingMessages_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuildingMessages_Users_WriterUserId",
                        column: x => x.WriterUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuildingPicture",
                columns: table => new
                {
                    BuildingPictureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuildingId = table.Column<int>(nullable: true),
                    Uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingPicture", x => x.BuildingPictureId);
                    table.ForeignKey(
                        name: "FK_BuildingPicture_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesiredBuilding",
                columns: table => new
                {
                    DesiredBuildingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesiredBuilding", x => x.DesiredBuildingId);
                    table.ForeignKey(
                        name: "FK_DesiredBuilding_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesiredPersonalities",
                columns: table => new
                {
                    DesiredCaracteristicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesiredPersonalities", x => x.DesiredCaracteristicId);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteBuildings",
                columns: table => new
                {
                    FavoriteBuildingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteBuildings", x => x.FavoriteBuildingId);
                    table.ForeignKey(
                        name: "FK_FavoriteBuildings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteUsers",
                columns: table => new
                {
                    FavoriteUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VoteUserUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteUsers", x => x.FavoriteUserId);
                    table.ForeignKey(
                        name: "FK_FavoriteUsers_Users_VoteUserUserId",
                        column: x => x.VoteUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPictures",
                columns: table => new
                {
                    UserPictureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPictures", x => x.UserPictureId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DesiredPersonalityFk",
                table: "Users",
                column: "DesiredPersonalityFk",
                unique: true,
                filter: "[DesiredPersonalityFk] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteUserId",
                table: "Users",
                column: "FavoriteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPictureFk",
                table: "Users",
                column: "UserPictureFk",
                unique: true,
                filter: "[UserPictureFk] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityReferencials_DesiredPersonalityDesiredCaracteristicId",
                table: "PersonalityReferencials",
                column: "DesiredPersonalityDesiredCaracteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_FavoriteBuildingId",
                table: "Buildings",
                column: "FavoriteBuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMessages_BuildingId",
                table: "BuildingMessages",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMessages_WriterUserId",
                table: "BuildingMessages",
                column: "WriterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingPicture_BuildingId",
                table: "BuildingPicture",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_DesiredBuilding_UserId",
                table: "DesiredBuilding",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteBuildings_UserId",
                table: "FavoriteBuildings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUsers_VoteUserUserId",
                table: "FavoriteUsers",
                column: "VoteUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_FavoriteBuildings_FavoriteBuildingId",
                table: "Buildings",
                column: "FavoriteBuildingId",
                principalTable: "FavoriteBuildings",
                principalColumn: "FavoriteBuildingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalityReferencials_DesiredPersonalities_DesiredPersonalityDesiredCaracteristicId",
                table: "PersonalityReferencials",
                column: "DesiredPersonalityDesiredCaracteristicId",
                principalTable: "DesiredPersonalities",
                principalColumn: "DesiredCaracteristicId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DesiredPersonalities_DesiredPersonalityFk",
                table: "Users",
                column: "DesiredPersonalityFk",
                principalTable: "DesiredPersonalities",
                principalColumn: "DesiredCaracteristicId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FavoriteUsers_FavoriteUserId",
                table: "Users",
                column: "FavoriteUserId",
                principalTable: "FavoriteUsers",
                principalColumn: "FavoriteUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserPictures_UserPictureFk",
                table: "Users",
                column: "UserPictureFk",
                principalTable: "UserPictures",
                principalColumn: "UserPictureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_FavoriteBuildings_FavoriteBuildingId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalityReferencials_DesiredPersonalities_DesiredPersonalityDesiredCaracteristicId",
                table: "PersonalityReferencials");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_DesiredPersonalities_DesiredPersonalityFk",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FavoriteUsers_FavoriteUserId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserPictures_UserPictureFk",
                table: "Users");

            migrationBuilder.DropTable(
                name: "BuildingMessages");

            migrationBuilder.DropTable(
                name: "BuildingPicture");

            migrationBuilder.DropTable(
                name: "DesiredBuilding");

            migrationBuilder.DropTable(
                name: "DesiredPersonalities");

            migrationBuilder.DropTable(
                name: "FavoriteBuildings");

            migrationBuilder.DropTable(
                name: "FavoriteUsers");

            migrationBuilder.DropTable(
                name: "UserPictures");

            migrationBuilder.DropIndex(
                name: "IX_Users_DesiredPersonalityFk",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FavoriteUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserPictureFk",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PersonalityReferencials_DesiredPersonalityDesiredCaracteristicId",
                table: "PersonalityReferencials");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_FavoriteBuildingId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "DesiredPersonalityFk",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FavoriteUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserPictureFk",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DesiredPersonalityDesiredCaracteristicId",
                table: "PersonalityReferencials");

            migrationBuilder.DropColumn(
                name: "IsReport",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DateStatus1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "DateStatus2",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Status1",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Status2",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "FavoriteBuildingId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "NbPiece",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "NbRoom",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Parking",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Buildings");

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
        }
    }
}

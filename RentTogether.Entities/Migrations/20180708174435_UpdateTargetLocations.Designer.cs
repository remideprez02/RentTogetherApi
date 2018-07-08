﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RentTogether.Entities;
using System;

namespace RentTogether.Entities.Migrations
{
    [DbContext(typeof(RentTogetherDbContext))]
    [Migration("20180708174435_UpdateTargetLocations")]
    partial class UpdateTargetLocations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RentTogether.Entities.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Address2");

                    b.Property<int>("Area");

                    b.Property<string>("City");

                    b.Property<string>("Description");

                    b.Property<int?>("FavoriteBuildingId");

                    b.Property<int>("IsRent");

                    b.Property<int>("NbMaxRenters");

                    b.Property<int>("NbPiece");

                    b.Property<int>("NbRenters");

                    b.Property<int>("NbRoom");

                    b.Property<int?>("OwnerUserId");

                    b.Property<int>("Parking");

                    b.Property<string>("PostalCode");

                    b.Property<int>("Price");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("BuildingId");

                    b.HasIndex("FavoriteBuildingId");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("RentTogether.Entities.BuildingMessage", b =>
                {
                    b.Property<int>("BuildingMessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuildingId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("IsReport");

                    b.Property<string>("MessageText");

                    b.Property<int?>("WriterUserId");

                    b.HasKey("BuildingMessageId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("WriterUserId");

                    b.ToTable("BuildingMessages");
                });

            modelBuilder.Entity("RentTogether.Entities.BuildingPicture", b =>
                {
                    b.Property<int>("BuildingPictureId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuildingId");

                    b.Property<string>("FileToBase64");

                    b.HasKey("BuildingPictureId");

                    b.HasIndex("BuildingId");

                    b.ToTable("BuildingPictures");
                });

            modelBuilder.Entity("RentTogether.Entities.BuildingUser", b =>
                {
                    b.Property<int>("BuildingId");

                    b.Property<int>("UserId");

                    b.HasKey("BuildingId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("BuildingUsers");
                });

            modelBuilder.Entity("RentTogether.Entities.Conversation", b =>
                {
                    b.Property<int>("ConversationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Type");

                    b.HasKey("ConversationId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("RentTogether.Entities.DesiredPersonality", b =>
                {
                    b.Property<int>("DesiredCaracteristicId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("DesiredCaracteristicId");

                    b.ToTable("DesiredPersonalities");
                });

            modelBuilder.Entity("RentTogether.Entities.FavoriteBuilding", b =>
                {
                    b.Property<int>("FavoriteBuildingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("UserId");

                    b.HasKey("FavoriteBuildingId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteBuildings");
                });

            modelBuilder.Entity("RentTogether.Entities.FavoriteUser", b =>
                {
                    b.Property<int>("FavoriteUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("VoteUserUserId");

                    b.HasKey("FavoriteUserId");

                    b.HasIndex("VoteUserUserId");

                    b.ToTable("FavoriteUsers");
                });

            modelBuilder.Entity("RentTogether.Entities.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Average");

                    b.Property<int>("StatusTargetUser");

                    b.Property<int>("StatusUser");

                    b.Property<int?>("TargetUserUserId");

                    b.Property<int?>("UserId");

                    b.HasKey("MatchId");

                    b.HasIndex("TargetUserUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("RentTogether.Entities.MatchDetail", b =>
                {
                    b.Property<int>("MatchDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MatchId");

                    b.Property<int>("Percent");

                    b.Property<int?>("PersonalityReferencialId");

                    b.Property<int>("Value");

                    b.HasKey("MatchDetailId");

                    b.HasIndex("MatchId");

                    b.HasIndex("PersonalityReferencialId");

                    b.ToTable("MatchDetails");
                });

            modelBuilder.Entity("RentTogether.Entities.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ConversationId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("EditorUserId");

                    b.Property<int>("IsReport");

                    b.Property<string>("MessageText");

                    b.HasKey("MessageId");

                    b.HasIndex("ConversationId");

                    b.HasIndex("EditorUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RentTogether.Entities.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ConversationId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("UserId");

                    b.HasKey("ParticipantId");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("RentTogether.Entities.Personality", b =>
                {
                    b.Property<int>("PersonalityId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("PersonalityId");

                    b.ToTable("Personnalities");
                });

            modelBuilder.Entity("RentTogether.Entities.PersonalityReferencial", b =>
                {
                    b.Property<int>("PersonalityReferencialId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description1");

                    b.Property<string>("Description2");

                    b.Property<string>("Description3");

                    b.Property<string>("Description4");

                    b.Property<string>("Description5");

                    b.Property<int?>("DesiredPersonalityDesiredCaracteristicId");

                    b.Property<string>("Name");

                    b.HasKey("PersonalityReferencialId");

                    b.HasIndex("DesiredPersonalityDesiredCaracteristicId");

                    b.ToTable("PersonalityReferencials");
                });

            modelBuilder.Entity("RentTogether.Entities.PersonalityValue", b =>
                {
                    b.Property<int>("PersonalityValueId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PersonalityId");

                    b.Property<int?>("PersonalityReferencialId");

                    b.Property<int>("Value");

                    b.HasKey("PersonalityValueId");

                    b.HasIndex("PersonalityId");

                    b.HasIndex("PersonalityReferencialId");

                    b.ToTable("PersonalityValues");
                });

            modelBuilder.Entity("RentTogether.Entities.PostalCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Gps");

                    b.Property<string>("InseeCode");

                    b.Property<string>("Libelle");

                    b.Property<string>("Libelle2");

                    b.Property<string>("PostalCodeId");

                    b.HasKey("Id");

                    b.ToTable("PostalCodes");
                });

            modelBuilder.Entity("RentTogether.Entities.TargetLocation", b =>
                {
                    b.Property<int>("TargetLocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("City2");

                    b.Property<string>("PostalCode");

                    b.Property<int?>("UserId");

                    b.HasKey("TargetLocationId");

                    b.HasIndex("UserId");

                    b.ToTable("TargetLocations");
                });

            modelBuilder.Entity("RentTogether.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<int?>("DesiredPersonalityFk");

                    b.Property<string>("Email");

                    b.Property<int?>("FavoriteUserId");

                    b.Property<string>("FirstName");

                    b.Property<int>("IsActive");

                    b.Property<int>("IsAdmin");

                    b.Property<int>("IsOwner");

                    b.Property<int>("IsRoomer");

                    b.Property<int>("IsValideUser");

                    b.Property<string>("LastName");

                    b.Property<int?>("MatchFk");

                    b.Property<string>("Password");

                    b.Property<int?>("PersonalityFk");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Token");

                    b.Property<DateTime>("TokenExpirationDate");

                    b.Property<int?>("UserPictureFk");

                    b.Property<int?>("Vote1Fk");

                    b.Property<int?>("Vote2Fk");

                    b.HasKey("UserId");

                    b.HasIndex("DesiredPersonalityFk")
                        .IsUnique()
                        .HasFilter("[DesiredPersonalityFk] IS NOT NULL");

                    b.HasIndex("FavoriteUserId");

                    b.HasIndex("PersonalityFk")
                        .IsUnique()
                        .HasFilter("[PersonalityFk] IS NOT NULL");

                    b.HasIndex("UserPictureFk")
                        .IsUnique()
                        .HasFilter("[UserPictureFk] IS NOT NULL");

                    b.HasIndex("Vote2Fk")
                        .IsUnique()
                        .HasFilter("[Vote2Fk] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RentTogether.Entities.UserPicture", b =>
                {
                    b.Property<int>("UserPictureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileToBase64");

                    b.HasKey("UserPictureId");

                    b.ToTable("UserPictures");
                });

            modelBuilder.Entity("RentTogether.Entities.Vote", b =>
                {
                    b.Property<int>("VoteId");

                    b.Property<int?>("PersonalityReferencialId");

                    b.Property<double>("Score");

                    b.Property<int?>("VoteUserUserId");

                    b.HasKey("VoteId");

                    b.HasIndex("PersonalityReferencialId");

                    b.HasIndex("VoteUserUserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("RentTogether.Entities.Building", b =>
                {
                    b.HasOne("RentTogether.Entities.FavoriteBuilding")
                        .WithMany("TargetBuildings")
                        .HasForeignKey("FavoriteBuildingId");

                    b.HasOne("RentTogether.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerUserId");
                });

            modelBuilder.Entity("RentTogether.Entities.BuildingMessage", b =>
                {
                    b.HasOne("RentTogether.Entities.Building", "Building")
                        .WithMany("BuildingMessages")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RentTogether.Entities.User", "Writer")
                        .WithMany()
                        .HasForeignKey("WriterUserId");
                });

            modelBuilder.Entity("RentTogether.Entities.BuildingPicture", b =>
                {
                    b.HasOne("RentTogether.Entities.Building", "Building")
                        .WithMany("BuildingPictures")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RentTogether.Entities.BuildingUser", b =>
                {
                    b.HasOne("RentTogether.Entities.Building", "Building")
                        .WithMany("BuildingUsers")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RentTogether.Entities.User", "User")
                        .WithMany("BuildingUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RentTogether.Entities.FavoriteBuilding", b =>
                {
                    b.HasOne("RentTogether.Entities.User", "User")
                        .WithMany("FavoriteBuildings")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RentTogether.Entities.FavoriteUser", b =>
                {
                    b.HasOne("RentTogether.Entities.User", "VoteUser")
                        .WithMany("FavoriteUsers")
                        .HasForeignKey("VoteUserUserId");
                });

            modelBuilder.Entity("RentTogether.Entities.Match", b =>
                {
                    b.HasOne("RentTogether.Entities.User", "TargetUser")
                        .WithMany()
                        .HasForeignKey("TargetUserUserId");

                    b.HasOne("RentTogether.Entities.User", "User")
                        .WithMany("Matches")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RentTogether.Entities.MatchDetail", b =>
                {
                    b.HasOne("RentTogether.Entities.Match", "Match")
                        .WithMany("MatchDetails")
                        .HasForeignKey("MatchId");

                    b.HasOne("RentTogether.Entities.PersonalityReferencial", "PersonalityReferencial")
                        .WithMany()
                        .HasForeignKey("PersonalityReferencialId");
                });

            modelBuilder.Entity("RentTogether.Entities.Message", b =>
                {
                    b.HasOne("RentTogether.Entities.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId");

                    b.HasOne("RentTogether.Entities.User", "Editor")
                        .WithMany("Messages")
                        .HasForeignKey("EditorUserId");
                });

            modelBuilder.Entity("RentTogether.Entities.Participant", b =>
                {
                    b.HasOne("RentTogether.Entities.Conversation", "Conversation")
                        .WithMany("Participants")
                        .HasForeignKey("ConversationId");

                    b.HasOne("RentTogether.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RentTogether.Entities.PersonalityReferencial", b =>
                {
                    b.HasOne("RentTogether.Entities.DesiredPersonality")
                        .WithMany("PersonalityReferencials")
                        .HasForeignKey("DesiredPersonalityDesiredCaracteristicId");
                });

            modelBuilder.Entity("RentTogether.Entities.PersonalityValue", b =>
                {
                    b.HasOne("RentTogether.Entities.Personality")
                        .WithMany("PersonalityValues")
                        .HasForeignKey("PersonalityId");

                    b.HasOne("RentTogether.Entities.PersonalityReferencial", "PersonalityReferencial")
                        .WithMany()
                        .HasForeignKey("PersonalityReferencialId");
                });

            modelBuilder.Entity("RentTogether.Entities.TargetLocation", b =>
                {
                    b.HasOne("RentTogether.Entities.User", "User")
                        .WithMany("TargetLocations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RentTogether.Entities.User", b =>
                {
                    b.HasOne("RentTogether.Entities.DesiredPersonality", "DesiredPersonality")
                        .WithOne("User")
                        .HasForeignKey("RentTogether.Entities.User", "DesiredPersonalityFk");

                    b.HasOne("RentTogether.Entities.FavoriteUser")
                        .WithMany("TargetUsers")
                        .HasForeignKey("FavoriteUserId");

                    b.HasOne("RentTogether.Entities.Personality", "Personality")
                        .WithOne("User")
                        .HasForeignKey("RentTogether.Entities.User", "PersonalityFk");

                    b.HasOne("RentTogether.Entities.UserPicture", "UserPicture")
                        .WithOne("User")
                        .HasForeignKey("RentTogether.Entities.User", "UserPictureFk");

                    b.HasOne("RentTogether.Entities.Vote", "Vote")
                        .WithOne()
                        .HasForeignKey("RentTogether.Entities.User", "Vote2Fk");
                });

            modelBuilder.Entity("RentTogether.Entities.Vote", b =>
                {
                    b.HasOne("RentTogether.Entities.PersonalityReferencial", "PersonalityReferencial")
                        .WithMany()
                        .HasForeignKey("PersonalityReferencialId");

                    b.HasOne("RentTogether.Entities.User", "TargetUser")
                        .WithOne()
                        .HasForeignKey("RentTogether.Entities.Vote", "VoteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RentTogether.Entities.User", "VoteUser")
                        .WithMany()
                        .HasForeignKey("VoteUserUserId");
                });
#pragma warning restore 612, 618
        }
    }
}

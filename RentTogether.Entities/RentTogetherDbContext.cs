using System;
using Microsoft.EntityFrameworkCore;

namespace RentTogether.Entities
{
    public class RentTogetherDbContext : DbContext
    {
        public RentTogetherDbContext(DbContextOptions<RentTogetherDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Personality> Personnalities { get; set; }
        public DbSet<PersonalityReferencial> PersonalityReferencials { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Match> Matches { get; set; }
		public DbSet<Participant> Participants { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Message> Messages { get; set; }
		public DbSet<BuildingUser> BuildingUsers { get; set; }

		public DbSet<BuildingMessage> BuildingMessages { get; set; }
		public DbSet<DesiredPersonality> DesiredPersonalities { get; set; }
		public DbSet<FavoriteBuilding> FavoriteBuildings { get; set; }
		public DbSet<FavoriteUser> FavoriteUsers { get; set; } 
		public DbSet<UserPicture> UserPictures { get; set; }
        public DbSet<PersonalityValue> PersonalityValues { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			//UserId
            modelBuilder.Entity<User>()
			            .HasKey(x => x.UserId);
            
            //User Personnality
			modelBuilder.Entity<User>()
			            .HasOne(x => x.Personality)
			            .WithOne(x => x.User)
			            .HasForeignKey<User>(x => x.PersonalityFk)
			            .IsRequired(false);
            
            //User Vote
			modelBuilder.Entity<User>()
			            .HasOne(x => x.Vote)
			            .WithOne()
			            .HasForeignKey<User>(x => x.Vote1Fk)
			            .IsRequired(false);
			
			modelBuilder.Entity<User>()
			            .HasOne(x => x.Vote)
			            .WithOne()
			            .HasForeignKey<User>(x => x.Vote2Fk)
			            .IsRequired(false);

            //User Matches
            modelBuilder.Entity<User>()
			            .HasMany(x => x.Matches)
			            .WithOne();
			
            modelBuilder.Entity<User>()
			            .HasMany(x => x.Matches)
			            .WithOne();

            //User Message
            modelBuilder.Entity<User>()
			            .HasMany(x => x.Messages)
			            .WithOne(x => x.Editor);

			//User FavoriteUser
			modelBuilder.Entity<User>()
						.HasMany(x => x.FavoriteUsers)
			            .WithOne(x => x.VoteUser);

			//User UserPicture
			modelBuilder.Entity<User>()
						.HasOne(x => x.UserPicture)
						.WithOne(x => x.User)
			            .HasForeignKey<User>(x => x.UserPictureFk)
			            .IsRequired(false);
            
			//User DesiredPersonality
			modelBuilder.Entity<User>()
						.HasOne(x => x.DesiredPersonality)
						.WithOne(x => x.User)
			            .HasForeignKey<User>(x => x.DesiredPersonalityFk)
			            .IsRequired(false);

			//User DesiredBuildings
			modelBuilder.Entity<User>()
						.HasMany(x => x.DesiredBuildings)
						.WithOne(x => x.User);

			//User FavoriteBuildings
			modelBuilder.Entity<User>()
						.HasMany(x => x.FavoriteBuildings)
						.WithOne(x => x.User);

			//User BuildingMessages
			modelBuilder.Entity<User>()
						.HasMany(x => x.BuildingMessages)
						.WithOne(x => x.Writer);
			

            //Table Join User Building
			modelBuilder.Entity<BuildingUser>()
			            .HasKey(x => new { x.BuildingId, x.UserId });

			modelBuilder.Entity<BuildingUser>()
			            .HasOne(x => x.Building)
			            .WithMany(x => x.BuildingUsers)
			            .HasForeignKey(x => x.BuildingId);
			
			modelBuilder.Entity<BuildingUser>()
			            .HasOne(x => x.User)
			            .WithMany(x => x.BuildingUsers)
			            .HasForeignKey(x => x.UserId);

			//Building Picture
			modelBuilder.Entity<BuildingPicture>()
						.HasKey(x => x.BuildingPictureId);

			modelBuilder.Entity<BuildingPicture>()
			            .HasOne(x => x.Building)
			            .WithMany(x => x.BuildingPictures);

            //Building
            modelBuilder.Entity<Building>()
			            .HasKey(x => x.BuildingId);

			modelBuilder.Entity<Building>()
						.HasMany(x => x.BuildingPictures)
						.WithOne(x => x.Building);

			modelBuilder.Entity<Building>()
						.HasOne(x => x.Owner);
                        
			//Desired Personality
			modelBuilder.Entity<DesiredPersonality>()
						.HasKey(x => x.DesiredCaracteristicId);

			modelBuilder.Entity<DesiredPersonality>()
						.HasOne(x => x.User)
						.WithOne(x => x.DesiredPersonality);

			modelBuilder.Entity<DesiredPersonality>()
						.HasMany(x => x.PersonalityReferencials);



            //Personality
            modelBuilder.Entity<Personality>()
			            .HasKey(x => x.PersonalityId);
			
			modelBuilder.Entity<Personality>()
                        .HasMany(x => x.PersonalityValues);
			
			modelBuilder.Entity<Personality>()
			            .HasOne(x => x.User)
			            .WithOne(x => x.Personality);
            
			//PersonalityReferencial
			modelBuilder.Entity<PersonalityReferencial>()
			            .HasKey(x => x.PersonalityReferencialId);

            //Messages
            modelBuilder.Entity<Message>()
			            .HasKey(x => x.MessageId);
			
            modelBuilder.Entity<Message>()
			            .HasOne(x => x.Editor)
			            .WithMany(x => x.Messages);
			
			modelBuilder.Entity<Message>()
			            .HasOne(x => x.Conversation);

			//Historic
			modelBuilder.Entity<Participant>()
			            .HasKey(x => x.ParticipantId);
			modelBuilder.Entity<Participant>()
			            .HasOne(x => x.Conversation);
			
			modelBuilder.Entity<Participant>()
			            .HasOne(x => x.User);

			//Conversation
			modelBuilder.Entity<Conversation>()
			            .HasKey(x => x.ConversationId);
			
			modelBuilder.Entity<Conversation>()
			            .HasMany(x => x.Messages)
			            .WithOne(x => x.Conversation);

			modelBuilder.Entity<Conversation>()
						.HasMany(x => x.Participants)
						.WithOne(x => x.Conversation);

			//Vote
			modelBuilder.Entity<Vote>()
			            .HasKey(x => x.VoteId);
			
			modelBuilder.Entity<Vote>()
			            .HasOne(x => x.PersonalityReferencial);
			
			modelBuilder.Entity<Vote>()
			            .HasOne(x => x.VoteUser)
			            .WithOne()
			            .HasForeignKey<Vote>(x => x.VoteId)
			            .OnDelete(DeleteBehavior.Restrict);
			
			modelBuilder.Entity<Vote>()
			            .HasOne(x => x.TargetUser)
			            .WithOne()
			            .HasForeignKey<Vote>(x => x.VoteId)
			            .OnDelete(DeleteBehavior.Restrict);
            
			//Matches
			modelBuilder.Entity<Match>()
			            .HasKey(x => x.MatchId);
			
			modelBuilder.Entity<Match>()
			            .HasOne(x => x.InterestedUser)
			            .WithMany();
			
			modelBuilder.Entity<Match>()
			            .HasOne(x => x.InterestingUser)
			            .WithMany();

            //Building Message
			modelBuilder.Entity<BuildingMessage>()
			            .HasKey(x => x.BuildingMessageId);
			
			modelBuilder.Entity<BuildingMessage>()
			            .HasOne(x => x.Writer)
			            .WithMany(x => x.BuildingMessages);

			modelBuilder.Entity<BuildingMessage>()
						.HasOne(x => x.Building);

            //Favorite building
			modelBuilder.Entity<FavoriteBuilding>()
			            .HasKey(x => x.FavoriteBuildingId);
			
			modelBuilder.Entity<FavoriteBuilding>()
			            .HasOne(x => x.User)
			            .WithMany(x => x.FavoriteBuildings);
			
			modelBuilder.Entity<FavoriteBuilding>()
			            .HasMany(x => x.TargetBuildings);

			//Desired Building
			modelBuilder.Entity<DesiredBuilding>()
						.HasKey(x => x.DesiredBuildingId);

			modelBuilder.Entity<DesiredBuilding>()
						.HasOne(x => x.User)
						.WithMany(x => x.DesiredBuildings);

			//Favorite User
			modelBuilder.Entity<FavoriteUser>()
						.HasKey(x => x.FavoriteUserId);

			modelBuilder.Entity<FavoriteUser>()
						.HasOne(x => x.VoteUser)
						.WithMany(x => x.FavoriteUsers);

			modelBuilder.Entity<FavoriteUser>()
						.HasMany(x => x.TargetUsers);

            //PersonalityValue
            modelBuilder.Entity<PersonalityValue>()
                        .HasKey(x => x.PersonalityValueId);
            
            modelBuilder.Entity<PersonalityValue>()
                        .HasOne(x => x.PersonalityReferencial);
        }
    }
}

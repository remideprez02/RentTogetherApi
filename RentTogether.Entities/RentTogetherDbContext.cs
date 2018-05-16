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
        public DbSet<PotentialMatch> PotentialMatches { get; set; }
        public DbSet<Historic> Historics { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<Validation> Validations { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Message> Messages { get; set; }
		public DbSet<BuildingUser> BuildingUsers { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            
            //User Personnality
			modelBuilder.Entity<User>().HasOne(x => x.Personality).WithOne(x => x.User).HasForeignKey<User>(x => x.PersonalityFk).IsRequired(false);
            
            //User Vote
			modelBuilder.Entity<User>().HasOne(x => x.Vote).WithOne().HasForeignKey<User>(x => x.Vote1Fk).IsRequired(false);
			modelBuilder.Entity<User>().HasOne(x => x.Vote).WithOne().HasForeignKey<User>(x => x.Vote2Fk).IsRequired(false);

            //User Matches
            modelBuilder.Entity<User>().HasMany(x => x.Matches).WithOne();
            modelBuilder.Entity<User>().HasMany(x => x.Matches).WithOne();

            //User Potential Matches
            modelBuilder.Entity<User>().HasMany(x => x.PotentialMatches).WithOne();
            modelBuilder.Entity<User>().HasMany(x => x.PotentialMatches).WithOne();

            //User Message
            modelBuilder.Entity<User>().HasMany(x => x.Messages).WithOne(x => x.Editor);

            //User Validation
            modelBuilder.Entity<User>().HasMany(x => x.Validations).WithOne(x => x.VoteUser);

            //User Demand
            modelBuilder.Entity<User>().HasMany(x => x.Demands).WithOne();
            modelBuilder.Entity<User>().HasMany(x => x.Demands).WithOne();

            //User Historic
            modelBuilder.Entity<User>().HasMany(x => x.Historics).WithOne(x => x.User);

            //Table Join User Building
			modelBuilder.Entity<BuildingUser>().HasKey(x => new { x.BuildingId, x.UserId });

			modelBuilder.Entity<BuildingUser>().HasOne(x => x.Building).WithMany(x => x.BuildingUsers).HasForeignKey(x => x.BuildingId);
			modelBuilder.Entity<BuildingUser>().HasOne(x => x.User).WithMany(x => x.BuildingUsers).HasForeignKey(x => x.UserId);

			//User Building
			modelBuilder.Entity<User>().HasMany(x => x.Buildings).WithOne(x => x.Owner);

            //Building
            modelBuilder.Entity<Building>().HasKey(x => x.BuildingId);
            modelBuilder.Entity<Building>().HasOne(x => x.Owner).WithMany(x => x.Buildings);

            //Personality
            modelBuilder.Entity<Personality>().HasKey(x => x.PersonalityId);
			modelBuilder.Entity<Personality>().HasMany(x => x.PersonalityReferencials);
			modelBuilder.Entity<Personality>().HasOne(x => x.User).WithOne(x => x.Personality);
            
			//PersonalityReferencial
			modelBuilder.Entity<PersonalityReferencial>().HasKey(x => x.PersonalityReferencialId);

            //Messages
            modelBuilder.Entity<Message>().HasKey(x => x.MessageId);
            modelBuilder.Entity<Message>().HasOne(x => x.Editor).WithMany(x => x.Messages);
			modelBuilder.Entity<Message>().HasOne(x => x.Conversation);

			//Validation
			modelBuilder.Entity<Validation>().HasKey(x => x.ValidationId);
			modelBuilder.Entity<Validation>().HasOne(x => x.VoteUser).WithMany(x => x.Validations);
			modelBuilder.Entity<Validation>().HasOne(x => x.Demand).WithOne(x => x.Validation).HasForeignKey<Validation>(x => x.ValidationId);

			//Demand
			modelBuilder.Entity<Demand>().HasKey(x => x.DemandId);
			modelBuilder.Entity<Demand>().HasOne(x => x.Conversation);
			modelBuilder.Entity<Demand>().HasOne(x => x.FromUser).WithMany();
			modelBuilder.Entity<Demand>().HasOne(x => x.ToUser).WithMany();

			//Historic
			modelBuilder.Entity<Historic>().HasKey(x => x.HistoricId);
			modelBuilder.Entity<Historic>().HasOne(x => x.Conversation);
			modelBuilder.Entity<Historic>().HasOne(x => x.User).WithMany(x => x.Historics);

			//Conversation
			modelBuilder.Entity<Conversation>().HasKey(x => x.ConversationId);

			//Vote
			modelBuilder.Entity<Vote>().HasKey(x => x.VoteId);
			modelBuilder.Entity<Vote>().HasOne(x => x.PersonalityReferencial);
			modelBuilder.Entity<Vote>().HasOne(x => x.VoteUser).WithOne().HasForeignKey<Vote>(x => x.VoteId).OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Vote>().HasOne(x => x.TargetUser).WithOne().HasForeignKey<Vote>(x => x.VoteId).OnDelete(DeleteBehavior.Restrict);
            
			//Matches
			modelBuilder.Entity<Match>().HasKey(x => x.MatchId);
			modelBuilder.Entity<Match>().HasOne(x => x.InterestedUser).WithMany();
			modelBuilder.Entity<Match>().HasOne(x => x.InterestingUser).WithMany();

            //Potential Matches
			modelBuilder.Entity<PotentialMatch>().HasKey(x => x.PotentialMatchId);
			modelBuilder.Entity<PotentialMatch>().HasOne(x => x.InterestedUser).WithMany();
			modelBuilder.Entity<PotentialMatch>().HasOne(x => x.InterestingUser).WithMany();
        }
    }
}

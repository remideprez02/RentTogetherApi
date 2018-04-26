using System;
using Microsoft.EntityFrameworkCore;

namespace RentTogetherApi.Entities
{
    public class RentTogetherDbContext : DbContext
    {
        public RentTogetherDbContext(DbContextOptions<RentTogetherDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Roomer> Roomers { get; set; }
        public DbSet<RoomMate> RoomMates { get; set; }
        public DbSet<Personnality> Personnalities { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<User>()
                        .HasOne(x => x.Owner)
                        .WithOne(x => x.User)
                        .HasForeignKey<Owner>("UserOwnerForeignKey");
            modelBuilder.Entity<User>()
                        .HasOne(x => x.Roomer)
                        .WithOne(x => x.User)
                        .HasForeignKey<Roomer>("UserRoomerForeignKey");
            modelBuilder.Entity<User>().HasMany(x => x.Messages).WithOne(x => x.User);


            modelBuilder.Entity<Roomer>().HasKey(x => x.RoomerId);
            modelBuilder.Entity<Roomer>().HasOne(x => x.RoomMate);
            modelBuilder.Entity<Roomer>().HasOne(x => x.User).WithOne(x => x.Roomer);
            modelBuilder.Entity<Roomer>().HasOne(x => x.RoomMate);

            modelBuilder.Entity<RoomMate>().HasKey(x => x.RoomMateId);
            modelBuilder.Entity<RoomMate>().HasMany(x => x.Roomers).WithOne(x => x.RoomMate);
            modelBuilder.Entity<RoomMate>()
                        .HasOne(x => x.Building)
                        .WithOne(x => x.RoomMate)
                        .HasForeignKey<Building>("RoomMateBuildingForeignKey");
            modelBuilder.Entity<RoomMate>().HasOne(x => x.Owner).WithMany(x => x.RoomMates);


            modelBuilder.Entity<Owner>().HasKey(x => x.OwnerId);
            modelBuilder.Entity<Owner>().HasMany(x => x.RoomMates).WithOne(x => x.Owner);
            modelBuilder.Entity<Owner>().HasOne(x => x.User).WithOne(x => x.Owner);

            modelBuilder.Entity<Building>().HasKey(x => x.BuildingId);
            modelBuilder.Entity<Building>().HasOne(x => x.Owner).WithMany(x => x.Buildings);
            modelBuilder.Entity<Building>().HasOne(x => x.RoomMate).WithOne(x => x.Building);

            modelBuilder.Entity<Personnality>().HasKey(x => x.PersonnalityId);
            modelBuilder.Entity<Personnality>().HasMany(x => x.Roomers).WithOne(x => x.Personnality);

            modelBuilder.Entity<Message>().HasKey(x => x.MessageId);
            modelBuilder.Entity<Message>().HasOne(x => x.User).WithMany(x => x.Messages);

        }
    }
}

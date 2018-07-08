using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
        public string Description { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
        public int IsValideUser { get; set; }
        public int IsAdmin { get; set; }
        public int IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime TokenExpirationDate { get; set; }

		public List<FavoriteUser> FavoriteUsers { get; set; }
		public List<Message> Messages { get; set; }
		public UserPicture UserPicture { get; set; }
		public List<Match> Matches { get; set; }
		public Personality Personality { get; set; }
		public DesiredPersonality DesiredPersonality { get; set; }
        public List<TargetLocation> TargetLocations { get; set; }
		public List<FavoriteBuilding> FavoriteBuildings { get; set; }
		public List<BuildingUser> BuildingUsers { get; set; }
        
        public Vote Vote { get; set; }

		public int? PersonalityFk { get; set; }
		public int? Vote1Fk { get; set; }
		public int? Vote2Fk { get; set; }
        public int? MatchFk { get; set; }
        public int? UserPictureFk { get; set; }
        public int? DesiredPersonalityFk { get; set; }
       
    }
}

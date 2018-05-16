using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class User
    {
        public User()
        {
        }

        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
        public int IsValideUser { get; set; }
        public int IsAdmin { get; set; }
        public int IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime TokenExpirationDate { get; set; }

        public List<BuildingUser> BuildingUsers { get; set; }
		public List<Building> Buildings { get; set; }

        public Vote Vote { get; set; }

        public Personality Personality { get; set; }

        public List<Match> Matches { get; set; }
        public List<PotentialMatch> PotentialMatches { get; set; }

        public List<Message> Messages { get; set; }

        public List<Demand> Demands { get; set; }
        public List<Validation> Validations { get; set; }

        public List<Historic> Historics { get; set; }

		public int? PersonalityFk { get; set; }
		public int? Vote1Fk { get; set; }
		public int? Vote2Fk { get; set; }
       
    }
}

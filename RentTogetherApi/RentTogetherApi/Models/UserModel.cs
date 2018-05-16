using System;
using RentTogetherApi.Entities;

namespace RentTogetherApi.Api.Models
{
    public class UserModel
    {
        public UserModel()
        {
        }
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
        public int IsAdmin { get; set; }
        public int IsValideUser { get; set; }
        public DateTime CreateDate { get; set; }

	}
}

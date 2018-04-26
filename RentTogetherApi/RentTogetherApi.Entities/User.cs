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
        public int PhoneNumber { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
        public int IsValideUser { get; set; }
        public int IsAdmin { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime TokenExpirationDate { get; set; }
        public virtual Roomer Roomer { get; set; }
        public virtual Owner Owner { get; set; }
        public List<Message> Messages { get; set; }
    }
}

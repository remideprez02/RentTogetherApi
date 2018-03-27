using System;
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
        public int PhoneNumber { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
        public int IsValideUser { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Roomer Roomer { get; set; }
        public virtual Owner Owner { get; set; }
    }
}

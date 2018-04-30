using System;
namespace RentTogetherApi.Entities.Dto
{
    public class UserDto
    {
        public UserDto()
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
        public DateTime CreateDate { get; set; }
        public Roomer Roomer { get; set; }
        public Owner Owner { get; set; }
    }
}

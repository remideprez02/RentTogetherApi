using System;
namespace RentTogetherApi.Entities.Dto
{
    public class UserRegisterDto
    {
        public UserRegisterDto()
        {
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
    }
}

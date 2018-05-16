using System;
namespace RentTogether.Entities.Dto
{
    public class UserApiDto
    {
        public UserApiDto()
        {
        }

        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
        public int IsAdmin { get; set; }
        public DateTime CreateDate { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }
}

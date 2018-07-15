//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto
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
		public string PhoneNumber { get; set; }
		public string City { get; set; }
        public string PostalCode { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
        public int IsAdmin { get; set; }
    }
}

//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto
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
		public string PhoneNumber { get; set; }
		public string City { get; set; }
        public string PostalCode { get; set; }
        public string Description { get; set; }
        public int IsOwner { get; set; }
        public int IsRoomer { get; set; }
        public int IsAdmin { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

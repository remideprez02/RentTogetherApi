//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto
{
    public class UserLoginDto
    {
        public UserLoginDto()
        {
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}

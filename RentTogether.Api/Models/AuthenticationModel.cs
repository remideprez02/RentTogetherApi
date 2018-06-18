using System;
namespace RentTogether.Api.Models
{
    public class AuthenticationModel
    {
        public AuthenticationModel()
        {
        }

        public string Login { get; set; }
        public string PassWord { get; set; }
    }
}

﻿using System;
namespace RentTogetherApi.Entities.Dto
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

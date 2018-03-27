using System;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;
using RentTogetherApi.Interfaces.Helpers;

namespace RentTogetherApi.Common.Mapper
{
    public class Mapper : IMapperHelper
    {
        public Mapper()
        {
        }

        public User MapUserRegisterDtoToUser(UserRegisterDto userRegisterDto){
            
            return new User()
            {
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                IsOwner = userRegisterDto.IsOwner,
                IsRoomer = userRegisterDto.IsRoomer,
                Owner = new Owner(),
                PhoneNumber = userRegisterDto.PhoneNumber,
                Roomer = new Roomer(),
            };
        }
    }
}

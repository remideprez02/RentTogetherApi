using System;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;

namespace RentTogetherApi.Interfaces.Helpers
{
    public interface IMapperHelper
    {
        User MapUserRegisterDtoToUser(UserRegisterDto userRegisterDto);
    }
}

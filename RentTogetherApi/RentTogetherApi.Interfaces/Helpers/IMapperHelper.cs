using System;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;

namespace RentTogetherApi.Interfaces.Helpers
{
    public interface IMapperHelper
    {
        User MapUserRegisterDtoToUser(UserRegisterDto userRegisterDto);
        UserApiDto MapUserToUserApiDto(User user);
        User MapUpdateUserApiDtoToUser(UserApiDto userApiDto, User user);
    }
}

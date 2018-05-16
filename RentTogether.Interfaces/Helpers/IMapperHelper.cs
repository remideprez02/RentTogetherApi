using System;
using RentTogether.Entities;
using RentTogether.Entities.Dto;

namespace RentTogether.Interfaces.Helpers
{
    public interface IMapperHelper
    {
        User MapUserRegisterDtoToUser(UserRegisterDto userRegisterDto);
        UserApiDto MapUserToUserApiDto(User user);
        User MapUpdateUserApiDtoToUser(UserApiDto userApiDto, User user);
    }
}

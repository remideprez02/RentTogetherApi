using System;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;
using RentTogetherApi.Entities.Dto.Message;
using RentTogetherApi.Interfaces.Helpers;

namespace RentTogetherApi.Common.Mapper
{
    public class Mapper : IMapperHelper
    {
        public Mapper()
        {
        }
        #region User

        public User MapUserRegisterDtoToUser(UserRegisterDto userRegisterDto)
        {

            return new User()
            {
                Email = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Password = userRegisterDto.Password,
                IsOwner = userRegisterDto.IsOwner,
                IsRoomer = userRegisterDto.IsRoomer,
                IsAdmin = userRegisterDto.IsAdmin,
                Owner = new Owner(),
                PhoneNumber = userRegisterDto.PhoneNumber,
                Roomer = new Roomer(),
            };
        }

        public UserApiDto MapUserToUserApiDto(User user)
        {

            return new UserApiDto()
            {
                Email = user.Email,
                CreateDate = user.CreateDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                IsOwner = user.IsOwner,
                IsRoomer = user.IsRoomer,
                IsAdmin = user.IsAdmin,
                PhoneNumber = user.PhoneNumber,
                Token = user.Token,
                UserId = user.UserId,
                TokenExpirationDate = user.TokenExpirationDate
            };
        }

        public User MapUpdateUserApiDtoToUser(UserApiDto userApiDto, User user)
        {

            user.Email = userApiDto.Email;
            user.FirstName = userApiDto.FirstName;
            user.LastName = userApiDto.LastName;
            user.IsOwner = userApiDto.IsOwner;
            user.IsRoomer = userApiDto.IsRoomer;
            user.IsAdmin = userApiDto.IsAdmin;
            user.Password = userApiDto.Password;
            user.PhoneNumber = userApiDto.PhoneNumber;

            return user;
        }
        #endregion

    }
}

﻿using System;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Message;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Common.Mapper
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
                PhoneNumber = userRegisterDto.PhoneNumber,
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

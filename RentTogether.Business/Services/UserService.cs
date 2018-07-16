﻿//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IDal _dal;
        private readonly IMapperHelper _mapperHelper;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;

        public UserService(IDal dal, IMapperHelper mapperHelper, IAuthenticationService authenticationService,
                           ICustomEncoder customEncoder)
        {
            _dal = dal;
            _mapperHelper = mapperHelper;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
        }

        /// <summary>
        /// Creates the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="userRegisterDto">User register dto.</param>
        public async Task<User> CreateUserAsync(UserRegisterDto userRegisterDto)
        {
            var user = _mapperHelper.MapUserRegisterDtoToUser(userRegisterDto);

            if (await _dal.CheckIfUserAlreadyExistAsync(user))
            {

                var userApiDto = _authenticationService.RequestToken(user);
                user.Password = _customEncoder.Base64Encode(user.Password);
                user.Token = userApiDto.Token;
                user.TokenExpirationDate = userApiDto.TokenExpirationDate;

                await _dal.CreateUserAsync(user);
                return user;
            }
                return null;
        }

        /// <summary>
        /// Gets the user API dto async by identifier.
        /// </summary>
        /// <returns>The user API dto async by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<UserApiDto> GetUserApiDtoAsyncById(int id)
        {

            var user = await _dal.GetUserAsyncById(id);
            if (user != null)
            {
                var userAPiDto = _mapperHelper.MapUserToUserApiDto(user);
                return userAPiDto;
            }
            return null;
        }

        /// <summary>
        /// Gets the user by basic authentication async.
        /// </summary>
        /// <returns>The user by basic authentication async.</returns>
        /// <param name="userLoginDto">User login dto.</param>
        public async Task<UserApiDto> GetUserByBasicAuthenticationAsync(UserLoginDto userLoginDto)
        {
            var user = await _dal.GetUserByBasicAuthenticationAsync(userLoginDto);
            if (user == null)
                return null;

            var time = user.TokenExpirationDate.ToUniversalTime();

            //Check if user token is not UpToDate
            if (user.TokenExpirationDate.ToUniversalTime() < DateTime.UtcNow)
            {
                //Update token user
                var userApiDtoUpdate = _authenticationService.RequestToken(user);

                //Update user
                await _dal.UpdateUserAsync(userApiDtoUpdate);
            }

            var userApiDto = _mapperHelper.MapUserToUserApiDto(user);

            return userApiDto;
        }

        /// <summary>
        /// Deletes the user by identifier async.
        /// </summary>
        /// <returns>The user by identifier async.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="token">Token.</param>
        public async Task<bool> DeleteUserByIdAsync(int userId, string token)
        {
            var isValid = await _authenticationService.CheckIfTokenIsValidAsync(token, userId);
            var user = await _dal.GetUserAsyncByToken(token);

            if (user.IsAdmin == 1 || isValid)
            {
                var userDeleted = await _dal.DeleteUserByIdAsync(userId);

                if (userDeleted)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Updates the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="userApiDto">User API dto.</param>
        /// <param name="token">Token.</param>
        public async Task<UserApiDto> UpdateUserAsync(UserApiDto userApiDto, string token)
        {
            var isValid = await _authenticationService.CheckIfTokenIsValidAsync(userApiDto.Token, userApiDto.UserId);
            var user = await _dal.GetUserAsyncByToken(token);

            if (user.IsAdmin == 1 || isValid)
            {
                var userUpdatedApiDto = await _dal.UpdateUserAsync(userApiDto);

                if (userUpdatedApiDto != null)
                {
                    return userUpdatedApiDto;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// Gets all users async.
        /// </summary>
        /// <returns>The all users async.</returns>
		public async Task<List<UserApiDto>> GetAllUsersAsync()
        {
            var users = await _dal.GetAllUserAsync();

            if (users == null)
            {
                return null;
            }
            return users;
        }

        /// <summary>
        /// Gets the user async by token.
        /// </summary>
        /// <returns>The user async by token.</returns>
        /// <param name="token">Token.</param>
        public async Task<UserApiDto> GetUserAsyncByToken(string token)
        {
            var user = await _dal.GetUserAsyncByToken(token);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        /// <summary>
        /// Checks if user model is valid.
        /// </summary>
        /// <returns>The if user model is valid.</returns>
        /// <param name="userRegisterDto">User register dto.</param>
		public Tuple<bool, string> CheckIfUserModelIsValid(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto.Email == "" || userRegisterDto.Password == "")
                return new Tuple<bool, string>(false, "Invalid UserEmail or Password");

            if (userRegisterDto.City == "" || userRegisterDto.PostalCode == "")
                return new Tuple<bool, string>(false, "Invalid City or PostalCode");

            if (userRegisterDto.PhoneNumber == "" || userRegisterDto.PhoneNumber.Length > 10)
                return new Tuple<bool, string>(false, "Invalid UserEmail or Password");

            if ((userRegisterDto.IsAdmin < 0 || userRegisterDto.IsAdmin > 1) ||
                (userRegisterDto.IsOwner < 0 || userRegisterDto.IsOwner > 1) ||
                (userRegisterDto.IsRoomer < 0 || userRegisterDto.IsRoomer > 1))
                return new Tuple<bool, string>(false, "Invalid User Type");

            return new Tuple<bool, string>(true, "");
        }

        /// <summary>
        /// Patchs the user.
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="userPatchApiDto">User patch API dto.</param>
        public async Task<UserApiDto> PatchUser(UserPatchApiDto userPatchApiDto)
        {
            try
            {
                if (userPatchApiDto.UserId == null)
                    return null;
                return await _dal.PatchUser(userPatchApiDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Filters.Users;
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

        public UserService(IDal dal, IMapperHelper mapperHelper, IAuthenticationService authenticationService)
        {
            _dal = dal;
            _mapperHelper = mapperHelper;
            _authenticationService = authenticationService;
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
                user.Token = userApiDto.Token;
                user.TokenExpirationDate = userApiDto.TokenExpirationDate;

                await _dal.CreateUserAsync(user);
                return user;
            }
            throw new Exception();
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
                var userAPiDto = _mapperHelper.MapUserToUserApiDto(await _dal.GetUserAsyncById(id));
                return userAPiDto;
            }
            throw new Exception("Cannot Get User By Id");
        }

        /// <summary>
        /// Gets the user by basic authentication async.
        /// </summary>
        /// <returns>The user by basic authentication async.</returns>
        /// <param name="userLoginDto">User login dto.</param>
        public async Task<UserApiDto> GetUserByBasicAuthenticationAsync(UserLoginDto userLoginDto)
        {
            var user = await _dal.GetUserByBasicAuthenticationAsync(userLoginDto);

            //Check if user token is not UpToDate
            if(user.TokenExpirationDate < DateTime.UtcNow){
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

		public async Task<List<UserApiDto>> GetAllUsersAsync(UserFilters userFilters)
        {
			var users = await _dal.GetAllUserAsync(userFilters);

            if (users == null)
            {
                return null;
            }
            return users;
        }

        public async Task<UserApiDto> GetUserAsyncByToken(string token)
        {
            var user = await _dal.GetUserAsyncByToken(token);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}

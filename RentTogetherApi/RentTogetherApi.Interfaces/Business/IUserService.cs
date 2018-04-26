using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Business;
using RentTogetherApi.Entities.Dto;

namespace RentTogetherApi.Interfaces.Business
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserRegisterDto userRegisterDto);
        Task<UserApiDto> GetUserApiDtoAsyncById(int id);
        Task<UserApiDto> GetUserByBasicAuthenticationAsync(UserLoginDto userRegisterDto);
        Task<bool> DeleteUserByIdAsync(int userId, string token);
        Task<UserApiDto> UpdateUserAsync(UserApiDto userApiDto);
        Task<List<UserApiDto>> GetAllUsersAsync();
        Task<UserApiDto> GetUserAsyncByToken(string token);
    }
}

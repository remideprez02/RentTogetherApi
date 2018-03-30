using System;
using System.Threading.Tasks;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;

namespace RentTogetherApi.Interfaces.Dal
{
    public interface IDal
    {
        Task CreateUserAsync(User user);
        Task<bool> CheckIfUserAlreadyExistAsync(User newUser);
        Task<User> GetUserAsyncById(int id);
        Task<User> GetUserByBasicAuthenticationAsync(UserLoginDto userLoginDto);
        Task<DateTime> GetUserTokenExpirationDateAsync(string token);
        Task<bool> DeleteUserByIdAsync(int userId);
        Task<UserApiDto> UpdateUserAsync(UserApiDto userApiDto);
    }
}

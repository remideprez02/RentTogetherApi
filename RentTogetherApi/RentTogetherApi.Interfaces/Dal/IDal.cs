using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;
using RentTogetherApi.Entities.Dto.Message;

namespace RentTogetherApi.Interfaces.Dal
{
    public interface IDal
    {
        #region User
        Task CreateUserAsync(User user);
        Task<bool> CheckIfUserAlreadyExistAsync(User newUser);
        Task<User> GetUserAsyncById(int id);
        Task<User> GetUserByBasicAuthenticationAsync(UserLoginDto userLoginDto);
        Task<DateTime> GetUserTokenExpirationDateAsync(string token);
        Task<bool> DeleteUserByIdAsync(int userId);
        Task<UserApiDto> UpdateUserAsync(UserApiDto userApiDto);
        Task<List<UserApiDto>> GetAllUserAsync();
        Task<UserApiDto> GetUserAsyncByToken(string token);
        #endregion

        #region Message
        Task<List<MessageApiDto>> GetMessagesAsyncByUserId(int userId);
        Task AddMessageAsync(MessageDto messageDto);
        #endregion

    }
}

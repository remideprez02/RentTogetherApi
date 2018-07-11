using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities;
using RentTogether.Entities.Dto;

namespace RentTogether.Interfaces.Business
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserRegisterDto userRegisterDto);
        Task<UserApiDto> GetUserApiDtoAsyncById(int id);
        Task<UserApiDto> GetUserByBasicAuthenticationAsync(UserLoginDto userRegisterDto);
        Task<bool> DeleteUserByIdAsync(int userId, string token);
        Task<UserApiDto> UpdateUserAsync(UserApiDto userApiDto, string token);
		Task<List<UserApiDto>> GetAllUsersAsync();
        Task<UserApiDto> GetUserAsyncByToken(string token);
		Tuple<bool, string> CheckIfUserModelIsValid(UserRegisterDto userRegisterDto);
        Task<UserApiDto> PatchUser(UserPatchApiDto userPatchApiDto);
    }
}

using System;
using System.Threading.Tasks;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;

namespace RentTogetherApi.Interfaces.Business
{
    public interface IAuthenticationService
    {
        UserApiDto RequestToken(User user);
        Task<bool> CheckIfTokenIsValidAsync(string token, int userId);
    }
}

using System;
using System.Threading.Tasks;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Business;
using RentTogetherApi.Entities.Dto;

namespace RentTogetherApi.Interfaces.Business
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserRegisterDto userRegisterDto);
    }
}

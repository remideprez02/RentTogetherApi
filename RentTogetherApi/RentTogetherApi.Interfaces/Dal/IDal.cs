using System;
using System.Threading.Tasks;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;

namespace RentTogetherApi.Interfaces.Dal
{
    public interface IDal
    {
        Task CreateUserAsync(User user);
        Task<bool> CheckIfUserIsValideAsync(User newUser);

    }
}

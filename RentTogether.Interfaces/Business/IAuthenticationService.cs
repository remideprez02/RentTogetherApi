//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Threading.Tasks;
using RentTogether.Entities;
using RentTogether.Entities.Dto;

namespace RentTogether.Interfaces.Business
{
    public interface IAuthenticationService
    {
        UserApiDto RequestToken(User user);
        Task<bool> CheckIfTokenIsValidAsync(string token, int userId = 0);
    }
}

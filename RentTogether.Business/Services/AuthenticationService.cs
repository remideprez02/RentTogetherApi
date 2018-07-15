//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapperHelper _mapperHelper;
        private readonly IDal _dal;

        public AuthenticationService(IConfiguration configuration, IMapperHelper mapperHelper, IDal dal)
        {
            _configuration = configuration;
            _mapperHelper = mapperHelper;
            _dal = dal;
        }

        /// <summary>
        /// Requests the token.
        /// </summary>
        /// <returns>UserApiDto.</returns>
        /// <param name="user">User.</param>
        public UserApiDto RequestToken(User user)
        {
            try
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var token = new JwtSecurityToken
                (issuer: _configuration["Issuer"],
                    audience: _configuration["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecretKey"])),
                            SecurityAlgorithms.HmacSha256)
                );

                var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                user.Token = tokenStr;
                IFormatProvider culture = new CultureInfo("fr-FR", true);

                user.TokenExpirationDate = DateTime.UtcNow.AddDays(1);

                var userApiDto = _mapperHelper.MapUserToUserApiDto(user);

                return userApiDto;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Checks if token is valid async.
        /// </summary>
        /// <returns>The if token is valid async.</returns>
        /// <param name="token">Token.</param>
        /// <param name="userId">User identifier.</param>
		public async Task<bool> CheckIfTokenIsValidAsync(string token, int userId)
        {
            if (userId > 0)
            {
                var user = await _dal.GetUserAsyncById(userId);

                if (user == null)
                    return false;

                if (user.Token == token)
                {
                    var date = await _dal.GetUserTokenExpirationDateAsync(token);

                    if (date.ToUniversalTime() > DateTime.UtcNow)
                    {
                        return true;
                    }
                    return false;
                }

                return false;
            }

            else
            {
                var user = await _dal.GetUserAsyncByToken(token);
                if (user != null)
                {
                    if (user.TokenExpirationDate.ToUniversalTime() > DateTime.UtcNow)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
    }
}
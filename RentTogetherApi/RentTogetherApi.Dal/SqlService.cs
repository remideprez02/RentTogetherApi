using System;
using System.Threading.Tasks;
using RentTogetherApi.Entities;
using RentTogetherApi.Interfaces.Dal;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RentTogetherApi.Dal
{
    public class SqlService : IDal
    {
        private readonly RentTogetherDbContext _rentTogetherDbContext;

        public SqlService(RentTogetherDbContext rentTogetherDbContext)
        {
            _rentTogetherDbContext = rentTogetherDbContext;
        }
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="user">User.</param>
        public async Task CreateUserAsync(User user)
        {
            try
            {
                await _rentTogetherDbContext.Users.AddAsync(user);
                await _rentTogetherDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Checks if user is valide.
        /// </summary>
        /// <returns>The if user is valide async.</returns>
        /// <param name="newUser">New user.</param>
        public async Task<bool> CheckIfUserIsValideAsync(User newUser)
        {
            try
            {
                if (!await _rentTogetherDbContext.Users.AnyAsync(x => x.Email == newUser.Email))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

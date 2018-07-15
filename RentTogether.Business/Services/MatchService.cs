//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Match;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;

namespace RentTogether.Business.Services
{
    public class MatchService : IMatchService
    {
        private readonly IDal _dal;
        public MatchService(IDal dal)
        {
            _dal = dal;
        }

        /// <summary>
        /// Posts the async match.
        /// </summary>
        /// <returns>The async match.</returns>
        /// <param name="matchDto">Match dto.</param>
        public async Task<MatchApiDto> PostAsyncMatch(MatchDto matchDto)
        {
            return await _dal.PostAsyncMatch(matchDto);
        }

        /// <summary>
        /// Gets the async list matches.
        /// </summary>
        /// <returns>The async list matches.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MatchApiDto>> GetAsyncListMatches(int userId)
        {
            return await _dal.GetAsyncListMatches(userId);
        }

        /// <summary>
        /// Patchs the async matches.
        /// </summary>
        /// <returns>The async matches.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MatchApiDto>> PatchAsyncMatches(int userId)
        {
            return await _dal.PatchAsyncMatches(userId);
        }

        /// <summary>
        /// Deletes the async match.
        /// </summary>
        /// <returns>The async match.</returns>
        /// <param name="matchId">Match identifier.</param>
        public async Task<bool> DeleteAsyncMatch(int matchId)
        {
            return await _dal.DeleteAsyncMatch(matchId);
        }

        /// <summary>
        /// Gets the async all matches.
        /// </summary>
        /// <returns>The async all matches.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MatchApiDto>> GetAsyncAllMatches(int userId)
        {
            return await _dal.GetAsyncAllMatches(userId);
        }

        /// <summary>
        /// Gets the async validate matches.
        /// </summary>
        /// <returns>The async validate matches.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MatchApiDto>> GetAsyncValidateMatches(int userId)
        {
            return await _dal.GetAsyncValidateMatches(userId);
        }
    }
}

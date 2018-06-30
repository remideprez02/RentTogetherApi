using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Match;

namespace RentTogether.Interfaces.Business
{
    public interface IMatchService
    {
        Task<MatchApiDto> PostAsyncMatch(MatchDto matchDto);
        Task<List<MatchApiDto>> GetAsyncAllMatches(int userId);
        Task<List<MatchApiDto>> GetAsyncListMatches(int userId);
        Task<List<MatchApiDto>> PatchAsyncMatches(int userId);
        Task<bool> DeleteAsyncMatch(int matchId);
    }
}

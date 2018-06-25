using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Match;

namespace RentTogether.Interfaces.Business
{
    public interface IMatchService
    {
        Task<MatchApiDto> PostAsyncMatch(MatchDto matchDto);
        Task<List<MatchApiDto>> GetAsyncListMatches(int userId);
    }
}

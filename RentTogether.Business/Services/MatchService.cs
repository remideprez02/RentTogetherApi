﻿using System;
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

        public async Task<MatchApiDto> PostAsyncMatch(MatchDto matchDto){
            return await _dal.PostAsyncMatch(matchDto);
        }
        public async Task<List<MatchApiDto>> GetAsyncListMatches(int userId){
            return await _dal.GetAsyncListMatches(userId);
        }
        public async Task<List<MatchApiDto>> PatchAsyncMatches(int userId){
            return await _dal.PatchAsyncMatches(userId);
        }
        public async Task<bool> DeleteAsyncMatch(int matchId){
            return await _dal.DeleteAsyncMatch(matchId);
        }
        public async Task<List<MatchApiDto>> GetAsyncAllMatches(int userId){
            return await _dal.GetAsyncAllMatches(userId);
        }
        public async Task<List<MatchApiDto>> GetAsyncValidateMatches(int userId){
            return await _dal.GetAsyncValidateMatches(userId);
        }
    }
}

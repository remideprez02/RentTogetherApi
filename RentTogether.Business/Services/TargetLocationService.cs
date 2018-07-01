using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.TargetLocation;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;

namespace RentTogether.Business.Services
{
    public class TargetLocationService : ITargetLocationService
    {
        private readonly IDal _dal;
        public TargetLocationService(IDal dal)
        {
            _dal = dal;
        }

        public async Task<List<TargetLocationApiDto>> GetAsyncTargetLocationsByUserId(int userId){
           return await _dal.GetAsyncTargetLocationsByUserId(userId);
        }

        public async Task<List<TargetLocationApiDto>> PostAsyncTargetLocation(List<TargetLocationDto> targetLocationDtos, int userId){
            return await _dal.PostAsyncTargetLocation(targetLocationDtos, userId);
        }

        public async Task<List<TargetLocationApiDto>> PatchAsyncTargetLocation(List<TargetLocationPatchDto> targetLocationPatchDtos, int userId){
            return await _dal.PatchAsyncTargetLocation(targetLocationPatchDtos, userId);
        }

        public async Task<bool> DeleteAsyncTargetLocation(int targetLocationId){
            return await _dal.DeleteAsyncTargetLocation(targetLocationId);
        }
    }
}

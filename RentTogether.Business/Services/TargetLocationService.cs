using System;
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

        public async Task<TargetLocationApiDto> GetAsyncTargetLocationByUserId(int userId){
           return await _dal.GetAsyncTargetLocationByUserId(userId);
        }

        public async Task<TargetLocationApiDto> PostAsyncTargetLocation(TargetLocationDto targetLocationDto){
            return await _dal.PostAsyncTargetLocation(targetLocationDto);
        }

        public async Task<TargetLocationApiDto> PatchAsyncTargetLocation(TargetLocationDto targetLocationDto){
            return await _dal.PatchAsyncTargetLocation(targetLocationDto);
        }

        public async Task<bool> DeleteAsyncTargetLocation(int targetLocationId){
            return await _dal.DeleteAsyncTargetLocation(targetLocationId);
        }
    }
}

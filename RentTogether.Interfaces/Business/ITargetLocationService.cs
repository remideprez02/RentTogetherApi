using System;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.TargetLocation;

namespace RentTogether.Interfaces.Business
{
    public interface ITargetLocationService
    {
        Task<TargetLocationApiDto> GetAsyncTargetLocationByUserId(int userId);
        Task<TargetLocationApiDto> PostAsyncTargetLocation(TargetLocationDto targetLocationDto);
        Task<TargetLocationApiDto> PatchAsyncTargetLocation(TargetLocationDto targetLocationDto);
        Task<bool> DeleteAsyncTargetLocation(int targetLocationId);
    }
}

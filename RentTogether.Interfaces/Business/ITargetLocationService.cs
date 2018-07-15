//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.TargetLocation;

namespace RentTogether.Interfaces.Business
{
    public interface ITargetLocationService
    {
        Task<List<TargetLocationApiDto>> GetAsyncTargetLocationsByUserId(int userId);
        Task<List<TargetLocationApiDto>> PostAsyncTargetLocation(List<TargetLocationDto> targetLocationDtos, int userId);
        Task<List<TargetLocationApiDto>> PatchAsyncTargetLocation(List<TargetLocationPatchDto> targetLocationPatchDtos, int userId);
        Task<bool> DeleteAsyncTargetLocation(int targetLocationId);
    }
}

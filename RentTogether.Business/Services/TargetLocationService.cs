//
//Author : Déprez Rémi
//Version : 1.0
//

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

        /// <summary>
        /// Gets the async target locations by user identifier.
        /// </summary>
        /// <returns>The async target locations by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<TargetLocationApiDto>> GetAsyncTargetLocationsByUserId(int userId)
        {
            return await _dal.GetAsyncTargetLocationsByUserId(userId);
        }

        /// <summary>
        /// Posts the async target location.
        /// </summary>
        /// <returns>The async target location.</returns>
        /// <param name="targetLocationDtos">Target location dtos.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<List<TargetLocationApiDto>> PostAsyncTargetLocation(List<TargetLocationDto> targetLocationDtos, int userId)
        {
            return await _dal.PostAsyncTargetLocation(targetLocationDtos, userId);
        }

        /// <summary>
        /// Patchs the async target location.
        /// </summary>
        /// <returns>The async target location.</returns>
        /// <param name="targetLocationPatchDtos">Target location patch dtos.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<List<TargetLocationApiDto>> PatchAsyncTargetLocation(List<TargetLocationPatchDto> targetLocationPatchDtos, int userId)
        {
            return await _dal.PatchAsyncTargetLocation(targetLocationPatchDtos, userId);
        }

        /// <summary>
        /// Deletes the async target location.
        /// </summary>
        /// <returns>The async target location.</returns>
        /// <param name="targetLocationId">Target location identifier.</param>
        public async Task<bool> DeleteAsyncTargetLocation(int targetLocationId)
        {
            return await _dal.DeleteAsyncTargetLocation(targetLocationId);
        }
    }
}

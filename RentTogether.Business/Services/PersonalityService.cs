//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Personality;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;

namespace RentTogether.Business.Services
{
    public class PersonalityService : IPersonalityService
    {
        private readonly IDal _dal;
        public PersonalityService(IDal dal)
        {
            _dal = dal;
        }

        /// <summary>
        /// Posts the async detail personality.
        /// </summary>
        /// <returns>The async detail personality.</returns>
        /// <param name="detailPersonalityDto">Detail personality dto.</param>
        public async Task<DetailPersonalityApiDto> PostAsyncDetailPersonality(DetailPersonalityDto detailPersonalityDto)
        {
            return await _dal.PostAsyncDetailPersonality(detailPersonalityDto);
        }

        /// <summary>
        /// Gets the async all personality referencials.
        /// </summary>
        /// <returns>The async all personality referencials.</returns>
        public async Task<List<DetailPersonalityApiDto>> GetAsyncAllPersonalityReferencials()
        {
            return await _dal.GetAsyncAllPersonalityReferencials();
        }

        /// <summary>
        /// Posts the async personality values.
        /// </summary>
        /// <returns>The async personality values.</returns>
        /// <param name="personalityValueDtos">Personality value dtos.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<List<PersonalityValueApiDto>> PostAsyncPersonalityValues(List<PersonalityValueDto> personalityValueDtos, int userId)
        {
            return await _dal.PostAsyncPersonalityValues(personalityValueDtos, userId);
        }

        /// <summary>
        /// Gets the personality async by user identifier.
        /// </summary>
        /// <returns>The personality async by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<PersonalityApiDto> GetPersonalityAsyncByUserId(int userId)
        {
            return await _dal.GetPersonalityAsyncByUserId(userId);
        }

        /// <summary>
        /// Patchs the async personality values by user identifier.
        /// </summary>
        /// <returns>The async personality values by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        /// <param name="personalityValuePatchDtos">Personality value patch dtos.</param>
        public async Task<List<PersonalityValueApiDto>> PatchAsyncPersonalityValuesByUserId(int userId, List<PersonalityValuePatchDto> personalityValuePatchDtos)
        {
            return await _dal.PatchAsyncPersonalityValuesByUserId(userId, personalityValuePatchDtos);
        }
    }
}

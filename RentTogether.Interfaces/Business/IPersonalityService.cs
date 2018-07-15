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

namespace RentTogether.Interfaces.Business
{
    public interface IPersonalityService
    {
        Task<DetailPersonalityApiDto> PostAsyncDetailPersonality(DetailPersonalityDto detailPersonalityDto);
        Task<List<DetailPersonalityApiDto>> GetAsyncAllPersonalityReferencials();

        Task<List<PersonalityValueApiDto>> PostAsyncPersonalityValues(List<PersonalityValueDto> personalityValueDtos, int userId);
        Task<PersonalityApiDto> GetPersonalityAsyncByUserId(int userId);
        Task<List<PersonalityValueApiDto>> PatchAsyncPersonalityValuesByUserId(int userId, List<PersonalityValuePatchDto> personalityValuePatchDtos);
    }
}

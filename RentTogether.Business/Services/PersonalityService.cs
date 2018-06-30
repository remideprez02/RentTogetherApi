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

        public async Task<DetailPersonalityApiDto> PostAsyncDetailPersonality(DetailPersonalityDto detailPersonalityDto)
        {
            return await _dal.PostAsyncDetailPersonality(detailPersonalityDto);
        }

        public async Task<List<DetailPersonalityApiDto>> GetAsyncAllPersonalityReferencials()
        {
            return await _dal.GetAsyncAllPersonalityReferencials();
        }

        public async Task<List<PersonalityValueApiDto>> PostAsyncPersonalityValues(List<PersonalityValueDto> personalityValueDtos, int userId)
        {
            return await _dal.PostAsyncPersonalityValues(personalityValueDtos, userId);
        }

        public async Task<PersonalityApiDto> GetPersonalityAsyncByUserId(int userId)
        {
            return await _dal.GetPersonalityAsyncByUserId(userId);
        }

        public async Task<List<PersonalityValueApiDto>> PatchAsyncPersonalityValuesByUserId(int userId, List<PersonalityValuePatchDto> personalityValuePatchDtos){
            return await _dal.PatchAsyncPersonalityValuesByUserId(userId, personalityValuePatchDtos);
        }
    }
}

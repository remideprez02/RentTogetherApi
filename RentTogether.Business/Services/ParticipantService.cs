using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;

namespace RentTogether.Business.Services
{
	public class ParticipantService : IParticipantService
    {
		private readonly IDal _dal;
		public ParticipantService(IDal dal)
        {
			_dal = dal;
        }

		public async Task<ParticipantApiDto> GetParticipantAsyncByUserId(int userId){
			return await _dal.GetParticipantAsyncByUserId(userId);
		}

		public async Task<List<ParticipantApiDto>> GetAllParticipantsAsync(){
			return await _dal.GetAllParticipantAsync();
		}

        public async Task<List<ParticipantApiDto>> PostAsyncParticipantToExistingConversation(List<ParticipantDto> participantDtos){
            var participantApiDto = await _dal.PostAsyncParticipantToExistingConversation(participantDtos);
			return participantApiDto;
		}
    }
}

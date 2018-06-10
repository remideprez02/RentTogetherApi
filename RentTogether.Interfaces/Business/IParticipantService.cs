using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Participant;

namespace RentTogether.Interfaces.Business
{
	public interface IParticipantService
    {
		Task<ParticipantApiDto> GetParticipantAsyncByUserId(int userId);
		Task<List<ParticipantApiDto>> GetAllParticipantsAsync();
		Task<ParticipantApiDto> PostAsyncParticipantToExistingConversation(ParticipantDto participantDto);
    }
}

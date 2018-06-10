using System;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Participant;

namespace RentTogether.Interfaces.Business
{
	public interface IParticipantService
    {
		Task<ParticipantApiDto> GetParticipantAsyncByUserId(int userId);
    }
}

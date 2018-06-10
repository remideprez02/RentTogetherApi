using System;
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
    }
}

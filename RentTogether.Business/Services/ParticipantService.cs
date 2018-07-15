//
//Author : Déprez Rémi
//Version : 1.0
//

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

        /// <summary>
        /// Gets the participant async by user identifier.
        /// </summary>
        /// <returns>The participant async by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
		public async Task<ParticipantApiDto> GetParticipantAsyncByUserId(int userId)
        {
            return await _dal.GetParticipantAsyncByUserId(userId);
        }

        /// <summary>
        /// Gets all participants async.
        /// </summary>
        /// <returns>The all participants async.</returns>
		public async Task<List<ParticipantApiDto>> GetAllParticipantsAsync()
        {
            return await _dal.GetAllParticipantAsync();
        }

        /// <summary>
        /// Posts the async participant to existing conversation.
        /// </summary>
        /// <returns>The async participant to existing conversation.</returns>
        /// <param name="participantDtos">Participant dtos.</param>
        public async Task<List<ParticipantApiDto>> PostAsyncParticipantToExistingConversation(List<ParticipantDto> participantDtos)
        {
            var participantApiDto = await _dal.PostAsyncParticipantToExistingConversation(participantDtos);
            return participantApiDto;
        }
    }
}

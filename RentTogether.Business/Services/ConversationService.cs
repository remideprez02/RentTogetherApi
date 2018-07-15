//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Conversation;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Business.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IDal _dal;
        private readonly IMapperHelper _mapperHelper;
        private readonly IAuthenticationService _authenticationService;

        public ConversationService(IDal dal, IMapperHelper mapperHelper, IAuthenticationService authenticationService)
        {
            _dal = dal;
            _mapperHelper = mapperHelper;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Adds the conversation async.
        /// </summary>
        /// <returns>The conversation async.</returns>
        /// <param name="conversationDto">Conversation dto.</param>
		public async Task<ConversationApiDto> AddConversationAsync(ConversationDto conversationDto)
        {
            var conversationApiDto = await _dal.AddConversationAsync(conversationDto);
            return conversationApiDto;
        }

        /// <summary>
        /// Gets the conversations async by user identifier.
        /// </summary>
        /// <returns>The conversations async by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<ConversationApiDto>> GetConversationsAsyncByUserId(int userId)
        {
            return await _dal.GetConversationsAsyncByUserId(userId);
        }

        /// <summary>
        /// Gets all conversations async.
        /// </summary>
        /// <returns>The all conversations async.</returns>
		public async Task<List<ConversationApiDto>> GetAllConversationsAsync()
        {
            return await _dal.GetAllConversationsAsync();
        }
    }
}

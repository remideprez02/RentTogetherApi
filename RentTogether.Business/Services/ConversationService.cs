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

		public async Task<ConversationApiDto> AddConversationAsync(ConversationDto conversationDto)
        {
			var conversationApiDto = await _dal.AddConversationAsync(conversationDto);
			return conversationApiDto;
        }

        public async Task<ConversationApiDto> GetConversationAsyncByUserId(int userId)
        {
			return await _dal.GetConversationAsyncByUserId(userId);
        }

		public async Task<List<ConversationApiDto>> GetAllConversationsAsync(){
			return await _dal.GetAllConversationsAsync();
		}
    }
}

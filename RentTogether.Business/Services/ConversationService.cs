﻿using System;
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

		public async Task AddConversationAsync(ConversationDto conversationDto)
        {
			await _dal.AddConversationAsync(conversationDto);
        }

        public async Task<ConversationApiDto> GetConversationAsyncById(int conversationId)
        {
			return await _dal.GetConversationAsyncById(conversationId);
        }
    }
}

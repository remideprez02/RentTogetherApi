//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Message;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;

namespace RentTogether.Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly IDal _dal;

        public MessageService(IDal dal)
        {
            _dal = dal;
        }

        /// <summary>
        /// Adds the message async.
        /// </summary>
        /// <returns>The message async.</returns>
        /// <param name="messageDto">Message dto.</param>
		public async Task<MessageApiDto> AddMessageAsync(MessageDto messageDto)
        {
            return await _dal.AddMessageAsync(messageDto);
        }

        /// <summary>
        /// Gets all messages async by user identifier.
        /// </summary>
        /// <returns>The all messages async by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<MessageApiDto>> GetAllMessagesAsyncByUserId(int userId)
        {
            var messages = await _dal.GetMessagesAsyncByUserId(userId);
            return messages;
        }

        /// <summary>
        /// Gets all messages async from conversation by conversation identifier.
        /// </summary>
        /// <returns>The all messages async from conversation by conversation identifier.</returns>
        /// <param name="conversationId">Conversation identifier.</param>
		public async Task<List<MessageApiDto>> GetAllMessagesAsyncFromConversationByConversationId(int conversationId){
			return await _dal.GetAllMessagesAsyncFromConversationByConversationId(conversationId);
		}
    }
}

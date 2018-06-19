using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Conversation;

namespace RentTogether.Interfaces.Business
{
    public interface IConversationService
    {
        Task<List<ConversationApiDto>> GetConversationsAsyncByUserId(int userId);
		Task<ConversationApiDto> AddConversationAsync(ConversationDto conversationDto);
		Task<List<ConversationApiDto>> GetAllConversationsAsync();
    }
}

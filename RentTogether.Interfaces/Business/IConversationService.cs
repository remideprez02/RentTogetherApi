using System;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Conversation;

namespace RentTogether.Interfaces.Business
{
    public interface IConversationService
    {
        Task<ConversationApiDto> GetConversationAsyncById(int conversationId);
        Task AddConversationAsync(ConversationDto conversationDto);
    }
}

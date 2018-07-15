//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Message;

namespace RentTogether.Interfaces.Business
{
    public interface IMessageService
    {
		Task<MessageApiDto> AddMessageAsync(MessageDto messageDto);
		Task<List<MessageApiDto>> GetAllMessagesAsyncFromConversationByConversationId(int conversationId);
		Task<List<MessageApiDto>> GetAllMessagesAsyncByUserId(int userId);
    }
}

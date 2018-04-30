using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogetherApi.Entities.Dto.Message;

namespace RentTogetherApi.Interfaces.Business
{
    public interface IMessageService
    {
        Task CreateMessageAsync(MessageDto messageDto);
        Task<List<MessageApiDto>> GetAllMessagesAsyncByUserId(int userId);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Message;

namespace RentTogether.Interfaces.Business
{
    public interface IMessageService
    {
        Task CreateMessageAsync(MessageDto messageDto);
        Task<List<MessageApiDto>> GetAllMessagesAsyncByUserId(int userId);
    }
}

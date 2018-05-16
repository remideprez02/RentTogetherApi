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

        public async Task CreateMessageAsync(MessageDto messageDto)
        {
            await _dal.AddMessageAsync(messageDto);
        }

        public async Task<List<MessageApiDto>> GetAllMessagesAsyncByUserId(int userId)
        {
            var messages = await _dal.GetMessagesAsyncByUserId(userId);
            return messages;
        }
    }
}

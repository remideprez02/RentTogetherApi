using System;
namespace RentTogetherApi.Entities.Dto.Message
{
    public class MessageApiDto
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public string MessageText { get; set; }
    }
}

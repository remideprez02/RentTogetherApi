using System;

namespace RentTogether.Entities.Dto.Message
{
    public class MessageApiDto
    {
		public int MessageId { get; set; }

        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int ConversationId { get; set; }
    }
}

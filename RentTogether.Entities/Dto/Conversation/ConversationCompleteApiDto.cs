using System;
using System.Collections.Generic;
using RentTogether.Entities.Dto.Message;

namespace RentTogether.Entities.Dto.Conversation
{
    public class ConversationCompleteApiDto
    {
        public ConversationCompleteApiDto()
        {
        }

		public int ConversationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Type { get; set; }
		public List<MessageApiDto> Messages { get; set; }
    }
}

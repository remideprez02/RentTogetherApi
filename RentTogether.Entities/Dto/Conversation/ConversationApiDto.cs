using System;
namespace RentTogether.Entities.Dto.Conversation
{
    public class ConversationApiDto
    {
        public ConversationApiDto()
        {
        }
		public int ConversationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Type { get; set; }
    }
}

using System;
using System.Collections.Generic;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;

namespace RentTogether.Entities.Dto.Conversation
{
    public class ConversationApiDto
    {
        
		public int ConversationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Type { get; set; }
		public List<ParticipantApiDto> Participants { get; set; }
		public List<MessageApiDto> Messages { get; set; }
    }
}

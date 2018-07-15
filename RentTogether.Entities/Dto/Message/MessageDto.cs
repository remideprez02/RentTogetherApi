//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.Message
{
    public class MessageDto
    {
        public int UserId { get; set; }
		public int ConversationId { get; set; }
        public string MessageText { get; set; }
    }
}

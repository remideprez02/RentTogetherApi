//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class Conversation
    {
        public Conversation()
        {
        }

        [Key]
        public int ConversationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Type { get; set; }
		public List<Participant> Participants { get; set; }
		public List<Message> Messages { get; set; }
    }
}

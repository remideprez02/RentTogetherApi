using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
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
    }
}

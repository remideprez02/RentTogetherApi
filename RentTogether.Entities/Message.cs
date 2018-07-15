//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class Message
    {
        public Message()
        {
        }
        [Key]
        public int MessageId { get; set; }
        
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
        public User Editor { get; set; }
		public int IsReport { get; set; }
		public Conversation Conversation { get; set; }
    }
}

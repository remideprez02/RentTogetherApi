//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class Participant
    {
        public Participant()
        {
        }
        [Key]
		public int ParticipantId { get; set; }
		public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Conversation Conversation { get; set; }
        public User User { get; set; }
    }
}

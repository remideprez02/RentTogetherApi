using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Demand
    {
        public Demand()
        {
        }

        [Key]
        public int DemandId { get; set; }
        public DateTime DemandDate { get; set; }
        public User FromUser { get; set; }
        public Conversation Conversation { get; set; }
        public User ToUser { get; set; }
		public Validation Validation { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Historic
    {
        public Historic()
        {
        }

        [Key]
        public int HistoricId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Conversation Conversation { get; set; }
        public User User { get; set; }
    }
}

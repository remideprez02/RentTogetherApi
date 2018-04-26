using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Message
    {
        public Message()
        {
        }
        [Key]
        public int MessageId { get; set; }
        public User User { get; set; }
    }
}

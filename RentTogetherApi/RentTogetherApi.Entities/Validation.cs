using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Validation
    {
        public Validation()
        {
        }

        [Key]
        public int ValidationId { get; set; }
        public User VoteUser { get; set; }
        public int IsValidate { get; set; }
        public DateTime ValidationDate { get; set; }
        public Demand Demand { get; set; }
    }
}

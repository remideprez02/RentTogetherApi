using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Vote
    {
        public Vote()
        {
        }

        [Key]
        public int VoteId { get; set; }
        public PersonalityReferencial PersonalityReferencial { get; set; }
        public User VoteUser { get; set; }
        public User TargetUser { get; set; }
        public double Score { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class PotentialMatch
    {
        public PotentialMatch()
        {
        }

        [Key]
        public int PotentialMatchId { get; set; }
        public User InterestedUser { get; set; }
        public User InterestingUser { get; set; }
    }
}

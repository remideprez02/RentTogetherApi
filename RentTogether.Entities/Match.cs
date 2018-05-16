using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class Match
    {
        public Match()
        {
        }

        [Key]
        public int MatchId { get; set; }
        public User InterestedUser { get; set; }
        public User InterestingUser { get; set; }
    }
}

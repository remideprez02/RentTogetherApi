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
		public int Status1 { get; set; }
		public int Status2 { get; set; }
		public DateTime DateStatus1 { get; set; }
		public DateTime DateStatus2 { get; set; }
    }
}

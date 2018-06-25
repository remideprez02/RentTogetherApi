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

        public User User { get; set; }
        public User TargetUser { get; set; }

		public int StatusUser { get; set; }
		public int StatusTargetUser { get; set; }

		public DateTime DateStatusUser { get; set; }
		public DateTime DateStatusTargetUser { get; set; }
    }
}

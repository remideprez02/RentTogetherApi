using System;
using System.Collections.Generic;
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

        public List<MatchDetail> MatchDetails { get; set; }
    }
}

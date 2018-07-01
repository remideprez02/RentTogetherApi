using System;
using System.Collections.Generic;
using RentTogether.Entities.Dto.Personality.Value;

namespace RentTogether.Entities.Dto.Match
{
    public class MatchApiDto
    {
        public int MatchId { get; set; }

        public int UserId { get; set; }
        public UserApiDto TargetUser { get; set; }
        public List<MatchDetailApiDto> MatchDetailApiDtos { get; set; }

        public int StatusUser { get; set; }
        public int StatusTargetUser { get; set; }
    }
}

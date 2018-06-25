using System;
namespace RentTogether.Entities.Dto.Match
{
    public class MatchApiDto
    {
        public int MatchId { get; set; }

        public int UserId { get; set; }
        public int TargetUserId { get; set; }

        public int StatusUser { get; set; }
        public int StatusTargetUser { get; set; }

        public DateTime DateStatusUser { get; set; }
        public DateTime DateStatusTargetUser { get; set; }
    }
}

//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.Match
{
    public class MatchDto
    {
        public int MatchId { get; set; }

        public int UserId { get; set; }
        public int TargetUserId { get; set; }

        public int StatusUser { get; set; }
    }
}

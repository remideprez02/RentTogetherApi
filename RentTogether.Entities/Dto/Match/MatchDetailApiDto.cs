//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using RentTogether.Entities.Dto.Personality.Detail;

namespace RentTogether.Entities.Dto.Match
{
    public class MatchDetailApiDto
    {
        public DetailPersonalityApiDto DetailPersonalityApiDto { get; set; }
        public int Percent { get; set; }
        public int Value { get; set; }
    }
}

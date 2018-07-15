//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class MatchDetail
    {
        public MatchDetail()
        {
        }
        [Key]
        public int MatchDetailId { get; set; }

        public Match Match { get; set; }
        public PersonalityReferencial PersonalityReferencial { get; set; }
        public int Percent { get; set; }
        public int Value { get; set; }
    }
}

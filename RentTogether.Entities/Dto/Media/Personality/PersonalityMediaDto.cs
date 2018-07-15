//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.Media.Personality
{
    public class PersonalityMediaDto
    {
        public string PositiveIconBase64 { get; set; }
        public string NegativeIconBase64 { get; set; }
        public int ReferencialPersonalityId { get; set; }
    }
}

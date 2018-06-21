using System;
using System.Collections.Generic;
using RentTogether.Entities.Dto.Personality.Value;

namespace RentTogether.Entities.Dto.Personality
{
    public class PersonalityApiDto
    {
        public int PersonalityId { get; set; }

        public int UserId { get; set; }
        public List<PersonalityValueApiDto> PersonalityValueApiDtos { get; set; }
    }
}

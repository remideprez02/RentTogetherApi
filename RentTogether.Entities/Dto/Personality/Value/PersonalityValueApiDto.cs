using System;
namespace RentTogether.Entities.Dto.Personality.Value
{
    public class PersonalityValueApiDto
    {
        public int PersonalityValueId { get; set; }

        public int PersonalityReferencialId { get; set; }
        public int Value { get; set; }
    }
}

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

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class PersonalityValue
    {
        [Key]
        public int PersonalityValueId { get; set; }

        public PersonalityReferencial PersonalityReferencial { get; set; }
        public int Value { get; set; }
    }
}

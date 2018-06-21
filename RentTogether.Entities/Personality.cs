using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class Personality
    {
        public Personality()
        {
        }

        [Key]
        public int PersonalityId { get; set; }

        public User User { get; set; }
        public List<PersonalityValue> PersonalityValues { get; set; }
    }
}

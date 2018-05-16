using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Personality
    {
        public Personality()
        {
        }

        [Key]
        public int PersonalityId { get; set; }

        public User User { get; set; }
        public List<PersonalityReferencial> PersonalityReferencials { get; set; }
        public double Score { get; set; }
    }
}

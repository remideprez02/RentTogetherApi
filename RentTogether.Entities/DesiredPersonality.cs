using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class DesiredPersonality
    {
        [Key]
		public int DesiredCaracteristicId { get; set; }

		public User User { get; set; }
		public List<PersonalityReferencial> PersonalityReferencials { get; set; }
    }
}

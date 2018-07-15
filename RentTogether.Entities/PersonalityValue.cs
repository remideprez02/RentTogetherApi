//
//Author : Déprez Rémi
//Version : 1.0
//

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

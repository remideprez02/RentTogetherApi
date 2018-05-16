﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class PersonalityReferencial
    {
        public PersonalityReferencial()
        {
        }

        [Key]
        public int PersonalityReferencialId { get; set; }

        public string Description { get; set; }
        public string Icon { get; set; }
    }
}

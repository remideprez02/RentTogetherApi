﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class PersonalityReferencial
    {
        [Key]
        public int PersonalityReferencialId { get; set; }

        public string Name { get; set; }

        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public string Description4 { get; set; }
        public string Description5 { get; set; }
    }
}

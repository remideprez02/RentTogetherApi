﻿//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.Personality.Value
{
    public class PersonalityValuePatchDto
    {
        public int PersonalityReferencialId { get; set; }
        public int? Value { get; set; }
    }
}

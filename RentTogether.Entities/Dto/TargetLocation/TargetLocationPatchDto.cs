﻿using System;
namespace RentTogether.Entities.Dto.TargetLocation
{
    public class TargetLocationPatchDto
    {
        public int TargetLocationId { get; set; }
        public int UserId { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string City2 { get; set; }
    }
}

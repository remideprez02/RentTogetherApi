//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.TargetLocation
{
    public class TargetLocationApiDto
    {
        public int TargetLocationId { get; set; }

        public int UserId { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string City2 { get; set; }
    }
}

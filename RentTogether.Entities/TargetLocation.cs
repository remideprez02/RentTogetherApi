//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class TargetLocation
    {
        [Key]
        public int TargetLocationId { get; set; }

        public User User { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string City2 { get; set; }

    }
}

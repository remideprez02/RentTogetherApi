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
    }
}

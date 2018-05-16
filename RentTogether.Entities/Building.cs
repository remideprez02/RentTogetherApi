using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class Building
    {
        public Building()
        {
        }

        [Key]
        public int BuildingId { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
		public List<BuildingUser> BuildingUsers { get; set; }
		public User Owner { get; set; }

    }
}

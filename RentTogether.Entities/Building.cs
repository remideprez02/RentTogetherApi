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
		public List<BuildingPicture> BuildingPictures { get; set; }
		public int Type { get; set; }
		public int NbRoom { get; set; }
		public int NbPiece { get; set; }
        public int NbRenters { get; set; }
		public int Status { get; set; }
		public int Area { get; set; }
		public int Price { get; set; }
		public int Parking { get; set; }
		public string Description { get; set; }
        public string Title { get; set; }
    }
}

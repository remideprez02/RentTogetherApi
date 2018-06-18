using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class DesiredBuilding
    {
        public DesiredBuilding()
        {
        }
        [Key]
		public int DesiredBuildingId { get; set; }
		public User User { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class FavoriteBuilding
    {
        [Key]
		public int FavoriteBuildingId { get; set; }
		public List<Building> TargetBuildings { get; set; }
		public User User { get; set; }

    }
}

//
//Author : Déprez Rémi
//Version : 1.0
//

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

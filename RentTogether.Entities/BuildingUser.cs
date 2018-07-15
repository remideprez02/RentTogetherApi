//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities
{
    public class BuildingUser
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
    }
}

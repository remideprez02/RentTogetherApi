//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using RentTogether.Entities.Dto.Building;

namespace RentTogether.Entities.Dto.BuildingUser
{
    public class BuildingUserApiDto
    {
        public int UserId { get; set; }
        public int BuildingId { get; set; }
    }
}

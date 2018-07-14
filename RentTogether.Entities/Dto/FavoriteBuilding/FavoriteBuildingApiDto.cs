using System;
using RentTogether.Entities.Dto.Building;

namespace RentTogether.Entities.Dto.FavoriteBuilding
{
    public class FavoriteBuildingApiDto
    {
        public int FavoriteBuildingId { get; set; }
        public int BuildingId { get; set; }
        public int UserId { get; set; }
    }
}

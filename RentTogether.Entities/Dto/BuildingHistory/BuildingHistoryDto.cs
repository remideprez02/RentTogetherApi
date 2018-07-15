//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.BuildingHistory
{
    public class BuildingHistoryDto
    {
        public int HasSeen { get; set; }
        public int UserId { get; set; }
        public int BuildingId { get; set; }
    }
}

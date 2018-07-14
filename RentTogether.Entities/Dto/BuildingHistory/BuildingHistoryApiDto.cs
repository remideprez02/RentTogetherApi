using System;
namespace RentTogether.Entities.Dto.BuildingHistory
{
    public class BuildingHistoryApiDto
    {
		public int BuildingHistoryId { get; set; }
        public int HasSeen { get; set; }
        public int UserId { get; set; }
        public int BuildingId { get; set; }
    }
}

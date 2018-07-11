using System;
namespace RentTogether.Entities
{
    public class BuildingHistory
    {
        public int BuildingHistoryId { get; set; }
        public int HasSeen { get; set; }
        public User User { get; set; }
    }
}

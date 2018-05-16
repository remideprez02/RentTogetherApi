using System;
namespace RentTogetherApi.Entities
{
    public class BuildingUser
    {
        public BuildingUser()
        {
        }

        public int UserId { get; set; }
        public User User { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Owner
    {
        public Owner()
        {
        }

        [Key]
        public int OwnerId { get; set; }

        public List<RoomMate> RoomMates { get; set; }
        public List<Building> Buildings { get; set; }
        public User User { get; set; }
    }
}

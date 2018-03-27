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

        public virtual List<RoomMate> RoomMates { get; set; }
        public virtual List<Building> Buildings { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Building
    {
        public Building()
        {
        }

        [Key]
        public int BuildingId { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual RoomMate RoomMate { get; set;}
    }
}

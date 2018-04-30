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

        public Owner Owner { get; set; }
        public RoomMate RoomMate { get; set;}
    }
}

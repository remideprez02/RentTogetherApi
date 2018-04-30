using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Roomer
    {
        public Roomer()
        {
        }

        [Key]
        public int RoomerId { get; set; }

        public RoomMate RoomMate { get; set; }
        public User User { get; set; }
        public Personnality Personnality { get; set; }
    }
}

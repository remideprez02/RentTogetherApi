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

        public virtual RoomMate RoomMate { get; set; }
        public virtual User User { get; set; }
        public virtual Personnality Personnality { get; set; }
    }
}

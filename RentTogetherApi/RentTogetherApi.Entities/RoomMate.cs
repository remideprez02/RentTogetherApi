using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class RoomMate
    {
        public RoomMate()
        {
        }

        [Key]
        public int RoomMateId { get; set; }

        public virtual List<Roomer> Roomers { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Building Building { get; set; }
    }
}

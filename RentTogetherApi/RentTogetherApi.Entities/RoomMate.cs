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

        public List<Roomer> Roomers { get; set; }
        public Owner Owner { get; set; }
        public Building Building { get; set; }
    }
}

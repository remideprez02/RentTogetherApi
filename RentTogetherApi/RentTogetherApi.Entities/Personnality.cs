using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentTogetherApi.Entities
{
    public class Personnality
    {
        public Personnality()
        {
        }

        [Key]
        public int PersonnalityId { get; set; }

        public string Description { get; set; }
        public virtual List<Roomer> Roomers { get; set; }
    }
}

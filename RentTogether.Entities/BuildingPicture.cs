//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.ComponentModel.DataAnnotations;

namespace RentTogether.Entities
{
    public class BuildingPicture
    {
        public BuildingPicture()
        {
        }
        
        [Key]
		public int BuildingPictureId { get; set; }
        public string FileToBase64 { get; set; }
		public Building Building { get; set; }
    }
}

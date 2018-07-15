//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using RentTogether.Entities.Dto.Building;

namespace RentTogether.Entities.Dto.BuildingPicture
{
    public class BuildingPictureApiDto
    {
        public int BuildingPictureId { get; set; }
        public string FileToBase64 { get; set; }
        public int BuildingId { get; set; }
    }
}

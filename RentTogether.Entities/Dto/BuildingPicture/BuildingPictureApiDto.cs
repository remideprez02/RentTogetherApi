﻿using System;
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

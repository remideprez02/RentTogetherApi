using System;
using Microsoft.AspNetCore.Http;

namespace RentTogether.Entities.Dto.BuildingPicture
{
    public class BuildingPictureDto
    {
        public string FileToBase64 { get; set; }
        public IFormFile File { get; set; }
        public int BuildingId { get; set; }
    }
}

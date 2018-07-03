using System;
using System.Collections.Generic;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.BuildingUser;

namespace RentTogether.Entities.Dto.Building
{
    public class BuildingApiDto
    {
        public int BuildingId { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public List<BuildingUserApiDto> BuildingUserApiDtos { get; set; }
        public UserApiDto OwnerApiDto { get; set; }
        public List<BuildingPictureApiDto> BuildingPictureApiDtos { get; set; }
        public int Type { get; set; }
        public int NbRoom { get; set; }
        public int NbPiece { get; set; }
        public int NbRenters { get; set; }
        public int NbMaxRenters { get; set; }
        public int Status { get; set; }
        public int Area { get; set; }
        public int Price { get; set; }
        public int Parking { get; set; }
        public int IsRent { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}

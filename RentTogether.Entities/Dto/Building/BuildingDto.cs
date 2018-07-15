//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.Building
{
    public class BuildingDto
    {
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string City2 { get; set; }
        public int OwnerId { get; set; }
        public int Type { get; set; }
        public int NbRoom { get; set; }
        public int NbPiece { get; set; }
        public int NbRenters { get; set; }
        public int NbMaxRenters { get; set; }
        public int IsRent { get; set; }
        public int Area { get; set; }
        public int Price { get; set; }
        public int Parking { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}

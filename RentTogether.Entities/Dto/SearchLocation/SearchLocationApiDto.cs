//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.SearchLocation
{
    public class SearchLocationApiDto
    {
        public int Id { get; set; }
        public string PostalCodeId { get; set; }
        public string Libelle { get; set; }
        public string Libelle2 { get; set; }
    }
}

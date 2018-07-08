using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentTogether.Entities
{
    public class PostalCode
    {
        public int Id { get; set; }
        public string InseeCode { get; set; }
        public string PostalCodeId { get; set; }
        public string Libelle { get; set; }
        public string Libelle2 { get; set; }
        public string Gps { get; set; }
    }
}

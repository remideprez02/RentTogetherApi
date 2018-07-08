using System;
namespace RentTogether.Entities.Dto.TargetLocation
{
    public class TargetLocationDto
    {
        public int UserId { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string City2 { get; set; }
    }
}

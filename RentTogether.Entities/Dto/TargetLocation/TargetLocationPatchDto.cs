using System;
namespace RentTogether.Entities.Dto.TargetLocation
{
    public class TargetLocationPatchDto
    {
        public int UserId { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}

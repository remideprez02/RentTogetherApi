using System;
namespace RentTogether.Entities.Dto.BuildingMessage
{
    public class BuildingMessageDto
    {
        public int BuildingMessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public int IsReport { get; set; }
    }
}

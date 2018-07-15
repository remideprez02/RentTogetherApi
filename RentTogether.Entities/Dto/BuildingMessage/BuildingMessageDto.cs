//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities.Dto.BuildingMessage
{
    public class BuildingMessageDto
    {
        public string MessageText { get; set; }
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public int IsReport { get; set; }
    }
}

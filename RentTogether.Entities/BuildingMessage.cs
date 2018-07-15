//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Entities
{
    public class BuildingMessage
    {
        public int BuildingMessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
        public User Writer { get; set; }
		public Building Building { get; set; }
        public int IsReport { get; set; }
    }
}

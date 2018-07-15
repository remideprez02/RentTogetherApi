//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
namespace RentTogether.Api.Models
{
    public class PrivateMessageViewModel
    {
        public string MessageText { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int ConversationId { get; set; }
    }
}

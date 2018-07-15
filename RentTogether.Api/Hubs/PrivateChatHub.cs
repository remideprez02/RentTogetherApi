//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using Microsoft.AspNetCore.SignalR;
using RentTogether.Api.Models;

namespace RentTogether.Api.Hubs
{
    public class PrivateChatHub : Hub
    {
        public void Send(PrivateMessageViewModel privateMessageViewModel){
            

        }
    }
}

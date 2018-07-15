//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using Microsoft.AspNetCore.SignalR;

namespace RentTogether.Api.Hubs
{
    public class PublicChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", name, message);
        }
    }
}

//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Interfaces.Business;

namespace RentTogether.Api.Hubs
{
    public class PublicChatHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IBuildingService _buildingService;
        private readonly ILogger<PublicChatHub> _logger;

        public PublicChatHub(IUserService userService, IBuildingService buildingService, ILogger<PublicChatHub> logger)
        {
            _userService = userService;
            _buildingService = buildingService;
            _logger = logger;
        }

        public async void Send(int buildingId, int userId, string name, string message)
        {
            try
            {
                var user = _userService.GetUserApiDtoAsyncById(userId);

                if (user == null)
                    return;

                var buidlingMessage = new BuildingMessageDto()
                {
                    BuildingId = buildingId,
                    IsReport = 0,
                    MessageText = message,
                    UserId = userId
                };

                var buildingApiDto = await _buildingService.PostAsyncBuildingMessage(buidlingMessage);
                if (buildingApiDto == null)
                    return;

                // Call the broadcastMessage method to update clients.
                await Clients.All.SendAsync("broadcastMessage", name, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, "An error occurred while sending message.");
                throw new Exception(ex.Message, ex);
            }

        }
    }
}

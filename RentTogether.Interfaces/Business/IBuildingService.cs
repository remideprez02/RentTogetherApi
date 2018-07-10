using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Building;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.BuildingUser;

namespace RentTogether.Interfaces.Business
{
    public interface IBuildingService
    {
		Task<BuildingApiDto> PostAsyncBuilding(BuildingDto buildingDto);
        Task<List<BuildingApiDto>> GetAsyncBuilding(int isOwner, int userId);
        Task<BuildingApiDto> GetAsyncBuildingOfRenter(int userId);
        Task<bool> DeleteBuildingAsync(int buildingId, int userId, int isAdmin);

        Task<BuildingMessageApiDto> PostAsyncBuildingMessage(BuildingMessageDto buildingMessageDto);
        Task<List<BuildingMessageApiDto>> GetBuildingMessagesAsync(int buildingId);
        Task<bool> DeleteBuildingMessageAsync(int buildingMessageId);

        Task<BuildingUserApiDto> PostBuildingUserAsync(BuildingUserDto buildingUserDto);
        Task<bool> DeleteBuildingUserAsync(BuildingUserDto buildingUserDto);

        Task<BuildingPictureApiDto> PostBuildingPictureAsync(BuildingPictureDto buildingPictureDto);
        Task<BuildingPictureApiDto> GetBuildingPicturesAsync(int buildingPictureId);
        Task<List<BuildingPictureInformationApiDto>> GetBuildingPictureInformationsAsync(int buildingId);
        Task<bool> DeleteBuildingPictureAsync(int buildingPictureId);
    }
}

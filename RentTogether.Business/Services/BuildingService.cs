using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Building;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.BuildingUser;
using RentTogether.Interfaces.Dal;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Business.Services
{
    public class BuildingService
    {
        private readonly IDal _dal;
        private readonly ICustomEncoder _customEncoder;

        public BuildingService(IDal dal, ICustomEncoder customEncoder)
        {
            _dal = dal;
            _customEncoder = customEncoder;
        }

        #region Building

        public async Task<BuildingApiDto> PostAsyncBuilding(BuildingDto buildingDto)
        {
            var buildingApiDto = await _dal.PostAsyncBuilding(buildingDto);
            return buildingApiDto;
        }

        public async Task<List<BuildingApiDto>> GetAsyncBuildings(int isOwner, int userId)
        {
            if (isOwner == 0)
            {
                var buildingApiDtos = await _dal.GetAsyncBuildingForRenter(userId);
                return buildingApiDtos;
            }
            else
            {
                var buildingApiDtos = await _dal.GetAsyncBuildingsForOwner(userId);
                return buildingApiDtos;
            }
        }

        public async Task<bool> DeleteBuildingAsync(int buildingId, int userId, int isAdmin)
        {
            if (isAdmin == 1)
            {
                return await _dal.DeleteBuildingAsync(buildingId);
            }
            else
            {
                return await _dal.DeleteBuildingForOwnerIdAsync(buildingId, userId);
            }
        }

        public async Task<BuildingApiDto> GetAsyncBuildingOfRenter(int userId){
            return await _dal.GetAsyncBuildingOfRenter(userId);
        }

        #endregion

        #region BuildingPictures

        public async Task<BuildingPictureApiDto> PostBuildingPictureAsync(BuildingPictureDto buildingPictureDto)
        {
            buildingPictureDto.FileToBase64 = _customEncoder.FileToBase64(buildingPictureDto.File);
            var buildingPictureApiDto = await _dal.PostBuildingPictureAsync(buildingPictureDto);

            return buildingPictureApiDto;
        }

        public async Task<List<BuildingPictureApiDto>> GetBuildingPicturesAsync(int buildingId)
        {
            var buildingPictureApiDtos = await _dal.GetBuildingPicturesAsync(buildingId);
            return buildingPictureApiDtos;
        }

        public async Task<bool> DeleteBuildingPictureAsync(int buildingPictureId)
        {
            return  await _dal.DeleteBuildingPictureAsync(buildingPictureId);
        }

        #endregion

        #region BuildingUsers

        public async Task<BuildingUserApiDto> PostBuildingUserAsync(BuildingUserDto buildingUserDto)
        {
            var buildingUser = await _dal.PostBuildingUserAsync(buildingUserDto);
            return buildingUser;
        }

        public async Task<bool> DeleteBuildingUserAsync(BuildingUserDto buildingUserDto)
        {
            var isSuccess = await _dal.DeleteBuildingUserAsync(buildingUserDto);
            return isSuccess;
        }

        #endregion

        #region BuildingMessages

        public async Task<BuildingMessageApiDto> PostAsyncBuildingMessage(BuildingMessageDto buildingMessageDto)
        {
            var buildingMessageApiDto = await _dal.PostAsyncBuildingMessage(buildingMessageDto);
            return buildingMessageApiDto;
        }

        public async Task<List<BuildingMessageApiDto>> GetBuildingMessagesAsync(int buildingId)
        {
            var buildingMessageApiDtos = await _dal.GetBuildingMessagesAsync(buildingId);
            return buildingMessageApiDtos;
        }

        public async Task<bool> DeleteBuildingMessageAsync(int buildingMessageId)
        {
            var isSuccess = await _dal.DeleteBuildingMessageAsync(buildingMessageId);
            return isSuccess;
        }

        #endregion

    }
}

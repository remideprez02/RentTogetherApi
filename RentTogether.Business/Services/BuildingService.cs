//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Building;
using RentTogether.Entities.Dto.BuildingHistory;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.BuildingUser;
using RentTogether.Entities.Dto.FavoriteBuilding;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Business.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IDal _dal;
        private readonly ICustomEncoder _customEncoder;

        public BuildingService(IDal dal, ICustomEncoder customEncoder)
        {
            _dal = dal;
            _customEncoder = customEncoder;
        }

        #region Building

        /// <summary>
        /// Posts the async building.
        /// </summary>
        /// <returns>The async building.</returns>
        /// <param name="buildingDto">Building dto.</param>
        public async Task<BuildingApiDto> PostAsyncBuilding(BuildingDto buildingDto)
        {
            var buildingApiDto = await _dal.PostAsyncBuilding(buildingDto);
            return buildingApiDto;
        }

        /// <summary>
        /// Gets the async building.
        /// </summary>
        /// <returns>The async building.</returns>
        /// <param name="isOwner">Is owner.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<List<BuildingApiDto>> GetAsyncBuilding(int isOwner, int userId)
        {
            if (isOwner == 0)
            {
                return await _dal.GetAsyncBuildingForRenter(userId);
            }

            return await _dal.GetAsyncBuildingsOfOwner(userId);
        }

        /// <summary>
        /// Deletes the building async.
        /// </summary>
        /// <returns>The building async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="isAdmin">Is admin.</param>
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

        /// <summary>
        /// Gets the async building of renter.
        /// </summary>
        /// <returns>The async building of renter.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<BuildingApiDto> GetAsyncBuildingOfRenter(int userId)
        {
            return await _dal.GetAsyncBuildingOfRenter(userId);
        }

        /// <summary>
        /// Updates the building async.
        /// </summary>
        /// <returns>The building async.</returns>
        /// <param name="buildingUpdateDto">Building update dto.</param>
        public async Task<BuildingApiDto> UpdateBuildingAsync(BuildingUpdateDto buildingUpdateDto){
            return await _dal.UpdateBuildingAsync(buildingUpdateDto);
        }

        #endregion

        #region BuildingPictures

        /// <summary>
        /// Posts the building picture async.
        /// </summary>
        /// <returns>The building picture async.</returns>
        /// <param name="buildingPictureDto">Building picture dto.</param>
        public async Task<BuildingPictureApiDto> PostBuildingPictureAsync(BuildingPictureDto buildingPictureDto)
        {
            buildingPictureDto.FileToBase64 = _customEncoder.FileToBase64(buildingPictureDto.File);
            var buildingPictureApiDto = await _dal.PostBuildingPictureAsync(buildingPictureDto);

            return buildingPictureApiDto;
        }

        /// <summary>
        /// Gets the building pictures async.
        /// </summary>
        /// <returns>The building pictures async.</returns>
        /// <param name="buildingPictureId">Building picture identifier.</param>
        public async Task<BuildingPictureApiDto> GetBuildingPicturesAsync(int buildingPictureId)
        {
            var buildingPictureApiDto = await _dal.GetBuildingPicturesAsync(buildingPictureId);
            return buildingPictureApiDto;
        }

        /// <summary>
        /// Gets the building picture informations async.
        /// </summary>
        /// <returns>The building picture informations async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        public async Task<List<BuildingPictureInformationApiDto>> GetBuildingPictureInformationsAsync(int buildingId)
        {
            return await _dal.GetBuildingPictureInformationsAsync(buildingId);
        }

        /// <summary>
        /// Deletes the building picture async.
        /// </summary>
        /// <returns>The building picture async.</returns>
        /// <param name="buildingPictureId">Building picture identifier.</param>
        public async Task<bool> DeleteBuildingPictureAsync(int buildingPictureId)
        {
            return await _dal.DeleteBuildingPictureAsync(buildingPictureId);
        }

        #endregion

        #region BuildingUsers

        /// <summary>
        /// Posts the building user async.
        /// </summary>
        /// <returns>The building user async.</returns>
        /// <param name="buildingUserDto">Building user dto.</param>
        public async Task<BuildingUserApiDto> PostBuildingUserAsync(BuildingUserDto buildingUserDto)
        {
            var buildingUser = await _dal.PostBuildingUserAsync(buildingUserDto);
            return buildingUser;
        }

        /// <summary>
        /// Deletes the building user async.
        /// </summary>
        /// <returns>The building user async.</returns>
        /// <param name="buildingUserDto">Building user dto.</param>
        public async Task<bool> DeleteBuildingUserAsync(BuildingUserDto buildingUserDto)
        {
            var isSuccess = await _dal.DeleteBuildingUserAsync(buildingUserDto);
            return isSuccess;
        }

        #endregion

        #region BuildingMessages

        /// <summary>
        /// Posts the async building message.
        /// </summary>
        /// <returns>The async building message.</returns>
        /// <param name="buildingMessageDto">Building message dto.</param>
        public async Task<BuildingMessageApiDto> PostAsyncBuildingMessage(BuildingMessageDto buildingMessageDto)
        {
            var buildingMessageApiDto = await _dal.PostAsyncBuildingMessage(buildingMessageDto);
            return buildingMessageApiDto;
        }

        /// <summary>
        /// Gets the building messages async.
        /// </summary>
        /// <returns>The building messages async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        public async Task<List<BuildingMessageApiDto>> GetBuildingMessagesAsync(int buildingId)
        {
            var buildingMessageApiDtos = await _dal.GetBuildingMessagesAsync(buildingId);
            return buildingMessageApiDtos;
        }

        /// <summary>
        /// Deletes the building message async.
        /// </summary>
        /// <returns>The building message async.</returns>
        /// <param name="buildingMessageId">Building message identifier.</param>
        public async Task<bool> DeleteBuildingMessageAsync(int buildingMessageId)
        {
            var isSuccess = await _dal.DeleteBuildingMessageAsync(buildingMessageId);
            return isSuccess;
        }

        #endregion

        #region BuildingHistory

        /// <summary>
        /// Posts the building history async.
        /// </summary>
        /// <returns>The building history async.</returns>
        /// <param name="buildingHistoryDto">Building history dto.</param>
        public async Task<BuildingHistoryApiDto> PostBuildingHistoryAsync(BuildingHistoryDto buildingHistoryDto){
            return await _dal.PostBuildingHistoryAsync(buildingHistoryDto);
        }

        /// <summary>
        /// Gets the building histories by user identifier async.
        /// </summary>
        /// <returns>The building histories by user identifier async.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<BuildingHistoryApiDto>> GetBuildingHistoriesByUserIdAsync(int userId){
            return await _dal.GetBuildingHistoriesByUserIdAsync(userId);
        }
        #endregion

        #region FavoriteBuilding

        /// <summary>
        /// Posts the favorite building async.
        /// </summary>
        /// <returns>The favorite building async.</returns>
        /// <param name="favoriteBuildingDto">Favorite building dto.</param>
        public async Task<FavoriteBuildingApiDto> PostFavoriteBuildingAsync(FavoriteBuildingDto favoriteBuildingDto){
            return await _dal.PostFavoriteBuildingAsync(favoriteBuildingDto);
        }

        /// <summary>
        /// Gets the favorite buildings by user identifier async.
        /// </summary>
        /// <returns>The favorite buildings by user identifier async.</returns>
        /// <param name="userId">User identifier.</param>
        public async Task<List<BuildingApiDto>> GetFavoriteBuildingsByUserIdAsync(int userId){
            return await _dal.GetFavoriteBuildingsByUserIdAsync(userId);
        }

        /// <summary>
        /// Deletes the favorite building by building identifier async.
        /// </summary>
        /// <returns>The favorite building by building identifier async.</returns>
        /// <param name="buildingId">Building identifier.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<bool> DeleteFavoriteBuildingByBuildingIdAsync(int buildingId, int userId){
            return await _dal.DeleteFavoriteBuildingByBuildingIdAsync(buildingId, userId);
        }
        #endregion

    }
}

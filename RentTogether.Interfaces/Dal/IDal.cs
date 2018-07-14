using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Building;
using RentTogether.Entities.Dto.BuildingHistory;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.BuildingUser;
using RentTogether.Entities.Dto.Conversation;
using RentTogether.Entities.Dto.FavoriteBuilding;
using RentTogether.Entities.Dto.Match;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Entities.Dto.Personality;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Entities.Dto.SearchLocation;
using RentTogether.Entities.Dto.TargetLocation;

namespace RentTogether.Interfaces.Dal
{
    public interface IDal
    {
        #region User
        Task CreateUserAsync(User user);
        Task<bool> CheckIfUserAlreadyExistAsync(User newUser);
        Task<User> GetUserAsyncById(int id);
        Task<User> GetUserByBasicAuthenticationAsync(UserLoginDto userLoginDto);
        Task<DateTime> GetUserTokenExpirationDateAsync(string token);
        Task<bool> DeleteUserByIdAsync(int userId);
        Task<UserApiDto> UpdateUserAsync(UserApiDto userApiDto);
        Task<List<UserApiDto>> GetAllUserAsync();
        Task<UserApiDto> GetUserAsyncByToken(string token);
        Task<UserApiDto> PatchUser(UserPatchApiDto userPatchApiDto);
        #endregion

        #region Message
        Task<List<MessageApiDto>> GetMessagesAsyncByUserId(int userId);
        Task<MessageApiDto> AddMessageAsync(MessageDto messageDto);
        Task<List<MessageApiDto>> GetAllMessagesAsyncFromConversationByConversationId(int conversationId);
        #endregion

        #region Conversation
        Task<List<ConversationApiDto>> GetConversationsAsyncByUserId(int userId);
        Task<ConversationApiDto> AddConversationAsync(ConversationDto conversationDto);
        Task<List<ConversationApiDto>> GetAllConversationsAsync();
        #endregion

        #region Participant 
        Task<ParticipantApiDto> GetParticipantAsyncByUserId(int userId);
        Task<List<ParticipantApiDto>> GetAllParticipantAsync();
        Task<List<ParticipantApiDto>> PostAsyncParticipantToExistingConversation(List<ParticipantDto> participantDtos);
        #endregion

        #region Media
        Task<FileApiDto> PostAsyncUserPicture(FileDto fileDto);
        Task<FileApiDto> GetAsyncUserPictureByUserId(int userId);
        #endregion

        #region Personality
        Task<DetailPersonalityApiDto> PostAsyncDetailPersonality(DetailPersonalityDto detailPersonalityDto);
        Task<List<DetailPersonalityApiDto>> GetAsyncAllPersonalityReferencials();
        Task<List<PersonalityValueApiDto>> PatchAsyncPersonalityValuesByUserId(int userId, List<PersonalityValuePatchDto> personalityValuePatchDtos);
        Task<List<PersonalityValueApiDto>> PostAsyncPersonalityValues(List<PersonalityValueDto> personalityValueDtos, int userId);
        Task<PersonalityApiDto> GetPersonalityAsyncByUserId(int userId);
        #endregion

        #region Match
        Task<MatchApiDto> PostAsyncMatch(MatchDto matchDto);
        Task<List<MatchApiDto>> GetAsyncAllMatches(int userId);
        Task<List<MatchApiDto>> GetAsyncListMatches(int userId);
        Task<List<MatchApiDto>> PatchAsyncMatches(int userId);
        Task<bool> DeleteAsyncMatch(int matchId);
        Task<List<MatchApiDto>> GetAsyncValidateMatches(int userId);
        #endregion

        #region TargetLocation
        Task<List<TargetLocationApiDto>> GetAsyncTargetLocationsByUserId(int userId);
        Task<List<TargetLocationApiDto>> PostAsyncTargetLocation(List<TargetLocationDto> targetLocationDtos, int userId);
        Task<List<TargetLocationApiDto>> PatchAsyncTargetLocation(List<TargetLocationPatchDto> targetLocationPatchDtos, int userId);
        Task<bool> DeleteAsyncTargetLocation(int targetLocationId);
        #endregion

        #region Building
        Task<BuildingApiDto> PostAsyncBuilding(BuildingDto buildingDto);
        Task<List<BuildingApiDto>> GetAsyncBuildingsOfOwner(int userId);
        Task<List<BuildingApiDto>> GetAsyncBuildingForRenter(int userId);
        Task<BuildingApiDto> GetAsyncBuildingOfRenter(int userId);
        Task<bool> DeleteBuildingAsync(int buildingId);
        Task<bool> DeleteBuildingForOwnerIdAsync(int buildingId, int ownerId);

        Task<BuildingMessageApiDto> PostAsyncBuildingMessage(BuildingMessageDto buildingMessageDto);
        Task<List<BuildingMessageApiDto>> GetBuildingMessagesAsync(int buildingId);
        Task<bool> DeleteBuildingMessageAsync(int buildingMessageId);

        Task<BuildingUserApiDto> PostBuildingUserAsync(BuildingUserDto buildingUserDto);
        Task<bool> DeleteBuildingUserAsync(BuildingUserDto buildingUserDto);

        Task<BuildingPictureApiDto> PostBuildingPictureAsync(BuildingPictureDto buildingPictureDto);
        Task<BuildingPictureApiDto> GetBuildingPicturesAsync(int buildingPictureId);
        Task<List<BuildingPictureInformationApiDto>> GetBuildingPictureInformationsAsync(int buildingId);

        Task<bool> DeleteBuildingPictureAsync(int buildingPictureId);
        #endregion

        #region SearchLocation
        Task<List<SearchLocationApiDto>> GetSearchLocationsAsync(SearchLocationDto searchLocationDto);
        #endregion

        #region BuildingHistory
        Task<BuildingHistoryApiDto> PostBuildingHistoryAsync(BuildingHistoryDto buildingHistoryDto);
        #endregion

        #region FavoriteBuilding
        Task<FavoriteBuildingApiDto> PostFavoriteBuildingAsync(FavoriteBuildingDto favoriteBuildingDto);
        Task<List<BuildingApiDto>> GetFavoriteBuildingsByUserIdAsync(int userId);
        Task<bool> DeleteFavoriteBuildingByBuildingIdAsync(int buildingId, int userId);
        #endregion
    }
}

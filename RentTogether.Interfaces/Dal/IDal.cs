using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Conversation;
using RentTogether.Entities.Dto.Match;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Entities.Dto.Personality;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Entities.Filters.Users;

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
        Task<ParticipantApiDto> PostAsyncParticipantToExistingConversation(ParticipantDto participantDto);
        #endregion

        #region Media
        Task<FileApiDto> PostAsyncUserPicture(FileDto fileDto);
        Task<FileApiDto> GetAsyncUserPictureByUserId(int userId);
        #endregion

        #region Personality
        //Referencial
        Task<DetailPersonalityApiDto> PostAsyncDetailPersonality(DetailPersonalityDto detailPersonalityDto);
        Task<List<DetailPersonalityApiDto>> GetAsyncAllPersonalityReferencials();

        //Value/Personality
        Task<List<PersonalityValueApiDto>> PostAsyncPersonalityValues(List<PersonalityValueDto> personalityValueDtos, int userId);
        Task<PersonalityApiDto> GetPersonalityAsyncByUserId(int userId);
        #endregion

        #region Match
        Task<MatchApiDto> PostAsyncMatch(MatchDto matchDto);
        Task<List<MatchApiDto>> GetAsyncListMatches(int userId);
        #endregion
    }
}

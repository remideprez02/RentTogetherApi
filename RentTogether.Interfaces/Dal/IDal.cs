using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Conversation;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
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
		Task AddMessageAsync(MessageDto messageDto);
		#endregion

		#region Conversation
		Task<ConversationApiDto> GetConversationAsyncByUserId(int conversationId);
		Task<ConversationApiDto> AddConversationAsync(ConversationDto conversationDto);
		Task<List<ConversationApiDto>> GetAllConversationsAsync();
		#endregion

		#region Participant 
		Task<ParticipantApiDto> GetParticipantAsyncByUserId(int userId);
		Task<List<ParticipantApiDto>> GetAllParticipantAsync();
		Task<ParticipantApiDto> PostAsyncParticipantToExistingConversation(ParticipantDto participantDto);
		#endregion
	}
}

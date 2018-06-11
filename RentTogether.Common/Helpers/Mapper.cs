using System;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Common.Mapper
{
	public class Mapper : IMapperHelper
	{
		public Mapper()
		{
		}
		#region User

		public User MapUserRegisterDtoToUser(UserRegisterDto userRegisterDto)
		{

			return new User()
			{
				Email = userRegisterDto.Email,
				FirstName = userRegisterDto.FirstName,
				LastName = userRegisterDto.LastName,
				Password = userRegisterDto.Password,
				City = userRegisterDto.City,
				PostalCode = userRegisterDto.PostalCode,
				IsOwner = userRegisterDto.IsOwner,
				IsRoomer = userRegisterDto.IsRoomer,
				IsAdmin = userRegisterDto.IsAdmin,
				PhoneNumber = userRegisterDto.PhoneNumber,
			};
		}

		public UserApiDto MapUserToUserApiDto(User user)
		{

			return new UserApiDto()
			{
				Email = user.Email,
				CreateDate = user.CreateDate,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Password = user.Password,
				City = user.City,
				PostalCode = user.PostalCode,
				IsOwner = user.IsOwner,
				IsRoomer = user.IsRoomer,
				IsAdmin = user.IsAdmin,
				PhoneNumber = user.PhoneNumber,
				Token = user.Token,
				UserId = user.UserId,
				TokenExpirationDate = user.TokenExpirationDate
			};
		}

		public User MapUpdateUserApiDtoToUser(UserApiDto userApiDto, User user)
		{

			user.Email = userApiDto.Email;
			user.FirstName = userApiDto.FirstName;
			user.LastName = userApiDto.LastName;
			user.IsOwner = userApiDto.IsOwner;
			user.IsRoomer = userApiDto.IsRoomer;
			user.IsAdmin = userApiDto.IsAdmin;
			user.Password = userApiDto.Password;
			user.PhoneNumber = userApiDto.PhoneNumber;
			user.City = userApiDto.City;
			user.PostalCode = userApiDto.PostalCode;
			user.Token = userApiDto.Token;
			user.TokenExpirationDate = userApiDto.TokenExpirationDate;
			return user;
		}
		#endregion

		#region Participant
		public ParticipantApiDto MapParticipantToParticipantApiDto(Participant participant)
		{

			return new ParticipantApiDto()
			{
				ConversationId = participant.Conversation.ConversationId,
				UserId = participant.User.UserId,
				EndDate = participant.EndDate,
				ParticipantId = participant.ParticipantId,
				StartDate = participant.StartDate
			};
		}
		#endregion

		#region Message
		public MessageApiDto MapMessageToMessageApiDto(Message message){
			return new MessageApiDto()
			{
				CreatedDate = message.CreatedDate,
				IsReport = message.IsReport,
				MessageId = message.MessageId,
				MessageText = message.MessageText,
				UserId = message.Editor.UserId
			};
		}

#endregion
	}
}

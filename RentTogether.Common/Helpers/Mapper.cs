using System;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Common.Mapper
{
    public class Mapper : IMapperHelper
    {
        private ICustomEncoder _customEncoder { get; set; }

        public Mapper(ICustomEncoder customEncoder)
        {
            _customEncoder = customEncoder;
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
                UserApiDto = new UserApiDto()
                {
                    Email = participant.User.Email,
                    CreateDate = participant.User.CreateDate,
                    FirstName = participant.User.FirstName,
                    LastName = participant.User.LastName,
                    Password = participant.User.Password,
                    City = participant.User.City,
                    PostalCode = participant.User.PostalCode,
                    IsOwner = participant.User.IsOwner,
                    IsRoomer = participant.User.IsRoomer,
                    IsAdmin = participant.User.IsAdmin,
                    PhoneNumber = participant.User.PhoneNumber,
                    Token = participant.User.Token,
                    UserId = participant.User.UserId,
                    TokenExpirationDate = participant.User.TokenExpirationDate   
                },
                EndDate = participant?.EndDate,
                ParticipantId = participant.ParticipantId,
                StartDate = participant.StartDate
            };
        }
        #endregion

        #region Message
        public MessageApiDto MapMessageToMessageApiDto(Message message)
        {
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

        #region UserPicture

        public FileApiDto MapUserPictureToFileApiDto(UserPicture userPicture)
        {
            return new FileApiDto()
            {
                FileId = userPicture.UserPictureId,
                FileToBase64 = userPicture.FileToBase64,
                UserId = userPicture.UserPictureId
            };
        }

        #endregion

        #region Personality

        public DetailPersonalityApiDto MapPersonalityReferencialToDetailPersonalityApiDto(PersonalityReferencial personalityReferencial)
        {
            return new DetailPersonalityApiDto()
            {
                Description1 = personalityReferencial.Description1,
                Description2 = personalityReferencial.Description2,
                Description3 = personalityReferencial.Description3,
                Description4 = personalityReferencial.Description4,
                Description5 = personalityReferencial.Description5,
                Name = personalityReferencial.Name,
                PersonalityReferencialId = personalityReferencial.PersonalityReferencialId
            };
        }

        public PersonalityValueApiDto MapPersonalityValueToPersonalityValueApiDto(PersonalityValue personalityValue)
        {
            return new PersonalityValueApiDto()
            {
                PersonalityReferencialId = personalityValue.PersonalityReferencial.PersonalityReferencialId,
                PersonalityValueId = personalityValue.PersonalityValueId,
                Value = personalityValue.Value
            };
        }
        #endregion
    }
}

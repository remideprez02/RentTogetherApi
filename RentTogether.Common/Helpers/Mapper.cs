using System;
using System.Collections.Generic;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Building;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.BuildingUser;
using RentTogether.Entities.Dto.Match;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Entities.Dto.TargetLocation;
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
                City = user.City,
                PostalCode = user.PostalCode,
                Description = user.Description,
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
            user.Description = userApiDto.Description;
            user.Email = userApiDto.Email;
            user.FirstName = userApiDto.FirstName;
            user.LastName = userApiDto.LastName;
            user.IsOwner = userApiDto.IsOwner;
            user.IsRoomer = userApiDto.IsRoomer;
            user.IsAdmin = userApiDto.IsAdmin;
            user.PhoneNumber = userApiDto.PhoneNumber;
            user.City = userApiDto.City;
            user.PostalCode = userApiDto.PostalCode;
            user.Token = userApiDto.Token;
            user.TokenExpirationDate = userApiDto.TokenExpirationDate;
            return user;
        }

        public User MapUserPatchApiDtoToUser(User user, UserPatchApiDto userPatchApiDto)
        {
            if (!string.IsNullOrEmpty(userPatchApiDto.PhoneNumber))
                user.PhoneNumber = userPatchApiDto.PhoneNumber;

            if (!string.IsNullOrEmpty(userPatchApiDto.City))
                user.City = userPatchApiDto.City;

            if (!string.IsNullOrEmpty(userPatchApiDto.Email))
                user.Email = userPatchApiDto.Email;

            if (!string.IsNullOrEmpty(userPatchApiDto.FirstName))
                user.FirstName = userPatchApiDto.FirstName;

            if (!string.IsNullOrEmpty(userPatchApiDto.LastName))
                user.LastName = userPatchApiDto.LastName;

            if (userPatchApiDto.IsAdmin.HasValue)
                user.IsAdmin = userPatchApiDto.IsAdmin.Value;

            if (userPatchApiDto.IsOwner.HasValue)
                user.IsOwner = userPatchApiDto.IsOwner.Value;

            if (userPatchApiDto.IsRoomer.HasValue)
                user.IsRoomer = userPatchApiDto.IsRoomer.Value;

            if (!string.IsNullOrEmpty(userPatchApiDto.PostalCode))
                user.PostalCode = userPatchApiDto.PostalCode;

            if (!string.IsNullOrEmpty(userPatchApiDto.Password))
                user.Password = userPatchApiDto.Password;

            if (!string.IsNullOrEmpty(userPatchApiDto.Description))
                user.Description = userPatchApiDto.Description;

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

        #region Match   

        public MatchApiDto MapMatchToMatchApiDto(Match match)
        {
            var matchApiDto = new MatchApiDto()
            {
                UserId = match.User.UserId,
                TargetUser = MapUserToUserApiDto(match.TargetUser),
                MatchId = match.MatchId,
                StatusUser = match.StatusUser,
                StatusTargetUser = match.StatusTargetUser,
                Average = match.Average
            };
            matchApiDto.MatchDetailApiDtos = new System.Collections.Generic.List<MatchDetailApiDto>();
            foreach (var item in match.MatchDetails)
            {
                matchApiDto.MatchDetailApiDtos.Add(new MatchDetailApiDto()
                {
                    DetailPersonalityApiDto = MapPersonalityReferencialToDetailPersonalityApiDto(item.PersonalityReferencial),
                    Percent = item.Percent,
                    Value = item.Value
                });
            }
            return matchApiDto;
        }

        public MatchDetailApiDto MapMapDetailToMatchDetailApiDto(MatchDetail matchDetail)
        {
            return new MatchDetailApiDto()
            {
                DetailPersonalityApiDto = MapPersonalityReferencialToDetailPersonalityApiDto(matchDetail.PersonalityReferencial),
                Percent = matchDetail.Percent,
                Value = matchDetail.Value
            };
        }

        #endregion

        #region TargetLocation
        public TargetLocationApiDto MapTargetLocationToTargetLocationApiDto(TargetLocation targetLocation)
        {
            return new TargetLocationApiDto()
            {
                City = targetLocation.City,
                TargetLocationId = targetLocation.TargetLocationId,
                UserId = targetLocation.User.UserId,
                PostalCode = targetLocation.PostalCode
            };
        }
        #endregion

        #region Building

        public BuildingPictureApiDto MapBuildingPictureToBuildingPictureApiDto(BuildingPicture buildingPicture){
            return new BuildingPictureApiDto()
            {
                BuildingId = buildingPicture.Building.BuildingId,
                BuildingPictureId = buildingPicture.BuildingPictureId,
                FileToBase64 = buildingPicture.FileToBase64
            };
        }

        public BuildingUserApiDto MapBuildingUserToBuildingUserApiDto(BuildingUser buildingUser){
            return new BuildingUserApiDto()
            {
                BuildingId = buildingUser.BuildingId,
                UserId = buildingUser.UserId
            };
        }

        public BuildingApiDto MapBuildingToBuildingApiDto(Building building){
            var buildingApiDto = new BuildingApiDto
            {
                Address = building.Address,
                Address2 = building.Address2,
                Area = building.Area,
                City = building.City,
                Description = building.Description,
                NbPiece = building.NbPiece,
                NbRenters = building.NbRenters,
                NbRoom = building.NbRoom,
                OwnerApiDto = MapUserToUserApiDto(building.Owner),
                Parking = building.Parking,
                PostalCode = building.PostalCode,
                Price = building.Price,
                Status = building.Status,
                Title = building.Title,
                Type = building.Type,
                IsRent = building.IsRent,
                NbMaxRenters = building.NbMaxRenters
            };

            buildingApiDto.BuildingPictureApiDtos = new List<BuildingPictureApiDto>();

            foreach (var buildingPicture in building.BuildingPictures)
            {
                buildingApiDto.BuildingPictureApiDtos.Add(MapBuildingPictureToBuildingPictureApiDto(buildingPicture));
            }

            buildingApiDto.BuildingUserApiDtos = new List<BuildingUserApiDto>();

            foreach(var buildingUser in building.BuildingUsers){
                buildingApiDto.BuildingUserApiDtos.Add(MapBuildingUserToBuildingUserApiDto(buildingUser));
            }

            return buildingApiDto;
        }
        #endregion
    }
}

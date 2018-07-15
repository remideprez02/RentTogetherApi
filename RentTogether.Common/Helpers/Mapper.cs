//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Building;
using RentTogether.Entities.Dto.BuildingHistory;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.BuildingUser;
using RentTogether.Entities.Dto.Match;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Entities.Dto.SearchLocation;
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

        /// <summary>
        /// Maps the user register dto to user.
        /// </summary>
        /// <returns>The user register dto to user.</returns>
        /// <param name="userRegisterDto">User register dto.</param>
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

        /// <summary>
        /// Maps the user to user API dto.
        /// </summary>
        /// <returns>The user to user API dto.</returns>
        /// <param name="user">User.</param>
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

        /// <summary>
        /// Maps the update user API dto to user.
        /// </summary>
        /// <returns>The update user API dto to user.</returns>
        /// <param name="userApiDto">User API dto.</param>
        /// <param name="user">User.</param>
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

        /// <summary>
        /// Maps the user patch API dto to user.
        /// </summary>
        /// <returns>The user patch API dto to user.</returns>
        /// <param name="user">User.</param>
        /// <param name="userPatchApiDto">User patch API dto.</param>
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

        /// <summary>
        /// Maps the participant to participant API dto.
        /// </summary>
        /// <returns>The participant to participant API dto.</returns>
        /// <param name="participant">Participant.</param>
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

        /// <summary>
        /// Maps the message to message API dto.
        /// </summary>
        /// <returns>The message to message API dto.</returns>
        /// <param name="message">Message.</param>
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

        /// <summary>
        /// Maps the user picture to file API dto.
        /// </summary>
        /// <returns>The user picture to file API dto.</returns>
        /// <param name="userPicture">User picture.</param>
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

        /// <summary>
        /// Maps the personality referencial to detail personality API dto.
        /// </summary>
        /// <returns>The personality referencial to detail personality API dto.</returns>
        /// <param name="personalityReferencial">Personality referencial.</param>
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

        /// <summary>
        /// Maps the personality value to personality value API dto.
        /// </summary>
        /// <returns>The personality value to personality value API dto.</returns>
        /// <param name="personalityValue">Personality value.</param>
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

        /// <summary>
        /// Maps the match to match API dto.
        /// </summary>
        /// <returns>The match to match API dto.</returns>
        /// <param name="match">Match.</param>
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

        /// <summary>
        /// Maps the map detail to match detail API dto.
        /// </summary>
        /// <returns>The map detail to match detail API dto.</returns>
        /// <param name="matchDetail">Match detail.</param>
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

        /// <summary>
        /// Maps the target location to target location API dto.
        /// </summary>
        /// <returns>The target location to target location API dto.</returns>
        /// <param name="targetLocation">Target location.</param>
        public TargetLocationApiDto MapTargetLocationToTargetLocationApiDto(TargetLocation targetLocation)
        {
            return new TargetLocationApiDto()
            {
                City = targetLocation.City,
                TargetLocationId = targetLocation.TargetLocationId,
                UserId = targetLocation.User.UserId,
                PostalCode = targetLocation.PostalCode,
                City2 = targetLocation.City2
            };
        }
        #endregion

        #region Building

        /// <summary>
        /// Maps the building picture to building picture information API dto.
        /// </summary>
        /// <returns>The building picture to building picture information API dto.</returns>
        /// <param name="buildingPicture">Building picture.</param>
        public BuildingPictureInformationApiDto MapBuildingPictureToBuildingPictureInformationApiDto(BuildingPicture buildingPicture)
        {
            return new BuildingPictureInformationApiDto()
            {
                BuildingId = buildingPicture.Building.BuildingId,
                BuildingPictureId = buildingPicture.BuildingPictureId
            };
        }

        /// <summary>
        /// Maps the building message to building message API dto.
        /// </summary>
        /// <returns>The building message to building message API dto.</returns>
        /// <param name="buildingMessage">Building message.</param>
        public BuildingMessageApiDto MapBuildingMessageToBuildingMessageApiDto(BuildingMessage buildingMessage)
        {
            return new BuildingMessageApiDto()
            {
                BuildingId = buildingMessage.Building.BuildingId,
                BuildingMessageId = buildingMessage.BuildingMessageId,
                CreatedDate = buildingMessage.CreatedDate,
                IsReport = buildingMessage.IsReport,
                MessageText = buildingMessage.MessageText,
                Writer = MapUserToUserApiDto(buildingMessage.Writer)
            };
        }

        /// <summary>
        /// Maps the building picture to building picture API dto.
        /// </summary>
        /// <returns>The building picture to building picture API dto.</returns>
        /// <param name="buildingPicture">Building picture.</param>
        public BuildingPictureApiDto MapBuildingPictureToBuildingPictureApiDto(BuildingPicture buildingPicture)
        {
            return new BuildingPictureApiDto()
            {
                BuildingId = buildingPicture.Building.BuildingId,
                BuildingPictureId = buildingPicture.BuildingPictureId,
                FileToBase64 = buildingPicture.FileToBase64
            };
        }

        /// <summary>
        /// Maps the building user to building user API dto.
        /// </summary>
        /// <returns>The building user to building user API dto.</returns>
        /// <param name="buildingUser">Building user.</param>
        public BuildingUserApiDto MapBuildingUserToBuildingUserApiDto(BuildingUser buildingUser)
        {
            return new BuildingUserApiDto()
            {
                BuildingId = buildingUser.BuildingId,
                UserId = buildingUser.UserId
            };
        }

        /// <summary>
        /// Maps the building to building API dto.
        /// </summary>
        /// <returns>The building to building API dto.</returns>
        /// <param name="building">Building.</param>
        public BuildingApiDto MapBuildingToBuildingApiDto(Building building)
        {
            var buildingApiDto = new BuildingApiDto
            {
                BuildingId = building.BuildingId,
                Address = building.Address,
                Address2 = building.Address2,
                Area = building.Area,
                City = building.City,
                City2 = building.City2,
                Description = building.Description,
                NbPiece = building.NbPiece,
                NbRenters = building.NbRenters,
                NbRoom = building.NbRoom,
                OwnerApiDto = MapUserToUserApiDto(building.Owner),
                Parking = building.Parking,
                PostalCode = building.PostalCode,
                Price = building.Price,
                Title = building.Title,
                Type = building.Type,
                IsRent = building.IsRent,
                NbMaxRenters = building.NbMaxRenters
            };

            if (building.BuildingPictures != null)
            {
                buildingApiDto.BuildingPictureInformationApiDtos = new List<BuildingPictureInformationApiDto>();

                foreach (var buildingPicture in building.BuildingPictures)
                {
                    buildingApiDto.BuildingPictureInformationApiDtos.Add(MapBuildingPictureToBuildingPictureInformationApiDto(buildingPicture));
                }
            }

            if (building.BuildingUsers != null)
            {
                buildingApiDto.BuildingUserApiDtos = new List<BuildingUserApiDto>();

                foreach (var buildingUser in building.BuildingUsers)
                {
                    buildingApiDto.BuildingUserApiDtos.Add(MapBuildingUserToBuildingUserApiDto(buildingUser));
                }
            }

            buildingApiDto.BuildingMessageApiDtos = new List<BuildingMessageApiDto>();

            return buildingApiDto;
        }

        /// <summary>
        /// Maps the building history to building history API dto.
        /// </summary>
        /// <returns>The building history to building history API dto.</returns>
        /// <param name="buildingHistory">Building history.</param>
        public BuildingHistoryApiDto MapBuildingHistoryToBuildingHistoryApiDto(BuildingHistory buildingHistory)
        {
            return new BuildingHistoryApiDto()
            {
                BuildingHistoryId = buildingHistory.BuildingHistoryId,
                BuildingId = buildingHistory.Building.BuildingId,
                HasSeen = buildingHistory.HasSeen,
                UserId = buildingHistory.User.UserId
            };
        }

        /// <summary>
        /// Maps the building update dto to building.
        /// </summary>
        /// <returns>The building update dto to building.</returns>
        /// <param name="buildingUpdateDto">Building update dto.</param>
        /// <param name="building">Building.</param>
        public Building MapBuildingUpdateDtoToBuilding(BuildingUpdateDto buildingUpdateDto, Building building)
        {
            if (!string.IsNullOrEmpty(buildingUpdateDto.Address))
                building.Address = buildingUpdateDto.Address;

            if (!string.IsNullOrEmpty(buildingUpdateDto.Address2))
                building.Address2 = buildingUpdateDto.Address2;

            if (!string.IsNullOrEmpty(buildingUpdateDto.City))
                building.City = buildingUpdateDto.City;

            if (!string.IsNullOrEmpty(buildingUpdateDto.City2))
                building.City2 = buildingUpdateDto.City2;

            if (!string.IsNullOrEmpty(buildingUpdateDto.PostalCode))
                building.PostalCode = buildingUpdateDto.PostalCode;

            if (!string.IsNullOrEmpty(buildingUpdateDto.Description))
                building.Description = buildingUpdateDto.Description;

            if (!string.IsNullOrEmpty(buildingUpdateDto.Title))
                building.Title = buildingUpdateDto.Title;

            building.IsRent = buildingUpdateDto.IsRent ?? building.IsRent;
            building.NbMaxRenters = buildingUpdateDto.NbMaxRenters ?? building.NbMaxRenters;
            building.NbPiece = buildingUpdateDto.NbPiece ?? building.NbPiece;
            building.NbRenters = buildingUpdateDto.NbRenters ?? building.NbRenters;
            building.NbRoom = buildingUpdateDto.NbRoom ?? building.NbRoom;
            building.Parking = buildingUpdateDto.Parking ?? building.Parking;
            building.Price = buildingUpdateDto.Price ?? building.Price;
            building.Type = buildingUpdateDto.Type ?? building.Type;
            building.Area = buildingUpdateDto.Area ?? building.Area;

            return building;
        }
        #endregion

        #region SearchLocation

        /// <summary>
        /// Maps the postal code to search location.
        /// </summary>
        /// <returns>The postal code to search location.</returns>
        /// <param name="postalCodes">Postal codes.</param>
        public List<SearchLocationApiDto> MapPostalCodeToSearchLocation(List<PostalCode> postalCodes)
        {
            var searchLocationApiDtos = new List<SearchLocationApiDto>();

            foreach (var postalCode in postalCodes)
            {
                searchLocationApiDtos.Add(new SearchLocationApiDto()
                {
                    Id = postalCode.Id,
                    Libelle = postalCode.Libelle,
                    Libelle2 = postalCode.Libelle2,
                    PostalCodeId = postalCode.PostalCodeId
                });
            }
            return searchLocationApiDtos;
        }
        #endregion
    }
}

using System;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Building;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Entities.Dto.BuildingUser;
using RentTogether.Entities.Dto.Match;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Entities.Dto.TargetLocation;

namespace RentTogether.Interfaces.Helpers
{
    public interface IMapperHelper
    {
        User MapUserRegisterDtoToUser(UserRegisterDto userRegisterDto);
        UserApiDto MapUserToUserApiDto(User user);
        User MapUpdateUserApiDtoToUser(UserApiDto userApiDto, User user);
        User MapUserPatchApiDtoToUser(User user, UserPatchApiDto userPatchApiDto);

		ParticipantApiDto MapParticipantToParticipantApiDto(Participant participant);

		MessageApiDto MapMessageToMessageApiDto(Message message);
		FileApiDto MapUserPictureToFileApiDto(UserPicture userPicture);

        DetailPersonalityApiDto MapPersonalityReferencialToDetailPersonalityApiDto(PersonalityReferencial personalityReferencial);
        PersonalityValueApiDto MapPersonalityValueToPersonalityValueApiDto(PersonalityValue personalityValue);

        MatchApiDto MapMatchToMatchApiDto(Match match);
        MatchDetailApiDto MapMapDetailToMatchDetailApiDto(MatchDetail matchDetail);

        TargetLocationApiDto MapTargetLocationToTargetLocationApiDto(TargetLocation targetLocation);

        BuildingPictureApiDto MapBuildingPictureToBuildingPictureApiDto(BuildingPicture buildingPicture);
        BuildingUserApiDto MapBuildingUserToBuildingUserApiDto(BuildingUser buildingUser);
        BuildingApiDto MapBuildingToBuildingApiDto(Building building);
        BuildingMessageApiDto MapBuildingMessageToBuildingMessageApiDto(BuildingMessage buildingMessage);
    }
}

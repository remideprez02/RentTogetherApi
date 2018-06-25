using System;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Match;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Entities.Dto.Personality.Value;

namespace RentTogether.Interfaces.Helpers
{
    public interface IMapperHelper
    {
        User MapUserRegisterDtoToUser(UserRegisterDto userRegisterDto);
        UserApiDto MapUserToUserApiDto(User user);
        User MapUpdateUserApiDtoToUser(UserApiDto userApiDto, User user);

		ParticipantApiDto MapParticipantToParticipantApiDto(Participant participant);

		MessageApiDto MapMessageToMessageApiDto(Message message);
		FileApiDto MapUserPictureToFileApiDto(UserPicture userPicture);

        DetailPersonalityApiDto MapPersonalityReferencialToDetailPersonalityApiDto(PersonalityReferencial personalityReferencial);
        PersonalityValueApiDto MapPersonalityValueToPersonalityValueApiDto(PersonalityValue personalityValue);

        MatchApiDto MapMatchToMatchApiDto(Match match);
    }
}

using System;
using RentTogether.Entities;
using RentTogether.Entities.Dto;
using RentTogether.Entities.Dto.Media;
using RentTogether.Entities.Dto.Message;
using RentTogether.Entities.Dto.Participant;

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
    }
}

//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Media;

namespace RentTogether.Interfaces.Business
{
    public interface IMediaService
    {
		Task<FileApiDto> PostAsyncUserPicture(FileDto fileDto);
		Task<FileApiDto> GetAsyncUserPictureByUserId(int userId);
    }
}

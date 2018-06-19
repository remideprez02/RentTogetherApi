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

//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Threading.Tasks;
using RentTogether.Entities.Dto.Media;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Dal;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Business.Services
{
    public class MediaService : IMediaService
    {
        private readonly IDal _dal;
        private readonly ICustomEncoder _customEncoder;
        public MediaService(IDal dal, ICustomEncoder customEncoder)
        {
            _dal = dal;
            _customEncoder = customEncoder;
        }

        /// <summary>
        /// Posts the async user picture.
        /// </summary>
        /// <returns>The async user picture.</returns>
        /// <param name="fileDto">File dto.</param>
		public async Task<FileApiDto> PostAsyncUserPicture(FileDto fileDto)
        {
            fileDto.FileToBase64 = _customEncoder.FileToBase64(fileDto.File);
            return await _dal.PostAsyncUserPicture(fileDto);
        }

        /// <summary>
        /// Gets the async user picture by user identifier.
        /// </summary>
        /// <returns>The async user picture by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
		public async Task<FileApiDto> GetAsyncUserPictureByUserId(int userId)
        {
            return await _dal.GetAsyncUserPictureByUserId(userId);
        }
    }
}

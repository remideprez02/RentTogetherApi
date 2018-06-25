using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.Media;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Api.Controllers
{
	public class MediaController : ODataController
    {
		private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
		private readonly IMediaService _mediaService;
		private readonly ICustomEncoder _customEncoder;

		public MediaController(IUserService userService, IAuthenticationService authenticationService,
		                       IMediaService mediaService, ICustomEncoder customEncoder)
        {
            _userService = userService;
            _authenticationService = authenticationService;
			_mediaService = mediaService;
			_customEncoder = customEncoder;
        }
		[Route("api/Media")]
        [HttpPost]
		public async Task<IActionResult> PostUserPicture(IFormFile file, int userId)
		{
			//Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
					if (await _authenticationService.CheckIfTokenIsValidAsync(token, userId))
                    {
						var fileDto = new FileDto()
						{
							File = file,
							UserId = userId
						};

						var fileApiDto = await _mediaService.PostAsyncUserPicture(fileDto);

						if (fileApiDto == null)
                        {
                            return StatusCode(400, "Unable to upload UserPicture.");
                        }

						byte[] fileBytes = Convert.FromBase64String(fileApiDto.FileToBase64);
						var f = File(fileBytes, "image/png");

						return f;
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
		}

		[Route("api/Media/{userId}")]
        [HttpGet]
        [EnableQuery]
		public async Task<IActionResult> GetUserPicture(int userId)
		{
			//Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token))
                    {
                        
						var fileApiDto = await _mediaService.GetAsyncUserPictureByUserId(userId);
						if (fileApiDto == null)
                        {
                            return StatusCode(404, "UserPicture not found.");
                        }
						byte[] fileBytes = Convert.FromBase64String(fileApiDto.FileToBase64);
                        var f = File(fileBytes, "image/png");

                        return f;
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
		}
    }
}

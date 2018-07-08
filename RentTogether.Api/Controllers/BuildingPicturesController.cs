using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.BuildingPicture;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class BuildingPicturesController : Controller
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IUserService _userService;
        private readonly IBuildingService _buildingService;

        public BuildingPicturesController(IBuildingService buildingService, IAuthenticationService authenticationService,
                                       ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
            _buildingService = buildingService;
            _customEncoder = customEncoder;
            _userService = userService;
        }
        [Route("api/Building/{buildingId}/BuildingPictures/{buildingPictureId}")]
        [HttpGet]
        public async Task<IActionResult> Get(int buildingId, int buildingPictureId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && buildingId > -1 && buildingPictureId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    var user = await _userService.GetUserAsyncByToken(token);
                    if (user != null)
                    {
                        //Verify if the token exist and is not expire
                        if ((await _authenticationService.CheckIfTokenIsValidAsync(token) && user.IsAdmin == 1) || await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {

                            var buildingPictureApiDto = await _buildingService.GetBuildingPicturesAsync(buildingId, buildingPictureId);

                            if (buildingPictureApiDto == null)
                            {
                                return StatusCode(400, "Building Pictures(s) not found.");
                            }
                            byte[] fileBytes = Convert.FromBase64String(buildingPictureApiDto.FileToBase64);
                            var f = File(fileBytes, "image/png");

                            return f;
                        }

                        return StatusCode(401, "Invalid token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }
        [Route("api/Building/{buildingId}/BuildingPictures")]
        [HttpGet]
        public async Task<IActionResult> GetBuildingPictures(int buildingId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && buildingId > -1 )
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    var user = await _userService.GetUserAsyncByToken(token);
                    if (user != null)
                    {
                        //Verify if the token exist and is not expire
                        if ((await _authenticationService.CheckIfTokenIsValidAsync(token) && user.IsAdmin == 1) || await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {

                            var buildingPictureApiDtos = await _buildingService.GetBuildingPictureInformationsAsync(buildingId);

                            if (!buildingPictureApiDtos.Any())
                            {
                                return StatusCode(400, "Building Pictures Information(s) not found.");
                            }
                            return Ok(buildingPictureApiDtos);
                        }

                        return StatusCode(401, "Invalid token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }

        [Route("api/BuildingPictures")]
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file, int buildingId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && buildingId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    var user = await _userService.GetUserAsyncByToken(token);
                    if (user != null)
                    {
                        //Verify if the token exist and is not expire
                        if ((await _authenticationService.CheckIfTokenIsValidAsync(token) && user.IsAdmin == 1) || await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {
                            var buildingPictureDto = new BuildingPictureDto
                            {
                                File = file,
                                BuildingId = buildingId
                            };

                            var buildingPictureApiDto = await _buildingService.PostBuildingPictureAsync(buildingPictureDto);

                            if (buildingPictureApiDto == null)
                            {
                                return StatusCode(400, "Unable to create building picture.");
                            }

                            byte[] fileBytes = Convert.FromBase64String(buildingPictureApiDto.FileToBase64);
                            var f = File(fileBytes, "image/png");

                            return f;
                        }
                        return StatusCode(401, "Invalid token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }

        [Route("api/BuildingPictures/{buildingPictureId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int buildingPictureId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && buildingPictureId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    var user = await _userService.GetUserAsyncByToken(token);
                    if (user != null)
                    {
                        //Verify if the token exist and is not expire
                        if ((await _authenticationService.CheckIfTokenIsValidAsync(token) && user.IsAdmin == 1) || await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {

                            var isDeleted = await _buildingService.DeleteBuildingPictureAsync(buildingPictureId);

                            if (isDeleted == false)
                            {
                                return StatusCode(404, "Unable to delete building picture.");
                            }
                            return StatusCode(204, "Building Picture has been deleted.");
                        }
                        return StatusCode(401, "Invalid token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }
    }
}

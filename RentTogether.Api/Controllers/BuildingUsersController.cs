using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.BuildingUser;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class BuildingUsersController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IUserService _userService;
        private readonly IBuildingService _buildingService;

        public BuildingUsersController(IBuildingService buildingService, IAuthenticationService authenticationService,
                                       ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
            _buildingService = buildingService;
            _customEncoder = customEncoder;
            _userService = userService;
        }

        [Route("api/BuildingUsers")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BuildingUserDto buildingUserDto)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
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

                            var buildingApiDto = await _buildingService.PostBuildingUserAsync(buildingUserDto);

                            if (buildingApiDto == null)
                            {
                                return StatusCode(400, "Unable to create building user.");
                            }
                            return Ok(buildingApiDto);
                        }
                        return StatusCode(401, "Invalid token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }

        [Route("api/BuildingUsersController")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]BuildingUserDto buildingUserDto)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
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

                            var isDeleted = await _buildingService.DeleteBuildingUserAsync(buildingUserDto);

                            if (isDeleted == false)
                            {
                                return StatusCode(404, "Unable to delete building user.");
                            }
                            return StatusCode(204, "Building User has been deleted.");
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

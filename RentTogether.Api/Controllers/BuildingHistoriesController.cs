//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.BuildingHistory;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

namespace RentTogether.Api.Controllers
{
    
    public class BuildingHistoriesController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IUserService _userService;
        private readonly IBuildingService _buildingService;

        public BuildingHistoriesController(IBuildingService buildingService, IAuthenticationService authenticationService,
                                       ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
            _buildingService = buildingService;
            _customEncoder = customEncoder;
            _userService = userService;
        }

        //Get List Building History By UserId
        [Route("api/BuildingHistories/{userId}")]
        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
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

                            var buildingHistoriesApiDto = await _buildingService.GetBuildingHistoriesByUserIdAsync(userId);

                            if (buildingHistoriesApiDto == null)
                            {
                                return StatusCode(400, "Building(s) History not found.");
                            }
                            return Ok(buildingHistoriesApiDto);
                        }
                        return StatusCode(401, "Invalid token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }

        /// <summary>
        /// Post the specified buildingHistoryDto.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="buildingHistoryDto">Building history dto.</param>
        [Route("api/BuildingHistories")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BuildingHistoryDto buildingHistoryDto)
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

                            var buildingHistoryApiDto = await _buildingService.PostBuildingHistoryAsync(buildingHistoryDto);

                            if (buildingHistoryApiDto == null)
                            {
                                return StatusCode(400, "Unable to create building history.");
                            }
                            return Ok(buildingHistoryApiDto);
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

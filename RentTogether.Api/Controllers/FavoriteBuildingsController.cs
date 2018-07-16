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
using RentTogether.Entities.Dto.FavoriteBuilding;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class FavoriteBuildingsController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IUserService _userService;
        private readonly IBuildingService _buildingService;

        public FavoriteBuildingsController(IBuildingService buildingService, IAuthenticationService authenticationService,
                                       ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
            _buildingService = buildingService;
            _customEncoder = customEncoder;
            _userService = userService;
        }

        //Get favorite buildings by User Id
        [Route("api/FavoriteBuildings/{userId}")]
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

                            var buildingApiDtos = await _buildingService.GetFavoriteBuildingsByUserIdAsync(userId);

                            if (buildingApiDtos == null)
                            {
                                return StatusCode(404, "Building(s) not found.");
                            }
                            return Ok(buildingApiDtos);
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
        /// Post the specified favoriteBuildingDto.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="favoriteBuildingDto">Favorite building dto.</param>
        [Route("api/FavoriteBuildings")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FavoriteBuildingDto favoriteBuildingDto)
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

                            var favoriteBuildingApiDto = await _buildingService.PostFavoriteBuildingAsync(favoriteBuildingDto);

                            if (favoriteBuildingApiDto == null)
                            {
                                return StatusCode(404, "Unable to create favorite building.");
                            }
                            return Ok(favoriteBuildingApiDto);
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
        /// Delete the specified buildingId and userId.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="buildingId">Building identifier.</param>
        /// <param name="userId">User identifier.</param>
        [Route("api/FavoriteBuildings/{buildingId}/User/{userID}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int buildingId, int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && buildingId > -1 && userId > -1)
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

                            var isDeleted = await _buildingService.DeleteFavoriteBuildingByBuildingIdAsync(buildingId, userId);

                            if (isDeleted == false)
                            {
                                return StatusCode(404, "Unable to delete favorite building.");
                            }
                            return StatusCode(204, "Building has been deleted.");
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

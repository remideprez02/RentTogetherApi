//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.Building;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class BuildingsController : ODataController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IUserService _userService;
        private readonly IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService, IAuthenticationService authenticationService,
                                       ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
            _buildingService = buildingService;
            _customEncoder = customEncoder;
            _userService = userService;
        }

        /// <summary>
        /// Get the specified userId.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="userId">User identifier.</param>
        [Route("api/Building/{userId}")]
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
                            
                            var buildingApiDtos = await _buildingService.GetAsyncBuilding(user.IsOwner, userId);

                            if (buildingApiDtos == null)
                            {
                                return StatusCode(400, "Building(s) not found.");
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
        /// Gets the building of renter.
        /// </summary>
        /// <returns>The building of renter.</returns>
        /// <param name="userId">User identifier.</param>
        [Route("api/Building/BuildingUser/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetBuildingOfRenter(int userId)
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

                            var buildingApiDto = await _buildingService.GetAsyncBuildingOfRenter(userId);

                            if (buildingApiDto == null)
                            {
                                return StatusCode(400, "Building(s) not found.");
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

        /// <summary>
        /// Post the specified buildingDto.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="buildingDto">Building dto.</param>
        [Route("api/Building")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BuildingDto buildingDto)
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

                            var buildingApiDto = await _buildingService.PostAsyncBuilding(buildingDto);

                            if (buildingApiDto == null)
                            {
                                return StatusCode(400, "Unable to create building.");
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

        /// <summary>
        /// Delete the specified buildingId and userId.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="buildingId">Building identifier.</param>
        /// <param name="userId">User identifier.</param>
        [Route("api/Building/{buildingId}/User/{userId}")]
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

                            var isDeleted = await _buildingService.DeleteBuildingAsync(buildingId, userId, user.IsAdmin);

                            if (isDeleted == false)
                            {
                                return StatusCode(404, "Unable to delete building.");
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

        /// <summary>
        /// Put the specified buildingUpdateDto.
        /// </summary>
        /// <returns>The put.</returns>
        /// <param name="buildingUpdateDto">Building update dto.</param>
        [Route("api/Building")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]BuildingUpdateDto buildingUpdateDto)
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

                            var buildingApiDto = await _buildingService.UpdateBuildingAsync(buildingUpdateDto);

                            if (buildingApiDto == null)
                            {
                                return StatusCode(400, "Unable to update building.");
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
    }
}

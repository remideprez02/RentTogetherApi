﻿//
//Author : Déprez Rémi
//Version : 1.0
//

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.BuildingMessage;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{

    public class BuildingMessagesController : ODataController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IUserService _userService;
        private readonly IBuildingService _buildingService;

        public BuildingMessagesController(IBuildingService buildingService, IAuthenticationService authenticationService,
                                       ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
            _buildingService = buildingService;
            _customEncoder = customEncoder;
            _userService = userService;
        }

        //Get Messages from Building By Building Id
        [EnableQuery]
        [Route("api/BuildingMessages/{buildingId}")]
        [HttpGet]
        public async Task<IActionResult> Get(int buildingId)
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

                            var buildingMessageApiDtos = await _buildingService.GetBuildingMessagesAsync(buildingId);

                            if (!buildingMessageApiDtos.Any())
                            {
                                return StatusCode(400, "Building Message(s) not found.");
                            }
                            return Ok(buildingMessageApiDtos);
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
        /// Post the specified buildingMessageDto.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="buildingMessageDto">Building message dto.</param>
        [EnableQuery]
        [Route("api/BuildingMessages")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BuildingMessageDto buildingMessageDto)
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

                            var buildingMessageApiDto = await _buildingService.PostAsyncBuildingMessage(buildingMessageDto);

                            if (buildingMessageApiDto == null)
                            {
                                return StatusCode(400, "Unable to create building message.");
                            }
                            return Ok(buildingMessageApiDto);
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
        /// Delete the specified buildingMessageId.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="buildingMessageId">Building message identifier.</param>
        [EnableQuery]
        [Route("api/BuildingMessages/{buildingMessageId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int buildingMessageId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && buildingMessageId > -1)
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

                            var isDeleted = await _buildingService.DeleteBuildingMessageAsync(buildingMessageId);

                            if (isDeleted == false)
                            {
                                return StatusCode(404, "Unable to delete building message.");
                            }
                            return StatusCode(204, "Building message has been deleted.");
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

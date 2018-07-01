﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.TargetLocation;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class TargetLocationsController : ODataController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IMapperHelper _mapperHelper;
        private readonly ITargetLocationService _targetLocationService;

        public TargetLocationsController(IUserService userService,
                                  IAuthenticationService authenticationService,
                                         ICustomEncoder customEncoder, IMapperHelper mapperHelper, ITargetLocationService targetLocationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
            _targetLocationService = targetLocationService;
        }

        [Route("api/TargetLocations/{userId}")]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri]int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    var user = await _userService.GetUserApiDtoAsyncById(userId);
                    if (user != null)
                    {
                        //Verify if the token exist and is not expire
                        if (await _authenticationService.CheckIfTokenIsValidAsync(token, userId))
                        {
                            //Verify if messages for this userId exist
                            var targetLocationApiDto = await _targetLocationService.GetAsyncTargetLocationsByUserId(userId);
                            if (targetLocationApiDto == null)
                            {
                                return StatusCode(404, "Target Location(s) Not Found.");
                            }
                            return Ok(targetLocationApiDto);
                        }
                        return StatusCode(401, "Invalid Token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        [Route("api/TargetLocations/{userId}")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<TargetLocationDto> targetLocationDto, int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());

                if (token != null)
                {
                    if (targetLocationDto != null || targetLocationDto.Count > 0)
                    {
                        var user = await _userService.GetUserApiDtoAsyncById(userId);

                        if (user != null)
                        {
                            //Verify if the token exist and is not expire
                            if (await _authenticationService.CheckIfTokenIsValidAsync(token, userId))
                            {
                                //Verify if messages for this userId exist
                                var targetLocationApiDto = await _targetLocationService.PostAsyncTargetLocation(targetLocationDto, userId);
                                if (targetLocationApiDto == null)
                                {
                                    return StatusCode(404, "Unable to post target location(s).");
                                }
                                return Ok(targetLocationApiDto);
                            }
                            return StatusCode(401, "Invalid Token.");
                        }
                        return StatusCode(403, "Invalid data model.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        [Route("api/TargetLocations/{userId}")]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody]List<TargetLocationPatchDto> targetLocationPatchDtos, int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());

                if (token != null)
                {
                    if (targetLocationPatchDtos != null || targetLocationPatchDtos.Count > 0)
                    {
                        var user = await _userService.GetUserApiDtoAsyncById(userId);

                        if (user != null)
                        {
                            //Verify if the token exist and is not expire
                            if (await _authenticationService.CheckIfTokenIsValidAsync(token, userId))
                            {
                                //Verify if messages for this userId exist
                                var targetLocationApiDto = await _targetLocationService.PatchAsyncTargetLocation(targetLocationPatchDtos, userId);
                                if (targetLocationApiDto == null)
                                {
                                    return StatusCode(404, "Unable to patch target location.");
                                }
                                return Ok(targetLocationApiDto);
                            }
                            return StatusCode(401, "Invalid Token.");
                        }
                        return StatusCode(403, "Invalid data model.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        [Route("api/TargetLocations/{targetLocationId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int targetLocationId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && targetLocationId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    var user = await _userService.GetUserAsyncByToken(token);
                    if (user != null)
                    {
                        //Verify if the token exist and is not expire
                        if (await _authenticationService.CheckIfTokenIsValidAsync(token))
                        {
                            //Verify if messages for this userId exist
                            var isDeleted = await _targetLocationService.DeleteAsyncTargetLocation(targetLocationId);
                            if (isDeleted == false)
                            {
                                return StatusCode(404, "Unable to delete target location.");
                            }
                            return StatusCode(204, "The target location has been deleted.");
                        }
                        return StatusCode(401, "Invalid Token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using RentTogetherApi.Api.Models;
using RentTogetherApi.Common.Helpers;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;
using RentTogetherApi.Interfaces.Business;
using RentTogetherApi.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace RentTogetherApi.Api.Controllers
{
    //[RequireHttps]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IMapperHelper _mapperHelper;
        private readonly ILogger _logger;

        public UsersController(IUserService userService, IAuthenticationService authenticationService,
                               ICustomEncoder customEncoder, IMapperHelper mapperHelper,
                               ILogger<UsersController> logger)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
            _logger = logger;
        }

        // GET User
        [Route("api/Users/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && id > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                _logger.LogInformation(LoggingEvents.BearerAuthInProgress, "BearerAuthInProgress({token}) VERIFY TOKEN", token);
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token, id))
                    {
                        _logger.LogInformation(LoggingEvents.GetItem, "Getting item {ID}", id);

                        //Verify if user exist
                        var userApiDto = await _userService.GetUserApiDtoAsyncById(id);
                        if (userApiDto == null)
                        {
                            _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", id);
                            return StatusCode(404);
                        }
                        return Json(userApiDto);
                    }
                }
                _logger.LogWarning(LoggingEvents.BearerAuthFailed, "BearerAuthFailed({token}) BAD TOKEN", token);
            }
            return StatusCode(401);
        }

        // Get Authentication (Basic Auth)
        [Route("api/Users")]
        [HttpGet]
        //[RequireHttps]
        public async Task<IActionResult> Get()
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                _logger.LogInformation(LoggingEvents.BearerAuthInProgress, "BearerAuthInProgress({token}) VERIFY TOKEN", token);
                if (token != null)
                {
                    var user = _userService.GetUserAsyncByToken(token);
                    if (user != null)
                    {
                        //Verify if the token exist and is not expire
                        if (await _authenticationService.CheckIfTokenIsValidAsync(token, user.Id))
                        {
                            _logger.LogInformation(LoggingEvents.GetItem, "Getting item {ID}", user.Id);

                            var usersApiDto = _userService.GetAllUsersAsync();
                            return Json(usersApiDto);
                        }
                    }
                }
                _logger.LogWarning(LoggingEvents.BearerAuthFailed, "BearerAuthFailed({token}) BAD TOKEN", token);
            }
            return StatusCode(401);
        }

        //POST User
        [Route("api/Users")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]UserRegisterDto userRegisterDto)
        {
            _logger.LogInformation(LoggingEvents.InsertItem, "Insert New User({Email})", userRegisterDto.Email);
            var user = await _userService.CreateUserAsync(userRegisterDto);
            if (user != null)
            {
                var userApiDto = _mapperHelper.MapUserToUserApiDto(user);
                return Json(userApiDto);
            }
            _logger.LogWarning(LoggingEvents.InsertItem, "Insert New User({Email}) FAILED, USER ALREADY EXIST", userRegisterDto.Email);
            return StatusCode(500);
        }

        //PUT User
        [Route("api/Users/{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UserApiDto userApiDto)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userApiDto != null)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());

                if (token != null)
                {
                    userApiDto.Token = token;
                    var userApiDtoUpdated = await _userService.UpdateUserAsync(userApiDto);
                    if (userApiDtoUpdated != null)
                    {
                        return Json(userApiDtoUpdated);
                    }
                    return StatusCode(404);
                }
                return StatusCode(403);
            }
            return StatusCode(401);
        }

        //DELETE User
        [Route("api/Users/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && id > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());

                if (token != null)
                {
                    var userDeleted = await _userService.DeleteUserByIdAsync(id, token);
                    if (userDeleted)
                    {
                        return StatusCode(204, Json("User supprimé ! "));
                    }
                    return StatusCode(403);
                }
                return StatusCode(403);
            }
            return StatusCode(401);
        }
    }
}

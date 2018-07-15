﻿//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;
using RentTogether.Common.Helpers;
using RentTogether.Entities.Dto;
using Microsoft.AspNet.OData;
using RentTogether.Entities;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace RentTogether.Api.Controllers
{
    //[RequireHttps]
    public class UsersController : ODataController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IMapperHelper _mapperHelper;
        private readonly ILogger _logger;
        private readonly RentTogetherDbContext _rentTogetherDbContext;

        public UsersController(IUserService userService, IAuthenticationService authenticationService,
                               ICustomEncoder customEncoder, IMapperHelper mapperHelper,
                               ILogger<UsersController> logger, RentTogetherDbContext rentTogetherDbContext)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
            _logger = logger;
            _rentTogetherDbContext = rentTogetherDbContext;
        }

        // GET User
        [Route("api/Users/{id}")]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri]int id)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && id > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {

                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token, id))
                    {
                        //Verify if user exist
                        var user = await _userService.GetUserApiDtoAsyncById(id);
                        if (user == null)
                        {
                            return StatusCode(404, "User Not Found.");
                        }
                        return Ok(user);
                    }
                    return StatusCode(401, "Invalid Token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>The get.</returns>
        [Route("api/Users")]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    var user = await _userService.GetUserAsyncByToken(token);
                    if (user != null && user.IsAdmin == 1)
                    {
                        //Verify if the token exist and is not expire
                        if (await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {
                            var users = await _userService.GetAllUsersAsync();
                            if (users == null)
                            {
                                return StatusCode(404, "Users not found.");
                            }
                            return Ok(users);
                        }
                        return StatusCode(401, "Invalid Token.");
                    }
                    return StatusCode(403, "Invalid User.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        /// <summary>
        /// Post the specified userRegisterDto.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="userRegisterDto">User register dto.</param>
        [Route("api/Users")]
        [HttpPost]
        [EnableQuery]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]UserRegisterDto userRegisterDto)
        {
            var isValid = _userService.CheckIfUserModelIsValid(userRegisterDto);
            if (isValid.Item1 == true)
            {
                var user = await _userService.CreateUserAsync(userRegisterDto);
                if (user != null)
                {
                    var userApiDto = _mapperHelper.MapUserToUserApiDto(user);
                    return Ok(userApiDto);
                }
                return StatusCode(400, "Unable to create user.");
            }
            return StatusCode(400, isValid.Item2);
        }

        /// <summary>
        /// Put the specified userApiDto.
        /// </summary>
        /// <returns>The put.</returns>
        /// <param name="userApiDto">User API dto.</param>
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

                    var userApiDtoUpdated = await _userService.UpdateUserAsync(userApiDto, token);

                    if (userApiDtoUpdated != null)
                    {
                        return Ok(userApiDtoUpdated);
                    }
                    return StatusCode(404);
                }
                return StatusCode(403);

            }

            return StatusCode(401);
        }

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
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
                        return StatusCode(204, "The user has been deleted.");
                    }
                    return StatusCode(403);
                }
                return StatusCode(403);
            }
            return StatusCode(401);
        }

        /// <summary>
        /// Patch the specified userPatchApiDto.
        /// </summary>
        /// <returns>The patch.</returns>
        /// <param name="userPatchApiDto">User patch API dto.</param>
        [HttpPatch]
        [Route("api/Users")]
        public async Task<IActionResult> Patch([FromBody]UserPatchApiDto userPatchApiDto)
        {

            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
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
                            var userApiDto = await _userService.PatchUser(userPatchApiDto);
                            if (userApiDto == null)
                            {
                                return StatusCode(404, "User not found.");
                            }
                            return Ok(userApiDto);
                        }
                        return StatusCode(401, "Invalid Token.");
                    }
                    return StatusCode(403, "Invalid User.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }
    }
}

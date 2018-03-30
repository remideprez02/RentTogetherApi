using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogetherApi.Api.Models;
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

        public UsersController(IUserService userService, IAuthenticationService authenticationService, ICustomEncoder customEncoder, IMapperHelper mapperHelper)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
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

                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token, id))
                    {
                        //Verify if user exist
                        var userApiDto = await _userService.GetUserApiDtoAsyncById(id);
                        if (userApiDto == null)
                            return StatusCode(401);

                        return Json(userApiDto);
                    }
                }

            }
            return StatusCode(401);
        }

        // Get Authentication
        [Route("api/Users")]
        [HttpGet]
        //[RequireHttps]
        public async Task<IActionResult> Get()
        {
            var header = Request.Headers;
            //Get header basic
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
            {
                var decodedBasicAuth = _customEncoder.DecodeBasicAuth(headerValues.ToString());

                //If not null
                if (decodedBasicAuth != null && decodedBasicAuth.Item1 != "" && decodedBasicAuth.Item2 != "")
                {
                    var userLoginDto = new UserLoginDto()
                    {
                        Email = decodedBasicAuth.Item1,
                        Password = decodedBasicAuth.Item2
                    };

                    var userApiDto = await _userService.GetUserByBasicAuthenticationAsync(userLoginDto);
                    if (userApiDto == null)
                    {
                        return StatusCode(401);
                    }
                    return Json(userApiDto);
                }
                return StatusCode(401);
            }
            return StatusCode(401);
        }

        //POST User
        [Route("api/Users")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserRegisterDto userRegisterDto)
        {
            var user = await _userService.CreateUserAsync(userRegisterDto);
            if (user != null)
            {
                var userApiDto = _mapperHelper.MapUserToUserApiDto(user);
                return Json(userApiDto);
            }
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

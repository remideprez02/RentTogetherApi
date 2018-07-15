//
//Author : Déprez Rémi
//Version : 1.0
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentTogether.Entities.Dto;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    //[Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IMapperHelper _mapperHelper;
        private readonly ILogger _logger;

        public LoginController(IUserService userService, IAuthenticationService authenticationService,
                               ICustomEncoder customEncoder, IMapperHelper mapperHelper,
                               ILogger<UsersController> logger)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
            _logger = logger;
        }

        // Get Authentication (Basic Auth)
        [Route("api/Login")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //Get header basic
            if (Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues headerValues))
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
                        return StatusCode(404, "LogIn Not Found.");
                    }
                    return Json(userApiDto);
                }
                return StatusCode(401, "Invalid authorization.");
            }
			return StatusCode(401, "Invalid authorization.");
        }
    }
}

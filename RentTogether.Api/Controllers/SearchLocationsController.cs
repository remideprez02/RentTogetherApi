using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.SearchLocation;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    
    public class SearchLocationsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly ISearchLocationService _searchLocationService;
        private readonly IMapperHelper _mapperHelper;

        public SearchLocationsController(IUserService userService,IAuthenticationService authenticationService,
                                         ICustomEncoder customEncoder, IMapperHelper mapperHelper, ISearchLocationService searchLocationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
            _searchLocationService = searchLocationService;
        }


        [Route("api/SearchLocations")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SearchLocationDto searchLocationDto)
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

                            var searchLocationApiDto = await _searchLocationService.GetSearchLocationsAsync(searchLocationDto);

                            if (searchLocationApiDto.Count == 0)
                            {
                                return StatusCode(400, "Location(s) not found.");
                            }
                            return Ok(searchLocationApiDto);
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

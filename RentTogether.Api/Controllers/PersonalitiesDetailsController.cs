using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.Personality.Detail;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class PersonalitiesDetailsController : ODataController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IMapperHelper _mapperHelper;
        private readonly IPersonalityService _personalityService;

        public PersonalitiesDetailsController(IUserService userService,
                                  IAuthenticationService authenticationService,
                                              ICustomEncoder customEncoder, IMapperHelper mapperHelper, IPersonalityService personalityService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
            _personalityService = personalityService;
        }

        [Route("api/PersonalitiesDetails")]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllPersonalityReferencials()
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token))
                    {
                        var user = await _userService.GetUserAsyncByToken(token);
                        if (user!= null)
                        {
                            //Verify if personalities exist
                            var personalitiesApiDtos = await _personalityService.GetAsyncAllPersonalityReferencials();
                            if (personalitiesApiDtos == null)
                            {
                                return StatusCode(404, "Personality Referencials not found.");
                            }
                            return Ok(personalitiesApiDtos);
                        }
                        return StatusCode(403, "Invalid user.");
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }

        //POST User
        [Route("api/PersonalitiesDetails")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DetailPersonalityDto detailPersonalityDto)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token))
                    {
                        var user = await _userService.GetUserAsyncByToken(token);
                        if (user.IsAdmin == 1)
                        {
                            var detailPersonalityApiDto = await _personalityService.PostAsyncDetailPersonality(detailPersonalityDto);

                            if (detailPersonalityApiDto == null)
                            {
                                return StatusCode(400, "Unable to create referencial personality.");
                            }
                            return Ok(detailPersonalityApiDto);
                        }
                        return StatusCode(403, "Invalid user.");
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }

    }
}

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
using RentTogether.Entities.Dto.Personality.Value;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class PersonalitiesController : ODataController
    {

        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IMapperHelper _mapperHelper;
        private readonly IPersonalityService _personalityService;

        public PersonalitiesController(IUserService userService,
                                  IAuthenticationService authenticationService,
                                              ICustomEncoder customEncoder, IMapperHelper mapperHelper, IPersonalityService personalityService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
            _personalityService = personalityService;
        }

        /// <summary>
        /// Gets the personality by user identifier.
        /// </summary>
        /// <returns>The personality by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        [Route("api/Personalities/{userId}")]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetPersonalityByUserId(int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token))
                    {
                            //Verify if personalities exist
                        var personalityApiDto = await _personalityService.GetPersonalityAsyncByUserId(userId);
                        if (personalityApiDto == null)
                            {
                                return StatusCode(404, "Personality not found.");
                            }
                        return Ok(personalityApiDto);
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }

        /// <summary>
        /// Posts the personality values.
        /// </summary>
        /// <returns>The personality values.</returns>
        /// <param name="personalityValueDtos">Personality value dtos.</param>
        /// <param name="userId">User identifier.</param>
        [Route("api/Personalities/{userId}")]
        [HttpPost]
        public async Task<IActionResult> PostPersonalityValues([FromBody]List<PersonalityValueDto> personalityValueDtos, int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token))
                    {
                        var personalityApiValueDtos = await _personalityService.PostAsyncPersonalityValues(personalityValueDtos, userId);

                        if (personalityApiValueDtos == null || personalityApiValueDtos.Count <= 0)
                        {
                            return StatusCode(404, "Unable to create personality values.");
                        }
                        return Ok(personalityApiValueDtos);
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid authorization.");
            }
            return StatusCode(401, "Invalid authorization.");
        }

        /// <summary>
        /// Patch the specified personalityValuePatchDtos and userId.
        /// </summary>
        /// <returns>The patch.</returns>
        /// <param name="personalityValuePatchDtos">Personality value patch dtos.</param>
        /// <param name="userId">User identifier.</param>
        [Route("api/Personalities/{userId}")]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody]List<PersonalityValuePatchDto> personalityValuePatchDtos, int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());

                if (token != null)
                {
                    if (personalityValuePatchDtos != null)
                    {
                        var user = await _userService.GetUserApiDtoAsyncById(userId);

                        if (user != null)
                        {
                            //Verify if the token exist and is not expire
                            if (await _authenticationService.CheckIfTokenIsValidAsync(token, userId))
                            {
                                
                                var personalityValuesApiDtos = await _personalityService.PatchAsyncPersonalityValuesByUserId(userId, personalityValuePatchDtos);
                                if (personalityValuesApiDtos == null)
                                {
                                    return StatusCode(404, "Unable to patch personality values.");
                                }
                                return Ok(personalityValuesApiDtos);
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
    }
}

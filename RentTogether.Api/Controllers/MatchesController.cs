using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.Match;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class MatchesController : ODataController
    {

        private readonly IMatchService _matchService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IUserService _userService;

        public MatchesController(IMatchService matchService, IAuthenticationService authenticationService,
                                       ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
            _matchService = matchService;
            _customEncoder = customEncoder;
            _userService = userService;
        }

        // GET api/values/5
        [EnableQuery]
        [HttpGet]
        [Route("api/Matches/{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token, userId))
                    {
                        //Verify if messages for this userId exist
                        var matchApiDtos = await _matchService.GetAsyncListMatches(userId);
                        if (matchApiDtos == null || matchApiDtos.Count <= 0)
                        {
                            return StatusCode(404, "Match(es) not found.");
                        }
                        return Ok(matchApiDtos);
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        [EnableQuery]
        [HttpGet]
        [Route("api/Matches/{userId}/GetValidateMatches")]
        public async Task<IActionResult> GetValidateMatches(int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token, userId))
                    {
                        //Verify if messages for this userId exist
                        var matchApiDtos = await _matchService.GetAsyncValidateMatches(userId);
                        if (matchApiDtos == null || matchApiDtos.Count <= 0)
                        {
                            return StatusCode(404, "Validate Match(es) not found.");
                        }
                        return Ok(matchApiDtos);
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        [EnableQuery]
        [HttpGet]
        [Route("api/Matches/{userId}/GetAllMatches")]
        public async Task<IActionResult> GetAllMatches(int userId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && userId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token, userId))
                    {
                        //Verify if messages for this userId exist
                        var matchApiDtos = await _matchService.GetAsyncAllMatches(userId);
                        if (matchApiDtos == null || matchApiDtos.Count <= 0)
                        {
                            return StatusCode(404, "Match(es) not found.");
                        }
                        return Ok(matchApiDtos);
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        [EnableQuery]
        [HttpPost]
        [Route("api/Matches")]
        public async Task<IActionResult> Post([FromBody]MatchDto matchDto)
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

                        var matchApiDto = await _matchService.PostAsyncMatch(matchDto);
                        if (matchApiDto == null)
                        {
                            return StatusCode(404, "Unable to post match.");
                        }
                        return Ok(matchApiDto);
                    }
                    return StatusCode(401, "Invalid Token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        [EnableQuery]
        [Route("api/Matches/{userId}")]
        [HttpPatch]
        public async Task<IActionResult> Patch(int userId)
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

                            var matchApiDtos = await _matchService.PatchAsyncMatches(userId);
                            if (matchApiDtos.Count == 0)
                            {
                                return StatusCode(404, "No matche(s) to patch.");
                            }
                            return Ok(matchApiDtos);

                        }
                        return StatusCode(401, "Invalid Token.");
                    }
                    return StatusCode(403, "Invalid User.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        [EnableQuery]
        [Route("api/Matches/{matchId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int matchId)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && matchId > -1)
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
                            var isDeleted = await _matchService.DeleteAsyncMatch(matchId);
                            if (isDeleted == false)
                            {
                                return StatusCode(404, "Unable to delete match.");
                            }
                            return StatusCode(204, "The match has been deleted.");
                        }
                        return StatusCode(401, "Invalid Token.");
                    }
                    return StatusCode(401, "Invalid Token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace RentTogether.Api.Controllers
{
	public class ParticipantsController : ODataController 
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IUserService _userService;
		private readonly IParticipantService _participantService;

		public ParticipantsController(IParticipantService participantService, IAuthenticationService authenticationService,
                                       ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
			_participantService = participantService;
            _customEncoder = customEncoder;
            _userService = userService;
        }

        //GET Conversation
        [Route("api/Participants/{userId}")]
        [HttpGet]
        [EnableQuery]
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
						var participant = await _participantService.GetParticipantAsyncByUserId(userId);
						if (participant == null)
                        {
                            return StatusCode(404, "Participant not found.");
                        }
						return Ok(participant);
                    }
                    return StatusCode(401, "Invalid token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        //GET Conversation
        [Route("api/Participants")]
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
                    if (user.IsAdmin == 1 && user != null)
                    {
                        //Verify if the token exist and is not expire
                        if (await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {
                            //Verify if messages for this userId exist
                            var conversations = await _conversationService.GetAllConversationsAsync();
                            if (conversations == null)
                            {
                                return StatusCode(404);
                            }
                            return Ok(conversations);
                        }
                        return StatusCode(401, "Invalid Token.");
                    }
                    return StatusCode(403, "Invalid user.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }

        //POST Conversation
        [Route("api/Conversations")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ConversationDto conversationDto)
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
                        //Verify if messages for this userId exist
                        var conversation = await _conversationService.AddConversationAsync(conversationDto);
                        if (conversation == null)
                        {
                            return StatusCode(404, "Unable to create conversation.");
                        }
                        return Ok(conversation);
                    }
                    return StatusCode(401, "Invalid Token.");
                }
                return StatusCode(401, "Invalid Authorization.");
            }
            return StatusCode(401, "Invalid Authorization.");
        }
    }
}

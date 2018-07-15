

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.Conversation;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class ConversationsController : ODataController
    {
		private readonly IConversationService _conversationService;
		private readonly IAuthenticationService _authenticationService;
		private readonly ICustomEncoder _customEncoder;
		private readonly IUserService _userService;

		public ConversationsController(IConversationService conversationService, IAuthenticationService authenticationService,
		                               ICustomEncoder customEncoder, IUserService userService)
        {
            _authenticationService = authenticationService;
			_conversationService = conversationService;
			_customEncoder = customEncoder;
			_userService = userService;
        }

		//GET Conversation
		[Route("api/Conversations/{userId}")]
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
                            
							var conversations = await _conversationService.GetConversationsAsyncByUserId(userId);
							if(conversations == null){
                            return StatusCode(404, "Convertation(s) not found.");
							}
							return Ok(conversations);
                        }
						return StatusCode(401, "Invalid token.");
                }
				return StatusCode(401, "Invalid Authorization.");
            }
			return StatusCode(401, "Invalid Authorization.");
        }
        
		//GET Conversation
        [Route("api/Conversations")]
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
					if (user == null)
						return StatusCode(401, "Invalid token.");
                    if (user.IsAdmin == 1)
                    {
                        //Verify if the token exist and is not expire
						if (await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {
                            
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


		/// <summary>
        /// Post the specified conversationDto.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="conversationDto">Conversation dto.</param>
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
                            
                            var conversation = await _conversationService.AddConversationAsync(conversationDto);
							if(conversation == null){
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

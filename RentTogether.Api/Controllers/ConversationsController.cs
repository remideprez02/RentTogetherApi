using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RentTogether.Entities.Dto.Conversation;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogether.Api.Controllers
{
    public class ConversationsController : Controller
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
		[Route("api/Conversations/{conversationId}")]
		[HttpGet]
		public async Task<IActionResult> Get(int conversationId)
        {
			//Get header token
			if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && conversationId > -1)
            {
				var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    var user = await _userService.GetUserAsyncByToken(token);
                    if (user.IsAdmin == 1)
                    {
                        //Verify if the token exist and is not expire
                        if (await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {
                            //Verify if messages for this userId exist
							var conversation = await _conversationService.GetConversationAsyncById(conversationId);
							if(conversation == null){
								return StatusCode(404);
							}
							return Json(conversation);
                        }
                    }
                    return StatusCode(401);
                }
            }
            return StatusCode(401);
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
                    var user = await _userService.GetUserAsyncByToken(token);
                    if (user.IsAdmin == 1)
                    {
                        //Verify if the token exist and is not expire
                        if (await _authenticationService.CheckIfTokenIsValidAsync(token, user.UserId))
                        {
                            //Verify if messages for this userId exist
                            var conversation = await _conversationService.AddConversationAsync(conversationDto);
							if(conversation == null){
								return StatusCode(404);
							}
							return Json(conversation);
                        }
                    }
                    return StatusCode(401);
                }
            }
            return StatusCode(401);
        }
    }
}

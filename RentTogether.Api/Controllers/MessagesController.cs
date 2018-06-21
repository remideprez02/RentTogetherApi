using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Linq;
using RentTogether.Interfaces.Business;
using RentTogether.Interfaces.Helpers;
using RentTogether.Entities.Dto.Message;
using Microsoft.AspNet.OData;

namespace RentTogether.Api.Controllers
{
    public class MessagesController : ODataController
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomEncoder _customEncoder;
        private readonly IMapperHelper _mapperHelper;

        public MessagesController(IMessageService messageService, IUserService userService,
                                  IAuthenticationService authenticationService, 
                                  ICustomEncoder customEncoder, IMapperHelper mapperHelper)
        {
            _messageService = messageService;
            _userService = userService;
            _authenticationService = authenticationService;
            _customEncoder = customEncoder;
            _mapperHelper = mapperHelper;
        }

        [Route("api/Messages/{userId}")]
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
                        //Verify if user exist
                        var messages = await _messageService.GetAllMessagesAsyncByUserId(userId);
                        if (messages == null)
                        {
                            return StatusCode(404, "Messages not found.");
                        }
						return Ok(messages);
                    }
					return StatusCode(401, "Invalid token.");
                }
				return StatusCode(401, "Invalid authorization.");
            }
			return StatusCode(401, "Invalid authorization.");
        }

		[Route("api/Messages/{conversationId}")]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetMessagesByConversationId(int conversationId)
        {
            //Get header token
			if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues) && conversationId > -1)
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token))
                    {
                        //Verify if user exist
						var messages = await _messageService.GetAllMessagesAsyncFromConversationByConversationId(conversationId);
                        if (messages == null)
                        {
                            return StatusCode(404, "Messages not found.");
                        }
                        return Ok(messages);
                    }
					return StatusCode(401, "Invalid token.");
                }
				return StatusCode(401, "Invalid authorization.");
            }
			return StatusCode(401, "Invalid authorization.");
        }

        //POST User
        [Route("api/Messages")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageDto messageDto)
        {
            //Get header token
            if (Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
            {
                var token = _customEncoder.DecodeBearerAuth(headerValues.First());
                if (token != null)
                {
                    //Verify if the token exist and is not expire
                    if (await _authenticationService.CheckIfTokenIsValidAsync(token, messageDto.UserId))
                    {
                        //Verify if messages for this userId exist
						var messageApiDto = await _messageService.AddMessageAsync(messageDto);

						if(messageApiDto == null){
							return StatusCode(400, "Unable to create message.");
						}
						return Ok(messageApiDto);
                    }
					return StatusCode(401, "Invalid token.");
                }
				return StatusCode(401, "Invalid authorization.");
            }
			return StatusCode(401, "Invalid authorization.");
        }
    }
}

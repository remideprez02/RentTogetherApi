using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentTogetherApi.Common.Helpers;
using RentTogetherApi.Interfaces.Business;
using Microsoft.Extensions.Primitives;
using System.Linq;
using RentTogetherApi.Interfaces.Helpers;
using RentTogetherApi.Entities.Dto.Message;

namespace RentTogetherApi.Api.Controllers
{
    public class MessagesController : Controller
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
                            return StatusCode(404);
                        }
                        return Json(messages);
                    }
                }
            }
            return StatusCode(401);
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
                        await _messageService.CreateMessageAsync(messageDto);
                        return StatusCode(201);
                    }
                }
            }
            return StatusCode(401);
        }
    }
}

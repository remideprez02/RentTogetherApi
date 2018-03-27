using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentTogetherApi.Api.Models;
using RentTogetherApi.Entities;
using RentTogetherApi.Entities.Dto;
using RentTogetherApi.Interfaces.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentTogetherApi.Api.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService){
            _userService = userService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [Route("api/Users/Register")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserRegisterDto userRegisterDto)
        {
            var user = await _userService.CreateUserAsync(userRegisterDto);
            if(user != null){
                return Ok("User Created");
            }
            return StatusCode(500);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

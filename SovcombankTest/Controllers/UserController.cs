using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SovcombankTest.Services.DTO;
using SovcombankTest.Services.Interfaces;

namespace SovcombankTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDto user)
        {
            if (string.IsNullOrEmpty(user.PhoneNumber))
                return BadRequest("Phone number was not entered");

            if (new Regex(@"^7[0-9]{10}").IsMatch(user.PhoneNumber))
            {
                try
                {
                    _userService.ApproveUser(user.PhoneNumber);
                }
                catch(Exception ex)
                {
                    return StatusCode(500, "INTERNAL " + ex.Source + ": " + ex.Message);
                }
            }

            return Ok();
        }
    }
}
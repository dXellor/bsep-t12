using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bsep_api.Extensions.Auth;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Users;
using bsep_dll.Helpers.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryParameters userQueryParameters)
        {
            var users = await _userService.GetAllAsync(userQueryParameters);
            var metadata = users.GetMetadata();
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata)); 
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
            return Ok(users);
        }

        [Authorize]
        [HttpGet("activate/{email}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ActivateUser(string email)
        {
            var success = await _userService.ActivateUser(email);
            return Ok(success);
        }

        [Authorize]
        [HttpGet("block/{email}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> BlockUser(string email)
        {
            var success = await _userService.BlockUser(email);
            return Ok(success);
        }

        [HttpGet("generateOtp")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateOtp(string email)
        {
            var success = await _userService.GenerateOtp(email);
            return Ok(success);
        }
    }
}

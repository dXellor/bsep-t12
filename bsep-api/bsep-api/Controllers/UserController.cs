using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bsep_api.Extensions.Auth;
using bsep_api.Middleware;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Users;
using bsep_dll.Helpers.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Google.Cloud.RecaptchaEnterprise.V1.AccountVerificationInfo.Types;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SignalRHub signalRHub;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            signalRHub = new SignalRHub();
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
        [HttpPut]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser)
        {
            try
            {
                var updatedUserDto = await _userService.UpdateAsync(updatedUser);
        
                if (updatedUserDto == null)
                {
                    return NotFound($"User with email {updatedUser.Email} not found.");
                }

                return Ok(updatedUserDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating the user.");
            }
        }
        
        [Authorize(Roles = "Administrator")]
        [HttpPut("changerole")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeUserRole([FromBody] RoleChangeDto request)
        {
            try
            {
                var updatedUserDto = await _userService.ChangeRoleAsync(request);

                if (updatedUserDto == null)
                {
                    return NotFound($"User with email {request.Email} not found.");
                }

                return Ok(updatedUserDto);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while updating the user role.");
            }
        }
        
        [Authorize]
        [HttpDelete("deleteUserByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserByEmail([FromQuery] string email)
        {
            var result = await _userService.DeleteUserByEmailAsync(email);

            if (result == 0)
                return NotFound("User not found");

            return Ok("User successfully deleted");
        }

        [Authorize]
        [HttpPut("block")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BlockUser([FromQuery] string email)
        {
            var result = await _userService.BlockUser(email);
            if (!result)
                return NotFound("User with the email " + email + " not found");
            return Ok("User blocked");
        }
    }
}

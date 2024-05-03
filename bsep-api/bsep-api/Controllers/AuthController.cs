using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bsep_api.Helpers.Validation.User;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authServiceService;

        public AuthController(IAuthService authService)
        {
            _authServiceService = authService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto registrationDto)
        {
            var validator = new UserRegistrationDtoValidator();
            var validationResult = await validator.ValidateAsync(registrationDto);
            
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors.First());
            
            var result = await _authServiceService.RegisterUser(registrationDto);
            if (result != null)
            {
                return Created("Success", result);
            }
            return BadRequest("Unable to register user");
        }
    }
}

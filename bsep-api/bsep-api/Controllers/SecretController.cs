using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        private static HttpClient _vpnSecretClient = new()
        {
            BaseAddress = new Uri("http://10.13.13.1:3000"),
        };
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVpnSecret()
        {
            try
            {
                var response = await _vpnSecretClient.GetAsync("");
                var secret = await response.Content.ReadAsStringAsync();
                return Ok(Encoding.UTF8.GetString(Convert.FromBase64String(secret)));
            }
            catch (Exception e)
            {
                return NotFound("Server not found, check VPN connection");
            }
        }
    }
}

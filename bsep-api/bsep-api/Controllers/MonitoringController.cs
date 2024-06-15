using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bsep_api.Extensions.Auth;
using bsep_bll.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringService _monitoringService;
        private readonly ILogger<MonitoringController> _logger;

        public MonitoringController(IMonitoringService monitoringService, ILogger<MonitoringController> logger)
        {
            _monitoringService = monitoringService;
            _logger = logger;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("logs")]
        [ProducesResponseType(typeof(MemoryStream), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetLatestLogs()
        {
            var email = User.GetClaim("email");
            _logger.LogInformation("{@RequestName} for {@User} from {@IpAddress}", "Requested latest log file", email, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            var result = await _monitoringService.GetLatestLogs();
            if (result == null)
                return NotFound();
            
            return File(result.MemoryStream, result.ContentType, result.FilePath);
        }
    }
}

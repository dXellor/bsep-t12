using Microsoft.AspNetCore.Mvc;
using bsep_bll.Contracts;
using bsep_bll.Dtos.Advertisements;
using bsep_dll.Helpers.QueryParameters;
using Newtonsoft.Json;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _adService;
        private readonly ILogger<AdvertisementController> _logger;

        public AdvertisementController(IAdvertisementService adService, ILogger<AdvertisementController> logger)
        {
            _adService = adService;
            _logger = logger;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AdvertisementDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers([FromQuery] AdvertisementQueryParameters queryPageParameters)
        {
            var ads = await _adService.GetAllAsync(queryPageParameters);
            var metadata = ads.GetMetadata();
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata)); 
            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
            return Ok(ads);
        }

        [HttpPost("click")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status429TooManyRequests)]
        public IActionResult Click([FromBody] ClickRequestDto clickRequest)
        {
            // left it here if something goes wrong(it won't, I wrote it.)
            var email = clickRequest.Email ?? "anonymous@gmail.com";
            var package = clickRequest.Package ?? "Basic";
            _logger.LogInformation($"Click request from User: {email}, Package: {package}");
    
            return Ok("Click registered");
        }
    }
}

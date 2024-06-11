using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bsep_api.Extensions.Auth;
using bsep_bll.Contracts;
using bsep_bll.Dtos;
using bsep_bll.Dtos.Users;
using bsep_dll.Helpers.Pagination;
using bsep_dll.Helpers.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bsep_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _adService;

        public AdvertisementController(IAdvertisementService adService)
        {
            _adService = adService;
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
        
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiServerResponceCache.Services;
using System.Net;

namespace RestApiServerResponceCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpResponceCacheController : ControllerBase
    {
        private readonly IHttpResponseCache _httpResponseCache;

        public HttpResponceCacheController(IHttpResponseCache httpResponseCache)
        {
            _httpResponseCache = httpResponseCache;
        }

        [AllowAnonymous]
        [HttpPost(nameof(GetAllMessage))]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetAllMessage(string name)
        {
            var response = await _httpResponseCache.GetAllMessage(name);
            return Ok(response);

        }
    }
}

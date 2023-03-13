using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Models;
using URL_Shortener.Services;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromQuery] CreateRequest request)
        {
            try
            {
                var response = await _urlService.Create(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest("Wrong address");
            }
            catch (Exception)
            {
                return StatusCode(500, "Server Error");
            }
        }

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdRequest request)
        {
            try
            {
                var response = await _urlService.GetById(request);
                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server Error");
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] DeleteRequest request)
        {
            try
            {
                var response = await _urlService.Delete(request);
                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server Error");
            }
        }
    }
}

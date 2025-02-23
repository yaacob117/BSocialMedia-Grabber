using BSocialMedia_Grabber.Models;
using BSocialMedia_Grabber.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSocialMedia_Grabber.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ControllerBase
    {
        private readonly ScraperService _scraperService;
        public ScraperController(ScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        [HttpPost("Hashtags")]
        public async Task<IActionResult> ScrapeByHashtag([FromBody] ScrapeRequest request)
        {
            try
            {
                var result = await _scraperService.ScrapeHashtagsAsync(
                    request.Hashtags,
                    request.ResultsLimit,
                    request.SearchType
                    );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}") ;
            }
        }
    }
}

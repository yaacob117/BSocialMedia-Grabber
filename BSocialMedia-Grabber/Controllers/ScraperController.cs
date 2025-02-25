using BSocialMedia_Grabber.Models;
using BSocialMedia_Grabber.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSocialMedia_Grabber.Controllers
{
    /// <summary>
    /// Controlador para manejar las solicitudes relacionadas con el scraping de hashtags en Instagram.
    /// </summary>
    /// <remarks>
    /// Este controlador expone endpoints para realizar operaciones de scraping utilizando la API de Apify.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ControllerBase
    {
        private readonly ScraperService _scraperService;

        /// <summary>
        /// Constructor del controlador ScraperController.
        /// </summary>
        /// <param name="scraperService">Instancia del servicio ScraperService inyectada mediante dependencia.</param>
        /// <remarks>
        /// El servicio ScraperService se utiliza para realizar las operaciones de scraping.
        /// </remarks>
        public ScraperController(ScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        /// <summary>
        /// Realiza el scraping de hashtags en Instagram basado en los parámetros proporcionados.
        /// </summary>
        /// <param name="request">Objeto ScrapeRequest que contiene los parámetros de la solicitud.</param>
        /// <returns>Una respuesta HTTP con los resultados del scraping en formato JSON.</returns>
        /// <response code="200">Devuelve los resultados del scraping si la operación es exitosa.</response>
        /// <response code="500">Devuelve un mensaje de error si ocurre un problema durante el scraping.</response>
        /// <remarks>
        /// Ejemplo de solicitud:
        /// 
        ///     POST /api/scraper/hashtags
        ///     {
        ///         "hashtags": ["nature", "travel"],
        ///         "resultsLimit": 10,
        ///         "searchType": "top"
        ///     }
        ///     
        /// Este endpoint realiza una solicitud al servicio ScraperService para obtener datos relacionados con los hashtags especificados.
        /// </remarks>
        [HttpPost("Hashtags")]
        public async Task<IActionResult> ScrapeByHashtag([FromBody] ScrapeRequest request)
        {
            try
            {
                // Llama al servicio para realizar el scraping
                var result = await _scraperService.ScrapeHashtagsAsync(
                    request.Hashtags,
                    request.ResultsLimit,
                    request.SearchType
                );

                // Devuelve los resultados en una respuesta HTTP 200 OK
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Maneja errores internos y devuelve una respuesta HTTP 500
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
using Newtonsoft.Json;
using System.Text;

namespace BSocialMedia_Grabber.Services
{
    /// <summary>
    /// Servicio encargado de realizar el scraping de hashtags en Instagram utilizando la API de Apify.
    /// </summary>
    /// <remarks>
    /// Este servicio utiliza HttpClient para enviar solicitudes POST a la API de Apify y obtener datos relacionados con hashtags.
    /// </remarks>
    public class ScraperService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor de la clase ScraperService.
        /// </summary>
        /// <param name="httpClient">Instancia de HttpClient configurada para realizar solicitudes HTTP.</param>
        /// <remarks>
        /// El HttpClient se inyecta mediante dependencia para permitir la reutilización y mejorar el rendimiento.
        /// </remarks>
        public ScraperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Realiza el scraping de hashtags en Instagram utilizando la API de Apify.
        /// </summary>
        /// <param name="hashtags">Arreglo de cadenas que representan los hashtags a buscar.</param>
        /// <param name="resultslimit">Número máximo de resultados a obtener por cada hashtag.</param>
        /// <param name="searchtype">Tipo de búsqueda (por ejemplo, "top" o "recent").</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado es una cadena JSON con los datos obtenidos.</returns>
        /// <exception cref="HttpRequestException">Se lanza si la solicitud HTTP no tiene éxito.</exception>
        /// <remarks>
        /// Este método envía una solicitud POST a la API de Apify con los parámetros especificados y devuelve los resultados en formato JSON.
        /// Asegúrate de configurar la variable de entorno "APIFY_TOKEN" con un token válido antes de usar este servicio.
        /// </remarks>
        public async Task<string> ScrapeHashtagsAsync(string[] hashtags, int resultslimit, string searchtype)
        {
            // Construye la URL de la API de Apify con el token de autenticación
            var url = $"https://api.apify.com/v2/acts/apify~instagram-hashtag-scraper/run-sync-get-dataset-items?token={Environment.GetEnvironmentVariable("APIFY_TOKEN")}";

            // Crea el cuerpo de la solicitud con los parámetros proporcionados
            var requestbody = new
            {
                hashtags,
                resultslimit,
                searchtype,
            };

            // Serializa el cuerpo de la solicitud a JSON y lo envuelve en un StringContent
            var content = new StringContent(JsonConvert.SerializeObject(requestbody), Encoding.UTF8, "application/json");

            // Envía la solicitud POST a la API de Apify
            var response = await _httpClient.PostAsync(url, content);

            // Verifica si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Devuelve el contenido de la respuesta como una cadena JSON
                return await response.Content.ReadAsStringAsync();
            }

            // Lanza una excepción si la solicitud falla
            throw new HttpRequestException($"Error al hacer el scraping: {response.StatusCode}");
        }
    }
}
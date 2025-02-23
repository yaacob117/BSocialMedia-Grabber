using Newtonsoft.Json;

using System.Text;

namespace BSocialMedia_Grabber.Services
{
    public class ScraperService
    {
        private readonly HttpClient _httpClient;

        public ScraperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> ScrapeHashtagsAsync(string[] hashtags, int resultslimit, string searchtype)
        {
            var url = $"https://api.apify.com/v2/acts/apify~instagram-hashtag-scraper/run-sync-get-dataset-items?token={Environment.GetEnvironmentVariable("APIFY_TOKEN")}";
            var requestbody = new
            {
                hashtags,
                resultslimit,
                searchtype,
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestbody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new HttpRequestException($"Error al hacer el scraping: {response.StatusCode}");
        }
    }
}

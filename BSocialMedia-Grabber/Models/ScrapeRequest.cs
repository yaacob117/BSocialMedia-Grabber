namespace BSocialMedia_Grabber.Models
{
    public class ScrapeRequest
    {
        public string[]? Hashtags { get; set; }
        public int ResultsLimit { get; set; }
        public string? SearchType { get; set; }
    }
}

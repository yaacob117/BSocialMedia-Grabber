using Prometheus;

namespace BSocialMedia_Grabber.Services
{
    public class MetricsService
    {
        private static readonly Counter RequestCounter = Metrics.CreateCounter(
    "app_requests_total",
    "Número total de solicitudes recibidas");

        public void IncrementRequestCounter()
        {
            RequestCounter.Inc();
        }
    }
}

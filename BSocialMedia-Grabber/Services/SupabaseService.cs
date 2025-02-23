namespace BSocialMedia_Grabber.Services
{
    using Supabase;
    using Supabase.Interfaces;

    public class SupabaseService
    {
        private readonly Supabase.Client client;
        
        public SupabaseService(string url, string key)
        {
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            client = new Supabase.Client(url, key, options);

        }
      
        public async Task InitializeAsync()
            {
                await client.InitializeAsync();
            }

        public Supabase.Client GetClient()
        {
            return client;
        }

    }
}

namespace OK.Hookman.Client.Config
{
    public class HookmanClientConfig
    {
        public string ApiUrl { get; set; }

        public HookmanClientConfig()
        {

        }

        public HookmanClientConfig(string apiUrl)
        {
            this.ApiUrl = apiUrl;
        }
    }
}
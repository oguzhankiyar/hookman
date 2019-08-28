namespace OK.Hookman.UI.Config
{
    public class HookmanUIConfig
    {
        public string ApiUrl { get; set; }
        public string UIPath { get; set; }

        public HookmanUIConfig()
        {

        }

        public HookmanUIConfig(string apiUrl, string uiPath)
        {
            this.ApiUrl = apiUrl;
            this.UIPath = uiPath;
        }
    }
}
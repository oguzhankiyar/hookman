namespace OK.Hookman.API.Config
{
    public class HookmanAPIConfig
    {
        public string ApiPath { get; set; }
        
        public HookmanAPIConfig()
        {

        }

        public HookmanAPIConfig(string apiPath)
        {
            this.ApiPath = apiPath;
        }
    }
}
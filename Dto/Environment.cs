namespace Formation.MS.EnvSupport.Dto
{
    public class Environment
    {
        public int ID {get; set;}
        
        public string Application {get; set;}

        public string OperatingSystem {get; set;}

        public string Browser {get; set;}

        public float BrowserVersion {get; set;}

        public string WebServer {get; set;}

        public float WebServerVersion {get; set;}

        public float WordPressVersion {get; set;}

        public float PHPVersion {get; set;}

        public float MySQlVersion {get; set;}

        public bool Enabled {get; set;}
    }
}
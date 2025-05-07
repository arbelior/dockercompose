namespace drushim.Appsettings
{
    public class AppSettingsConfig
    {
        public IConfiguration _configuration { get; set; }
        public   AppSettingsConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetElkUrl()
        {
            string elkUrl = _configuration["ElkSetting:ElkURL"]; // הדרך הנכונה לקרוא את ההגדרה

            if (string.IsNullOrWhiteSpace(elkUrl))
            {
                throw new ArgumentException("Not found Elk URL setting", nameof(elkUrl));
            }

            return elkUrl;
        }

        public bool IsLoggingToFileEnabled()
        {
            string value = _configuration["StartWritetologfile"];

            if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out bool result))
            {
                return result;
            }

            return false;
        }


        public string GetHref(string hrefservice)
        {

            if(string.IsNullOrWhiteSpace(hrefservice))
                throw new ArgumentException("Not Get href service");

            string gethref = _configuration[$"Href:{hrefservice}"]; // הדרך הנכונה לקרוא את ההגדרה

            if (string.IsNullOrEmpty(gethref))
            {
                throw new ArgumentException("Not found Href href setting", nameof(gethref));

            }

            return gethref;
        }

        public string GetAppId()
        {
            return _configuration["LogAppIdSetting:appid"];
        }
    }
}

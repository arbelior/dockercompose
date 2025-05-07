namespace drushim.Models.Proxy
{
    public class ProxySettings
    {
        public string Address { get; set; }
        public string Port { get; set; }
        public bool BypassOnLocal { get; set; }
        public bool UseDefaultWebProxy { get; set; }
    }
}

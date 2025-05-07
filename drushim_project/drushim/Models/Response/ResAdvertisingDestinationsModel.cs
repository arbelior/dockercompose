using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResAdvertisingDestinationsModel 
    {
        [JsonPropertyName("AdvertisingDetsinationID")]
        public int AdvertisingDetsinationID { get; set; }

        [JsonPropertyName("AdvertisingDetsinationTarget")]
        public string AdvertisingDetsinationTarget { get; set; }
        public ResAdvertisingDestinationsModel() { }



    }
}

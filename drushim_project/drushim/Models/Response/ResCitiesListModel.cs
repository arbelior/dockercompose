using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResCitiesListModel 
    {
        [JsonPropertyName("CityCode")]
        public int CityCode { get; set; }

        [JsonPropertyName("CityDesc")]
        public string CityDesc { get; set; }

        [JsonPropertyName("AreaCode")]
        public int AreaCode { get; set; }

        [JsonPropertyName("English_City")]
        public string EnglishCity { get; set; }
        public ResCitiesListModel()
        {

        }
    }
}

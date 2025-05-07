using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResAreaModel 
    {
        [JsonPropertyName("AreaId")]
        public int AreaId { get; set; }

        [JsonPropertyName("AreaName")]
        public string AreaName { get; set; }
        public ResAreaModel()
        {

        }
    }
}

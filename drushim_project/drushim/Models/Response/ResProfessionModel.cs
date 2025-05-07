using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResProfessionModel 
    {
        [JsonPropertyName("ProfessionID")]
        public int ProfessionID { get; set; }

        [JsonPropertyName("ProfessionName")]
        public string ProfessionName { get; set; }

        public int areaid { get; set; }
        public ResProfessionModel()
        {

        }
    }
}

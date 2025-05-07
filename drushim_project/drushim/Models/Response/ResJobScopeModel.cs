using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResJobScopeModel 
    {
        [JsonPropertyName("JobScopeCode")]
        public int JobScopeCode { get; set; }

        [JsonPropertyName("JobScopeDescription")]
        public string JobScopeDescription { get; set; }
        public ResJobScopeModel() { }


    }
}

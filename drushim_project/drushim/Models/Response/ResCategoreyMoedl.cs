using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResCategoreyMoedl
    {
        [JsonPropertyName("CategoryID")]
        public int CategoryID { get; set; }

        [JsonPropertyName("CategoryName")]
        public string CategoryName { get; set; }

        public ResCategoreyMoedl() { }
    }
}

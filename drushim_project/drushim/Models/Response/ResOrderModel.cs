using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResOrderModel
    {
        [JsonPropertyName("Order")]
        public long Order { get; set; }

        [JsonPropertyName("Desc")]
        public string Desc { get; set; }

        [JsonPropertyName("NameBranch")]
        public string NameBranch { get; set; }

        [JsonPropertyName("CodeBranch")]
        public int CodeBranch { get; set; }

        public ResOrderModel() { }


    }
}

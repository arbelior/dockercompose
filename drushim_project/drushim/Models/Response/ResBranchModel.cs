using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResBranchModel
    {
        [JsonPropertyName("BranchID")]
        public int BranchID { get; set; }

        [JsonPropertyName("CompanyID")]
        public string CompanyID { get; set; }  

        [JsonPropertyName("BranchName")]
        public string BranchName { get; set; }

        [JsonPropertyName("Street")]
        public string Street { get; set; }

        [JsonPropertyName("HouseNumber")]
        public string HouseNumber { get; set; }

        [JsonPropertyName("City")]
        public string City { get; set; }

        [JsonPropertyName("PostBoxTown")]
        public string? PostBoxTown { get; set; }

        [JsonPropertyName("PostBoxNumber")]
        public string? PostBoxNumber { get; set; }

        [JsonPropertyName("Phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("AdditionalPhone")]
        public string? AdditionalPhone { get; set; }

        [JsonPropertyName("Fax")]
        public string? Fax { get; set; }

        [JsonPropertyName("SecretaryName")]
        public string? SecretaryName { get; set; }

        [JsonPropertyName("BranchEmail")]
        public string? BranchEmail { get; set; }

        [JsonPropertyName("ExternalBranchID")]
        public string ExternalBranchID { get; set; }  // שונה ל-string

        [JsonPropertyName("ClusterID")]
        public string ClusterID { get; set; }  // שונה ל-string

        [JsonPropertyName("ClusterName")]
        public string ClusterName { get; set; }

        public ResBranchModel() { }
    }
}

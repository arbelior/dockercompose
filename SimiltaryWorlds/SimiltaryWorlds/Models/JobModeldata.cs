namespace SimiltaryWorlds.Models
{
    public class JobModeldata
    {
        public int order_id { get; set; }
        public string description { get; set; }
        public int ProffesionID { get; set; }

        public string notes { get; set; }

        public JobModeldata()
        {
            description = string.Empty;
        }
    }
}

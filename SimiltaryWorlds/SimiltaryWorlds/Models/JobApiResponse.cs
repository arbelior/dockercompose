namespace SimiltaryWorlds.Models
{
    public class JobApiResponse
    {
        public bool isSuccessfull { get; set; }
        public string error { get; set; }
        public List<JobModeldata> data { get; set; }

        public JobApiResponse()
        {
            isSuccessfull = false;
            error = string.Empty;

        }
    }
}

namespace drushim.Models.Response
{
    public class HomeResponse
    {
        public List<ResAreaModel> areas { get; set; }

        public List<ResProfessionModel> activeProfessions { get; set; }

        public List<JobModel> jobs { get; set; }
        public HomeResponse()
        {
                
        }
    }
}

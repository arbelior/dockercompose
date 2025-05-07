using SimiltaryWorlds.Models;

namespace SimiltaryWorlds.Helpers
{
    public static class ConvertDataToClientModel
    {
        public static List<Job> ConvertToJob(List<JobModeldata> jobs)
        {
            List<Job> result = new List<Job>();

            if (jobs != null && jobs.Count > 0)
            {

                foreach (var job in jobs)
                {
                    Job addjob = new Job()
                    {
                        JobName = job.description.Trim(),
                        JobId   = job.ProffesionID,
                        OrderId = job.order_id,
                        notes = job.notes,

                    };

                    if(addjob.JobName != null && !result.Contains(addjob))
                    {
                        result.Add(addjob);
                    }
                }
            }

            return result;
        }
    }
}

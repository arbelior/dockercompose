using SimiltaryWorlds.Models;

namespace SimiltaryWorlds.Services
{
    public interface IfreeSearch
    {
        Task<List<Job>> GetAllJobs();
        Task<List<Job>> GetRelevantJobs(string userSentence, List<Job> jobs);
    }
}

using SimiltaryWorlds.Models;

namespace SimiltaryWorlds.Services
{
    public interface IJobService
    {
        Task<List<Job>> GetSimilarJobsAsync(string userSentence);
        Task InitializeEmbeddingsAsync();
        Task SaveUniqueWordEmbeddingsToFileAsync(string filePath);
        Task<List<Job>> GetSimilarJobs(string userSentence, double tresh_hold, int totalorders, List<Job>? filteredJobs = null);  
    }
}

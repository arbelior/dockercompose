using SimiltaryWorlds.Models;

namespace SimiltaryWorlds.Services
{
    public interface Ifacadelogic
    {
        List<CitybyJob> GetequalData(string user_Sentence_include_city, List<Job> jobsbycategorey);

        List<Job> GetJobs(List<CitybyJob> jobs);

        List<Job> GetJobsWithWriteCityNotFoundJobsCategorey(string city);

        Task<List<Job>> GetAllJobsIfAsk(string user_sentnce);

        List<Job> GetRelevantJobSimilarByArea(List<string> cities, List<Job> jobs);

        Task<List<Job>> GetAllJobs();

        bool ContainsGlobalWordsPartially(string userSentence);
        Task UpdateHobsCategorey();
        Task<List<Job>> LoadJobsFromFileAsync();
        Task SaveJobsToFileAsync();

        bool AreJobListsFullyEqualIgnoreOrder(List<Job> list1, List<Job> list2);

        List<Job> GetNewJobsDifference(List<Job> newList, List<Job> oldList);

        Task SaveJobDifferencesToFileAsync(List<Job> getdiffernceJobs);
        Task<bool> RemoveJobByOrderIdAsyncFromFoundDiffernceJobsFle(int orderId);

        Task<List<Job>> GetJobsFromFile();

        Task<AiResponse> GetProfessionFromSentence(string sentence);

        Task<string> GetProfessionFromSentenceAi(string sentence);
     }
}

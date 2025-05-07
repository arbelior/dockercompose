using SimiltaryWorlds.Models;

namespace SimiltaryWorlds.Services
{
    public interface ISearchService
    {
        List<int> AnalyzeSearch(string input);
        List<Job> GetRellevatJobsbyCategorey(List<int> ids);
        bool checkIfCategoreyNotExist(string user_Sentence);
    }
}

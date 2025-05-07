namespace SimiltaryWorlds.Services
{
    public interface ISearchCityServer
    {
        //string? GetCityFromSentence(string sentence);
        //string NormalizeWord(string word);
        //string NormalizeSentence(string sentence);
        //List<string> GetAllCitiesFromSentence(string sentence);

        public string DetectCityFromText(string input);

        List<string> GetCitiesByArea(string sentenceUser);
    }
}

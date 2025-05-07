namespace drushim.Models.AiModels.Response
{
    public class WordResponse
    {
        public List<string> SimilarWords { get; set; }

        public WordResponse()
        {
            SimilarWords = new List<string>();
        }
    }
}

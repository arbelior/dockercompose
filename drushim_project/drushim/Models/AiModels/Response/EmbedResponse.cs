namespace drushim.Models.AiModels.Response
{
    public class EmbedResponse
    {
        public Dictionary<string, float> SimilarWords { get; set; }

        public EmbedResponse()
        {
            SimilarWords = new Dictionary<string, float>();
        }

    }
}

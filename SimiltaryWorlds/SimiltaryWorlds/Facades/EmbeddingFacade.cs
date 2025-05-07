using System.Text.Json;
using System.Text;

namespace SimiltaryWorlds.Facades
{
    public class EmbeddingFacade
    {
        private readonly HttpClient _httpClient;
        private readonly string _pythonApiUrl;

        public EmbeddingFacade(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _pythonApiUrl = configuration["PythonApiUrl"];
        }

        public async Task<float[]> GetEmbeddingAsync(string sentence)
        {
            var requestContent = new StringContent(
         JsonSerializer.Serialize(new { sentence }),
         Encoding.UTF8,
         "application/json"
             );

            var response = await _httpClient.PostAsync($"{_pythonApiUrl}/embed", requestContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseContent);
            var embeddingElement = doc.RootElement.GetProperty("embedding");

            float[] vector = embeddingElement.EnumerateArray()
                                             .Select(e => e.GetSingle())
                                             .ToArray();
            return vector;
        }




        //public async Task<List<EmbeddingResponse>> GenerateEmbeddingsFromFileAsync(string inputFilePath, string outputFilePath, string pythonApiUrl)
        //{
        //    var content = await File.ReadAllTextAsync(inputFilePath);
        //    var phrases = JsonSerializer.Deserialize<Dictionary<string, string>>(content);

        //    var results = new List<EmbeddingResult>();

        //    foreach (var phrase in phrases.Keys)
        //    {
        //        var embedding = await GetEmbeddingFromPythonAsync(phrase, pythonApiUrl);
        //        results.Add(new EmbeddingResult
        //        {
        //            Sentence = phrase,
        //            Embedding = embedding
        //        });
        //    }

        //    // שמירה לקובץ JSON
        //    var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        //    await File.WriteAllTextAsync(outputFilePath, json);

        //    return results;
        //}







        public class EmbeddingResponse
        {
            public float[] Embedding { get; set; }

        }


    }

}

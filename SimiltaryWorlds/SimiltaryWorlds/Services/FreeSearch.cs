using Newtonsoft.Json;
using SimiltaryWorlds.Models;
using System.Net.Http;
using System;
using System.Text;
using System.Text.Json;
using SimiltaryWorlds.Helpers;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SimiltaryWorlds.Services
{
    public class FreeSearch : IfreeSearch
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration configuration;
        public FreeSearch(HttpClient httpClient , IHttpClientFactory httpClientFactory, IConfiguration _configuration)
        {
            _httpClientFactory = httpClientFactory;
            configuration = _configuration;
        }

        public async Task<List<Job>> GetAllJobs()
        {
            List<Job> jobs = new List<Job>();

            try
            {
                string url = configuration["href"] ?? "";

                if (string.IsNullOrEmpty(url))
                    throw new Exception("Not Found The Url from the config");

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "text/plain");

                HttpClient http = _httpClientFactory.CreateClient();

                var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await System.Text.Json.JsonSerializer.DeserializeAsync<JobApiResponse>(responseStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                jobs = ConvertDataToClientModel.ConvertToJob(result?.data ?? new List<JobModeldata>());
                return jobs;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private List<string> ExpandGenderNeutralWords(string sentence)
        {
            var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var expandedWords = new List<string>();

            foreach (var word in words)
            {
                if (word.Contains("."))
                {
                    var parts = word.Split('.');
                    if (parts.Length == 2 && !string.IsNullOrEmpty(parts[0]) && !string.IsNullOrEmpty(parts[1]))
                    {
                        expandedWords.Add(parts[0]); // נציג
                        expandedWords.Add(parts[0] + parts[1]); // נציגת
                        continue;
                    }
                }

                expandedWords.Add(word);
            }

            return expandedWords;
        }

        private string NormalizeHebrewWord(string word)
        {
            string[] hebrewPrefixes = new[] { "ב", "ל", "כ", "מ", "ה", "ש" };

            if (string.IsNullOrEmpty(word)) return word;
            while (word.Length > 2 && hebrewPrefixes.Contains(word.Substring(0, 1)))
            {
                word = word.Substring(1);
            }
            return word;
        }

        public async Task<List<Job>> GetRelevantJobs(string userSentence, List<Job> jobs)
        {
            if (string.IsNullOrWhiteSpace(userSentence))
                throw new ArgumentNullException(nameof(userSentence), "Missing user sentence");

            if (jobs == null || !jobs.Any())
                throw new ArgumentNullException(nameof(jobs), "Jobs list is empty");

            var words = userSentence.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var results = new List<Job>();

            foreach (var word in words)
            {
                foreach (var job in jobs)
                {
                    if (!results.Contains(job) && !string.IsNullOrEmpty(job.JobName))
                    {
                        var jobWords = job.JobName.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        if (jobWords.Contains(word, StringComparer.OrdinalIgnoreCase))
                        {
                            results.Add(job);
                            break;
                        }
                        else
                        {
                            if(EqualBetWordToSentence(word,jobWords.ToList()))
                            {
                                results.Add(job);
                                break;
                            }


                            if (globalwordgender(word, job.JobName))
                            {
                                results.Add(job);
                                break;
                            }

                        }

                    }
                }
            }

            return results;
        }

        private bool EqualBetWordToSentence(string word , List<string> words_sentence) 
        {
            word = word.Trim();
            string word_without_perfix = NormalizeHebrewWord(word);

            foreach (var word_sentence in words_sentence)
            {
                if (string.IsNullOrWhiteSpace(word_sentence)) continue;


                if (word_sentence.Equals(word_without_perfix,StringComparison.OrdinalIgnoreCase))
                  return true;
                

                string word_sentence_without_perfix = NormalizeHebrewWord(word_sentence);
                if(word_sentence_without_perfix.Equals(word,StringComparison.OrdinalIgnoreCase))
                  return true;
                
                if(word_without_perfix.Equals(word_sentence_without_perfix, StringComparison.OrdinalIgnoreCase))
                    return true;

            }

            return false;
        }

        private bool globalwordgender(string word_user, string sentence_job)
        {
            List<string> wordgender = new List<string>();

            wordgender = ExpandGenderNeutralWords(sentence_job);

            if(wordgender != null && wordgender.Count() > 0)
            {
                if (wordgender.Any(g => g.Equals(word_user, StringComparison.OrdinalIgnoreCase)))
                    return true;
                     
                
            }

            return false;
        }


    }
}

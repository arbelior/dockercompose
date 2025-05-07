






using SimiltaryWorlds.Models;
using SimiltaryWorlds.Facades;
using SimiltaryWorlds.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace SimiltaryWorlds.Services
{
    public class JobService : IJobService
    {
        private readonly List<Job> _jobs;
        private readonly EmbeddingFacade _embeddingFacade;
        private readonly CityHelper _cityHelper;
        private static readonly HttpClient _httpClient = new HttpClient();
        List<Job> GetRelevantJobs = new List<Job>();
        private readonly IfreeSearch _freeSearchService;

        List<SimilarWord> similaritywordsVacabulary = new List<SimilarWord>()
            {
                new SimilarWord{key = "תחזוקה", value="עובד.ת אחזקה ותפעול"},
                new SimilarWord{key = "אחזקה", value="עובד.ת אחזקה ותפעול"}

            };

        List<Job> similitaryword_vac = new List<Job>();
        public JobService(EmbeddingFacade embeddingFacade, CityHelper cityHelper , IfreeSearch freesearchserver)
        {
            _embeddingFacade = embeddingFacade;
            _cityHelper = cityHelper;
            _freeSearchService = freesearchserver;

         

            _jobs = new List<Job>
            {
                new Job { JobId = 29, JobName = "מנהל.ת נכסים", OrderId = 2702 },
                new Job { JobId = 28, JobName = "עובד.ת אחזקה ותפעול", OrderId = 2689 },
                new Job { JobId = 27, JobName = "מתמחה ללשכה המשפטית חולון", OrderId = 2688 },
                new Job { JobId= 25, JobName= "מנהל.ת שיווק לקוחות מגזר פרטי",OrderId= 2675  },
                new Job { JobId= 9, JobName= "מומחה.ית תקשורת ואבטחת מידע לצוות אינטגרציה- חולון",OrderId= 2650},
                new Job { JobId= 24, JobName= "נציג.ת קידום ותמיכת הזמנות בחולון",OrderId= 2623  },
                new Job { JobId= 13, JobName= "מרכז שרות וחווית לקוח מודיעין עילית",OrderId= 2575  },
                new Job { JobId= 15, JobName= "אנשי.נשות מכירות שטח בעולמות הסיבים",OrderId= 2391  },
                new Job { JobId= 13, JobName= "נציג.ה מרכז תמיכה אסטרטגי מסחרי חולון",OrderId= 2154},
                new Job { JobId= 14, JobName= "נציג.ה במרכז תמיכה מסחרי עסקי- י\"ם",OrderId= 2084},
                new Job { JobId= 13, JobName= "יועצים.יועצות לשימור ופרסום דיגיטלי b144 ת\\\"א\"",OrderId= 2023},
                new Job { JobId= 19, JobName= "עובדי.ות תשתיות",OrderId= 2011},
                new Job { JobId= 13, JobName= "נציג.ה במרכז שרות וחווית לקוח- ב\"ש",OrderId= 1031},
                new Job { JobId= 14, JobName= "נציג.ת ייעוץ פרסום דיגיטלי לעסקים מוקד חיפה",OrderId= 1028},
                new Job { JobId= 21, JobName= "טכנאי.ת שירותי לקוחות", OrderId=1026 },
                new Job { JobId= 13, JobName= "נציג.ת שירות לעסקים קטנים", OrderId=1016 },
                new Job { JobId= 14, JobName= "נציג.ת מוקד מכירות ירושלים", OrderId=1012 },
                new Job { JobId= 13, JobName= "נציג.ה במרכז שרות וחווית לקוח חיפה", OrderId=1010 },
                new Job { JobId= 13, JobName= "נציג.ה במרכז שירות וחווית לקוח ת\"א", OrderId=1009 },
                new Job { JobId= 17, JobName= "נציג.ה במרכז תמיכה וחווית לקוח חיפה", OrderId=1008 },
                new Job { JobId= 17, JobName= "נציג.ה במרכז תמיכה וחווית לקוח באר שבע", OrderId=1007 },
                new Job { JobId= 17, JobName= "נציג.ה במרכז תמיכה וחוויית לקוח ירושלים", OrderId=1005 },
            };
        }

           public async Task<List<Job>> GetSimilarJobs(string userSentence, double tresh_hold, int totalorders, List<Job>? filteredJobs = null)
        {
            var userEmbedding = await  _embeddingFacade.GetEmbeddingAsync(userSentence);

            var jobsWithEmbeddings = await LoadEmbeddingsFromFileAsync("embeddings.json");

            var jobsToCompare = (filteredJobs != null && filteredJobs.Any())
                ? jobsWithEmbeddings.Where(j => filteredJobs.Any(f => f.OrderId == j.OrderId)).ToList()
                : jobsWithEmbeddings;

            var relevantJobs = new List<Job>();

            foreach (var job in jobsToCompare)
            {
                if (job.Embedding != null)
                {
                    var similarity = CosineSimilarityHelper.ComputeCosineSimilarity(userEmbedding, job.Embedding);
                    if (similarity >= tresh_hold)
                    {
                        job.similitaryjob = similarity;
                        relevantJobs.Add(job);
                    }
                }
            }

            var getrelevant =  relevantJobs.OrderByDescending(j => j.similitaryjob).Take(totalorders).ToList();

            List<Job> getreleventjobs = new List<Job>();

            foreach (var item in getrelevant)
            {
                Job addtojob = new Job()
                {
                   JobId = item.JobId,
                 JobName  =item.JobName,
                 OrderId = item.OrderId
                };

                
                if(!getreleventjobs.Contains(addtojob))
                {
                    getreleventjobs.Add(addtojob);
                }
            }

            return getreleventjobs;
        }

        public async Task<List<Job>> GetSimilarJobsAsync(string userSentence)
        {
            var words = userSentence.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            //צריך לייצר פונקציה שתחזיר את המילים הנכונות
            //if(_jobs.Any(x => userSentence.Contains(x.JobName)))
            //{
            //    var sentence_similar = _jobs.Where(x => x.JobName == userSentence).FirstOrDefault();

            //    if(sentence_similar != null)
            //    {
            //        Job 
            //    }
            //}

            var matchedJobsByWord = new HashSet<Job>();

            foreach (var word in words)
            {
                var wordEmbedding = await _embeddingFacade.GetEmbeddingAsync(word);

                foreach (var job in _jobs)
                {
                    var jobWords = job.JobName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var jw in jobWords)
                    {
                        var jobWordEmbedding = await _embeddingFacade.GetEmbeddingAsync(jw);
                        var sim = CosineSimilarityHelper.ComputeCosineSimilarity(wordEmbedding, jobWordEmbedding);
                        if (sim >= 0.83f)
                        {
                            matchedJobsByWord.Add(job);
                            break;
                        }
                    }

                    if (matchedJobsByWord.Count() >= 3)
                        break;
                }

                if (matchedJobsByWord.Count >= 3)
                {
                    var user_Embedding = await _embeddingFacade.GetEmbeddingAsync(userSentence);

                    return matchedJobsByWord
                        .Select(job =>
                        {
                            job.similitaryjob = CosineSimilarityHelper.ComputeCosineSimilarity(user_Embedding, _embeddingFacade.GetEmbeddingAsync(job.JobName).Result);
                            return job;
                        })
                        .OrderByDescending(j => j.similitaryjob)
                        .Take(3)
                        .ToList();
                }
            }

            var filteredByCity = new List<Job>();
            var city = _cityHelper.GetCityFromSentence(userSentence);

            if (!string.IsNullOrEmpty(city))
            {
                foreach (var item in _jobs)
                {
                    var cityFromArray = _cityHelper.GetCityFromSentence(item.JobName) ??
                                        _cityHelper.GetCityFromSentence(_cityHelper.NormalizeSentence(item.JobName));

                    if (!string.IsNullOrEmpty(cityFromArray) &&
                        cityFromArray.Equals(city, StringComparison.CurrentCultureIgnoreCase))
                    {
                        filteredByCity.Add(item);
                    }
                }
            }
            else
            {
                city = _cityHelper.GetCityFromSentence(_cityHelper.NormalizeSentence(userSentence));

                if (!string.IsNullOrEmpty(city))
                {
                    foreach (var item in _jobs)
                    {
                        var cityFromArray = _cityHelper.GetCityFromSentence(item.JobName) ??
                                            _cityHelper.GetCityFromSentence(_cityHelper.NormalizeSentence(item.JobName));

                        if (!string.IsNullOrEmpty(cityFromArray) &&
                            cityFromArray.Equals(city, StringComparison.CurrentCultureIgnoreCase))
                        {
                            filteredByCity.Add(item);
                        }
                    }
                }
            }

            var userEmbedding = await _embeddingFacade.GetEmbeddingAsync(userSentence);
            var jobsWithEmbeddings = await LoadEmbeddingsFromFileAsync("embeddings.json");

            var targetJobs = matchedJobsByWord.Any()
                ? jobsWithEmbeddings.Where(j => matchedJobsByWord.Any(m => m.OrderId == j.OrderId)).ToList()
                : filteredByCity.Any()
                    ? jobsWithEmbeddings.Where(j => filteredByCity.Any(c => c.OrderId == j.OrderId)).ToList()
                    : jobsWithEmbeddings;

            var similarJobs = new List<Job>();

            foreach (var job in targetJobs)
            {
                if (job.Embedding != null)
                {
                    var similarity = CosineSimilarityHelper.ComputeCosineSimilarity(userEmbedding, job.Embedding);
                    job.similitaryjob = (double)similarity;

                    if (similarity >= 0.6f)
                    {
                        similarJobs.Add(job);
                    }
                }
            }

            return similarJobs.OrderByDescending(x => x.similitaryjob).Take(3).ToList();
        }

        public async Task SaveUniqueWordEmbeddingsToFileAsync(string filePath)
        {
            if (File.Exists(filePath)) return;

            var uniqueWords = new HashSet<string>();
            foreach (var job in _jobs)
            {
                var words = job.JobName.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var word in words)
                {
                    uniqueWords.Add(word);
                }
            }

            var embeddings = new Dictionary<string, float[]>();
            foreach (var word in uniqueWords)
            {
                var embedding = await _embeddingFacade.GetEmbeddingAsync(word);
                if (embedding != null)
                {
                    embeddings[word] = embedding;
                }
            }

            var json = JsonSerializer.Serialize(embeddings);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task InitializeEmbeddingsAsync()
        {
            foreach (var job in _jobs)
            {
                job.Embedding = await _embeddingFacade.GetEmbeddingAsync(job.JobName);
            }
        }

        //public async Task InitailizeEmbeddingFile()
        //{
        //    List<Job> Alljobs = new List<Job>();

        //    Alljobs = await _freeSearchService.GetAllJobs();

        //    if(Alljobs != null && Alljobs.Count() > 0 )
        //    {
        //        foreach (var job in Alljobs) 
        //        {
        //            job.Embedding = await _embeddingFacade.GetEmbeddingAsync(job.JobName);

        //        }
        //    }
        //}

        public async Task SaveEmbeddingsToFileAsync(string filePath)
        {
            await InitializeEmbeddingsAsync();
            var json = JsonSerializer.Serialize(_jobs);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<List<Job>> LoadEmbeddingsFromFileAsync(string filePath)
        {
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                await SaveEmbeddingsToFileAsync(filePath);
            }

            var json = await File.ReadAllTextAsync(filePath);
            var jobs = JsonSerializer.Deserialize<List<Job>>(json);
            return jobs ?? new List<Job>();
        }

        public async Task<Dictionary<string, float[]>> LoadWordEmbeddingsAsync(string filePath)
        {
            if (!File.Exists(filePath)) return new Dictionary<string, float[]>();

            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, float[]>>(json) ?? new Dictionary<string, float[]>();
        }
    }
}




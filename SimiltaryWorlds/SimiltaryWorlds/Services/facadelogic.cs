using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using SimiltaryWorlds.Models;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;
using ChatMessage = OpenAI.ObjectModels.RequestModels.ChatMessage;

namespace SimiltaryWorlds.Services
{
    public class facadelogic : Ifacadelogic
    {
        private readonly List<Job> _jobs;
        ISearchCityServer _search_city_Service;
        List<CitybyJob> similartlist_jobs_bycity_user = new List<CitybyJob>();
        List<Job> Relevantjobs = new List<Job>();
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _citiesMap;
        private readonly string _openAiApiKey;
        private readonly IOpenAIService _openAi;


        private static readonly HashSet<string> GlobalWords = new(
            new[]
            {
        "כל המשרות", "כל המשרות שיש לכם להציע", "תביא לי הכל", "כל המשרות ", "תביא לי את כל המשרות", "הכל","משרות"
            }.Select(s => s.Trim())
        );

        public facadelogic(Dictionary<string, string> citiesMap , ISearchCityServer search_city_Service, HttpClient httpClient, string openAiApiKey, IOpenAIService openAi)
        {
            _search_city_Service = search_city_Service;
            _citiesMap = citiesMap;
            _openAi = openAi; 
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
                //new Job { JobId= 17, JobName= "מתכנת אנגולר", OrderId=3077 },

            };
            _httpClient = httpClient;
            _openAiApiKey = openAiApiKey;
        }


        public async Task<AiResponse> GetProfessionFromSentence(string sentence)
        {
            var url = "http://localhost:5005/get-profession";

            var json = JsonSerializer.Serialize(new { sentence });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AiResponse>(responseContent);
        }




        public async Task<string> GetProfessionFromSentenceAi(string userInput)
        {
            var url = "https://api.openai.com/v1/chat/completions";

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "אתה מזהה את שם המקצוע מתוך משפט בעברית." },
                new { role = "user", content = $"המשפט הבא מתאר חיפוש עבודה: \"{userInput}\". מהו שם איש המקצוע שמוזכר בו? החזר רק את שם המקצוע." }
            },
                temperature = 0.2
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_openAiApiKey}");
            _httpClient.DefaultRequestHeaders.Add("OpenAI-Project", _openAiApiKey);
            var response = await _httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"שגיאה: {response.StatusCode} - {responseContent}";
            }

            using var doc = JsonDocument.Parse(responseContent);
            var result = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return result?.Trim() ?? "לא נמצאה תשובה";
        }





        public async Task<bool> RemoveJobByOrderIdAsyncFromFoundDiffernceJobsFle(int orderId)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "FoundDiffernceJobs.json");

            if (!File.Exists(filePath))
                return false; // הקובץ לא קיים בכלל

            var json = await File.ReadAllTextAsync(filePath);
            if (string.IsNullOrWhiteSpace(json))
                return false; // אין תוכן בקובץ

            List<Job> jobs = JsonSerializer.Deserialize<List<Job>>(json) ?? new List<Job>();

            var jobToRemove = jobs.FirstOrDefault(j => j.OrderId == orderId);
            if (jobToRemove == null)
                return false; // לא נמצא כזה job

            jobs.Remove(jobToRemove);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var updatedJson = JsonSerializer.Serialize(jobs, options);

            await File.WriteAllTextAsync(filePath, updatedJson);

            return true; // ההסרה בוצעה בהצלחה
        }

        public async Task<List<Job>> LoadJobsFromFileAsync()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "FoundDiffernceJobs.json");

            if (!File.Exists(filePath))
            {
                // הקובץ לא קיים - מחזירים רשימה ריקה
                return new List<Job>();
            }

            var json = await File.ReadAllTextAsync(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                // הקובץ קיים אבל ריק - גם כאן מחזירים רשימה ריקה
                return new List<Job>();
            }

            var jobs = JsonSerializer.Deserialize<List<Job>>(json);
            return jobs ?? new List<Job>(); // אם משום מה יצא null

        }

        public async Task SaveJobDifferencesToFileAsync(List<Job> getdiffernceJobs)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "FoundDiffernceJobs.json");

            // בדוק אם הקובץ קיים
            List<Job> existingJobs = new List<Job>();

            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    try
                    {
                        existingJobs = JsonSerializer.Deserialize<List<Job>>(json) ?? new List<Job>();
                    }
                    catch
                    {
                        // אם יש שגיאה בפרמוט, נניח שאין נתונים קיימים
                        existingJobs = new List<Job>();
                    }
                }
            }

            // מצא רק את המשרות שלא קיימות בקובץ לפי JobId + OrderId
            var newJobs = getdiffernceJobs
                .Where(job => !existingJobs.Any(ej => ej.JobId == job.JobId && ej.OrderId == job.OrderId))
                .ToList();

            if (newJobs.Any())
            {
                existingJobs.AddRange(newJobs);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var updatedJson = JsonSerializer.Serialize(existingJobs, options);

                await File.WriteAllTextAsync(filePath, updatedJson);
            }
        }






        public async Task SaveJobsToFileAsync()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // כדי לשמור עברית קריאה
            };

            var json = JsonSerializer.Serialize(_jobs, options);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "JobsOffer.json");
            await File.WriteAllTextAsync(filePath, json);
        }

        public List<Job> GetNewJobsDifference(List<Job> newList, List<Job> oldList)
        {
            // מיפוי הרשימה הישנה להשוואה קלה לפי jobId, jobName, orderId
            var oldCompareSet = oldList
                .Select(j => new { j.JobId, j.JobName, j.OrderId })
                .ToHashSet();

            // החזרת רק האובייקטים החדשים שאינם קיימים ברשימה הישנה
            var differences = newList
                .Where(j => !oldCompareSet.Contains(new { j.JobId, j.JobName, j.OrderId }))
                .ToList();

            return differences;
        }

        public async Task<List<Job>> GetJobsFromFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "FoundDiffernceJobs.json");

            if (!File.Exists(filePath))
                return new List<Job>(); // או לזרוק שגיאה, אם חשוב לך שהקובץ תמיד יהיה

            var json = await File.ReadAllTextAsync(filePath);
            var jobs = JsonSerializer.Deserialize<List<Job>>(json);

            return jobs ?? new List<Job>();
        }

        public bool AreJobListsFullyEqualIgnoreOrder(List<Job> list1, List<Job> list2)
        {
            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            var sorted1 = list1.OrderBy(j => j.JobId).ThenBy(j => j.OrderId).ToList();
            var sorted2 = list2.OrderBy(j => j.JobId).ThenBy(j => j.OrderId).ToList();

            for (int i = 0; i < sorted1.Count; i++)
            {
                var job1 = sorted1[i];
                var job2 = sorted2[i];

                if (job1.JobId != job2.JobId ||
                    job1.JobName != job2.JobName ||
                    job1.OrderId != job2.OrderId)
                {
                    return false;
                }
            }

            return true;
        }



        public async Task UpdateHobsCategorey()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "categories.json");

            // שלב 1: קריאה שורה-שורה כדי לשמור הערות
            var allLines = await File.ReadAllLinesAsync(filePath);

            // שלב 2: שמירת הערות בצד והכנת JSON נקי לפענוח
            var jsonLinesOnly = allLines
                .Where(line => !line.TrimStart().StartsWith("//"))
                .ToArray();

            var cleanedJson = string.Join(Environment.NewLine, jsonLinesOnly);

            // שלב 3: פיענוח JSON (מילון)
            var categories = JsonSerializer.Deserialize<Dictionary<string, List<int>>>(cleanedJson);

            if (categories == null)
                throw new Exception("ה־JSON לא נטען כראוי.");

            // שלב 4: עדכון ערך
            categories["צלם"] = new List<int> { 5 };

            // שלב 5: המרה ל־JSON בטקסט עברי קריא
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var updatedJson = JsonSerializer.Serialize(categories, options);

            // שלב 6: פיצול לשורות כדי לשלב חזרה את ההערות המקוריות
            var updatedLines = updatedJson.Split(Environment.NewLine);

            // שלב 7: שילוב הערות ישנות במיקומים המקוריים (שיטה פשוטה – הוספה בתחילת הקובץ)
            var commentLines = allLines.Where(line => line.TrimStart().StartsWith("//")).ToList();
            var finalLines = new List<string>();
            finalLines.AddRange(commentLines);
            finalLines.AddRange(updatedLines);

            // שלב 8: שמירה חזרה לקובץ
            await File.WriteAllLinesAsync(filePath, finalLines);
        }

        public List<Job> GetRelevantJobSimilarByArea(List<string> cities , List<Job> jobs)
        {
            List<Job> GetRelJobsWithSameCities = new List<Job>();
            foreach (var job in jobs)
            {
                var list_city_job = _search_city_Service.GetCitiesByArea(job.JobName).ToList();

                if(list_city_job.Count() > 0)
                {
                    foreach (var city in list_city_job)
                    {
                        if(cities.Contains(city))
                        {
                            if(!GetRelJobsWithSameCities.Contains(job))
                            {
                                GetRelJobsWithSameCities.Add(job);
                            }
                        }
                    }
                }
            }

            return GetRelJobsWithSameCities;
        }

        public List<CitybyJob> GetequalData(string user_Sentence_include_city , List<Job> jobsbycategorey)
        {
            if (jobsbycategorey.Count() > 0)
            {

                if (!string.IsNullOrEmpty(user_Sentence_include_city))
                {
                    this._search_city_Service = new SearchCityServer(_citiesMap);
                    foreach (var job in jobsbycategorey)
                    {
                        var check_if_found_city_job = _search_city_Service.DetectCityFromText(job.JobName);

                        if(!string.IsNullOrEmpty(check_if_found_city_job) && check_if_found_city_job.Equals(user_Sentence_include_city,StringComparison.CurrentCultureIgnoreCase))
                        {
                            //נמצא עיר זהה במשפט ביחס לעיר של המשתמש

                            if(!similartlist_jobs_bycity_user.Any(x => x.OrderId.Equals(job.OrderId)))
                            {
                                CitybyJob additemequal_city = new CitybyJob()
                                {
                                    OrderId = job.OrderId,
                                    similar_city_to_cityUser = check_if_found_city_job
                                };

                                if(!similartlist_jobs_bycity_user.Contains(additemequal_city))
                                {
                                    similartlist_jobs_bycity_user.Add(additemequal_city);
                                }
                            }

                         
                        }

                        else if(!string.IsNullOrEmpty(check_if_found_city_job) && !check_if_found_city_job.Equals(user_Sentence_include_city, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // נמצאו עיר שונה במשפט ביחס למה שרשם המשתמש
                        }
                        else
                        {
                            //  לא נמצא עיר במשפט מוסיף את המשפט

                            if (!similartlist_jobs_bycity_user.Any(x => x.OrderId.Equals(job.OrderId)))
                            {
                                CitybyJob additemequal_city = new CitybyJob()
                                {
                                    OrderId = job.OrderId,
                                    similar_city_to_cityUser = check_if_found_city_job
                                };

                                if (!similartlist_jobs_bycity_user.Contains(additemequal_city))
                                {
                                    similartlist_jobs_bycity_user.Add(additemequal_city);
                                }
                            }

                        }

                    }
                }
            }

            return similartlist_jobs_bycity_user;
        }

        public async Task<List<Job>> GetAllJobs()
        {
            return _jobs;
        }

        public async Task<List<Job>> GetAllJobsIfAsk(string user_sentnce)
        {
            List<Job> EmptyJobs = new List<Job>();
            if (GlobalWords.Contains(user_sentnce))
            {
                return _jobs;
            }

            return EmptyJobs;
        }


        public bool ContainsGlobalWordsPartially(string userSentence)
        {
            string normalized = userSentence.Trim().ToLower();

            foreach (var phrase in GlobalWords)
            {
                if (normalized.Contains(phrase.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public List<Job> GetJobsWithWriteCityNotFoundJobsCategorey(string city)
        {
            string[] Jobs_desc_array = Array.Empty<string>();
            string desc_job = string.Empty;
            foreach (var job in _jobs)
            {
                if (job != null)
                {
                    Jobs_desc_array = job.JobName.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    if (Jobs_desc_array.Contains(city))
                    {
                        if (!Relevantjobs.Contains(job))
                        {
                            Relevantjobs.Add(job);
                        }
                    }

                    else
                    {
                        //צריך לנרמל את המילה ואז לבדוק

                        foreach (var item in Jobs_desc_array)
                        {
                            if (_citiesMap.ContainsKey(item))
                            {
                                var cityfromsentece = _citiesMap.Where(x => x.Key == item).Select(c => c.Value).FirstOrDefault();

                                if(cityfromsentece.Equals(city))
                                {
                                    if (!Relevantjobs.Contains(job))
                                    {
                                        Relevantjobs.Add(job);
                                    }
                                }
                              
                            }
                        }

                    }

                    Jobs_desc_array = Array.Empty<string>();
                }
            }

            return Relevantjobs;
        }

        public List<Job> GetJobs(List<CitybyJob> jobs)
        {
            foreach (var job in _jobs)
            {
                if(jobs.Any(x => x.OrderId == job.OrderId))
                {
                    if(!Relevantjobs.Contains(job))
                    {
                        Relevantjobs.Add(job);
                    }
                }
            }

            return Relevantjobs;
        }

    }
}

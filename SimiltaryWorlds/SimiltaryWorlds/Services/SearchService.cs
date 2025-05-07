using SimiltaryWorlds.Models;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SimiltaryWorlds.Services
{
    public class SearchService : ISearchService
    {
        private readonly Dictionary<string, List<int>> _CategoreyMap;
        private readonly List<Job> Add_jobs;
        private readonly List<string> relevantJobs;
        private readonly IConfiguration iconfig;


   
        private static readonly HashSet<string> StopWords = new()
        {
            "אני", "דרוש", "דרושה", "מחפש", "מחפשת", "עבודה", "משרה", "בתחום", "של", "באזור", "לצוות"
        };

        private static readonly string[] Prefixes = { "ב", "ל", "כ", "מ", "ה" };

        public SearchService(Dictionary<string, List<int>> Categorey_Map, List<string> RelevantJobs)
        {
            _CategoreyMap = Categorey_Map;

            relevantJobs = RelevantJobs
                .Select(j => Regex.Replace(j.ToLower(), "[^\\u0590-\\u05FFa-zA-Z0-9\\s]", "").Trim())
                .ToList();

            Add_jobs = new List<Job>
            {
                new Job { JobId = 29, JobName = "מנהל.ת נכסים", OrderId = 2702 },
                new Job { JobId = 28, JobName = "עובד.ת אחזקה ותפעול", OrderId = 2689 },
                new Job { JobId = 27, JobName = "מתמחה ללשכה המשפטית חולון", OrderId = 2688 },
                new Job { JobId = 25, JobName = "מנהל.ת שיווק לקוחות מגזר פרטי", OrderId = 2675 },
                new Job { JobId = 9, JobName = "מומחה.ית תקשורת ואבטחת מידע לצוות אינטגרציה- חולון", OrderId = 2650 },
                new Job { JobId = 24, JobName = "נציג.ת קידום ותמיכת הזמנות בחולון", OrderId = 2623 },
                new Job { JobId = 13, JobName = "מרכז שרות וחווית לקוח מודיעין עילית", OrderId = 2575 },
                new Job { JobId = 15, JobName = "אנשי.נשות מכירות שטח בעולמות הסיבים", OrderId = 2391 },
                new Job { JobId = 13, JobName = "נציג.ה מרכז תמיכה אסטרטגי מסחרי חולון", OrderId = 2154 },
                new Job { JobId = 14, JobName = "נציג.ה במרכז תמיכה מסחרי עסקי- י\"ם", OrderId = 2084 },
                new Job { JobId = 13, JobName = "יועצים.יועצות לשימור ופרסום דיגיטלי b144 ת\\\"א\"", OrderId = 2023 },
                new Job { JobId = 19, JobName = "עובדי.ות תשתיות", OrderId = 2011 },
                new Job { JobId = 13, JobName = "נציג.ה במרכז שרות וחווית לקוח- ב\"ש", OrderId = 1031 },
                new Job { JobId = 14, JobName = "נציג.ת ייעוץ פרסום דיגיטלי לעסקים מוקד חיפה", OrderId = 1028 },
                new Job { JobId = 21, JobName = "טכנאי.ת שירותי לקוחות", OrderId = 1026 },
                new Job { JobId = 13, JobName = "נציג.ת שירות לעסקים קטנים", OrderId = 1016 },
                new Job { JobId = 14, JobName = "נציג.ת מוקד מכירות ירושלים", OrderId = 1012 },
                new Job { JobId = 13, JobName = "נציג.ה במרכז שרות וחווית לקוח חיפה", OrderId = 1010 },
                new Job { JobId = 13, JobName = "נציג.ה במרכז שירות וחווית לקוח ת\"א", OrderId = 1009 },
                new Job { JobId = 17, JobName = "נציג.ה במרכז תמיכה וחווית לקוח חיפה", OrderId = 1008 },
                new Job { JobId = 17, JobName = "נציג.ה במרכז תמיכה וחווית לקוח באר שבע", OrderId = 1007 },
                new Job { JobId = 17, JobName = "נציג.ה במרכז תמיכה וחוויית לקוח ירושלים", OrderId = 1005 }
            };
        }

        public List<Job> GetRellevatJobsbyCategorey(List<int> ids)
        {
            return Add_jobs.Where(x => ids.Contains(x.JobId)).ToList();
        }

        public bool checkIfCategoreyNotExist(string user_Sentence)
        {
            var sentence = user_Sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var word in sentence)
            {
                if(HasNegativeOne(word))
                {
                    return true;
                }

           }

            for (int i = 0; i < sentence.Length - 1; i++)
            {
                var bigram = $"{sentence[i]} {sentence[i + 1]}";

                if(HasNegativeOne(bigram))
                {
                    return true;

                }
            }


            return false;
        }

        private bool HasNegativeOne(string word)
        {
            if (_CategoreyMap.TryGetValue(word, out var values))
            {
                return values.Contains(-1);
            }
            return false;
        }

   


        //public List<int> AnalyzeSearch(string input)
        //{
        //    var words = NormalizeText(input);
        //    var results = new HashSet<int>();

        //    for (int i = 0; i < words.Count; i++)
        //    {
        //        var word = words[i];

        //        if (_CategoreyMap.TryGetValue(word, out var singleMatch))
        //            foreach (var val in singleMatch)
        //                results.Add(val);

        //        if (i < words.Count - 1)
        //        {
        //            var twoWords = $"{words[i]} {words[i + 1]}";
        //            if (_CategoreyMap.TryGetValue(twoWords, out var match2))
        //                foreach (var val in match2)
        //                    results.Add(val);
        //        }

        //        if (i < words.Count - 2)
        //        {
        //            var threeWords = $"{words[i]} {words[i + 1]} {words[i + 2]}";
        //            if (_CategoreyMap.TryGetValue(threeWords, out var match3))
        //                foreach (var val in match3)
        //                    results.Add(val);
        //        }
        //    }

        //    return results.Distinct().ToList();
        //}


        public List<int> AnalyzeSearch(string input)
        {

            var rawWords = Regex.Replace(input.ToLower(), "[^\\u0590-\\u05FFa-zA-Z0-9\\s]", "")
                                 .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                 .ToList();

            var results = new HashSet<int>();

            for (int i = 0; i < rawWords.Count; i++)
            {
                // ננסה קודם עם מילים מקוריות ללא נירמול
                TryMatchSequence(rawWords, i, results);

                // נרמל את המילה הנוכחית
                string normalizedWord = ShouldNormalize(rawWords[i]) ? RemovePrefix(rawWords[i]) : rawWords[i];

                // נבנה מערך חדש עם המילה הנרמלת
                var normalizedWords = new List<string>(rawWords);
                normalizedWords[i] = normalizedWord;

                // ננסה שוב אחרי נירמול
                TryMatchSequence(normalizedWords, i, results);
            }

            return results.Distinct().ToList();
        }


        private void TryMatchSequence(List<string> words, int index, HashSet<int> results)
        {
        
            // 1. מילה אחת
            if (_CategoreyMap.TryGetValue(words[index], out var match1))
                foreach (var val in match1)
                    results.Add(val);

            // 2. שתי מילים
            if (index + 1 < words.Count)
            {
                var twoWords = $"{words[index]} {words[index + 1]}";
                if (_CategoreyMap.TryGetValue(twoWords, out var match2))
                    foreach (var val in match2)
                        results.Add(val);
            }

            // 3. שלוש מילים
            if (index + 2 < words.Count)
            {
                var threeWords = $"{words[index]} {words[index + 1]} {words[index + 2]}";
                if (_CategoreyMap.TryGetValue(threeWords, out var match3))
                    foreach (var val in match3)
                        results.Add(val);
            }
        }

        private List<string> NormalizeText(string input)
        {
            input = Regex.Replace(input.ToLower(), "[^\\u0590-\\u05FFa-zA-Z0-9\\s]", "");

            var words = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(w => !StopWords.Contains(w))
                .Select(word => ShouldNormalize(word) ? RemovePrefix(word) : word)
                .ToList();

            return words;
        }

        // ✅ שודרגה — לא תנרמל מילים שקיימות במפת הקטגוריות או ברשימת המשרות
        private bool ShouldNormalize(string word)
        {
            return !relevantJobs.Contains(word);
        }

        private string RemovePrefix(string word)
        {
            foreach (var prefix in Prefixes)
            {
                if (word.StartsWith(prefix) && word.Length > 2)
                    return word.Substring(1);
            }
            return word;
        }
    }
}

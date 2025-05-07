using System.Globalization;

namespace drushim.Services
{
    public class SearchCityServer : ISearchCityServer
    {

        Dictionary<string, List<string>> areaToCities = new Dictionary<string, List<string>>
        {
            ["צפון"] = new List<string>
            {
                "חיפה", "נצרת", "עפולה", "קריית שמונה", "נהריה", "צפת", "טבריה", "כרמיאל", "נוף הגליל", "סח'נין", "מעלות-תרשיחא", "יקנעם עילית"
            },
            ["דרום"] = new List<string>
            {
                "אשדוד", "אשקלון", "באר שבע", "אילת", "דימונה", "נתיבות", "שדרות", "מצפה רמון", "ערד", "רהט"
            },
            ["שפלה"] = new List<string>
            {
                "לוד", "רמלה", "מודיעין-מכבים-רעות", "יבנה", "נס ציונה", "רחובות", "בית שמש", "קריית מלאכי", "קריית עקרון"
            },
            ["מרכז"] = new List<string>
            {
             "תל אביב-יפו", "רמת גן", "גבעתיים", "הרצליה", "פתח תקווה", "חולון", "בת ים", "בני ברק", "ראשון לציון", "רמת השרון", "כפר סבא", "רעננה", "הוד השרון"
            }
        };



        private readonly Dictionary<string, string> _citiesMap;
        private static readonly string[] Prefixes = { "ב", "ל", "כ", "מ", "מה", "וה", "וכ" };

        private static readonly string[] CityAbbreviations = { "ת\"א", "ר\"ג", "ראשל\"צ", };

        public SearchCityServer(Dictionary<string, string> citiesMap)
        {
            _citiesMap = citiesMap.ToDictionary(
                kv => kv.Key.Trim().ToLower(),
                kv => kv.Value.Trim());
        }

        public List<string> GetCitiesByArea(string sentenceUser)
        {
            var result = new List<string>();

            string[] prefixes = { "ב", "ל", "מ", "ה", "כ", "ש" };

            var words = sentenceUser.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                if (areaToCities.ContainsKey(word))
                {
                    result = areaToCities[word].ToList();
                    break;
                }

                var cleanWord = word;
                foreach (var prefix in prefixes)
                {
                    if (cleanWord.StartsWith(prefix) && cleanWord.Length > prefix.Length)
                    {
                        cleanWord = cleanWord.Substring(prefix.Length);
                        break;
                    }
                }

                if (areaToCities.ContainsKey(cleanWord))
                {
                    result = areaToCities[cleanWord].ToList();
                    break;
                }
            }

            return result;
        }



        public string DetectCityFromText(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = input.ToLower();
            //input = Regex.Replace(input, "[^\\u0590-\\u05FF\\s]", ""); // שמור רק עברית ורווחים
            var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                var original = words[i].Trim();

                //if (CityAbbreviations.Contains(original))
                //    return original;

                // 🔍 קודם בדיקה על המילה המקורית
                if (_citiesMap.TryGetValue(original, out var cityExact))
                    return NormalizeCityName(cityExact);

                // 🔁 אחר כך נירמול תחיליות
                var w1 = RemovePrefix(original);
                if (_citiesMap.TryGetValue(w1, out var cityNorm))
                    return NormalizeCityName(cityNorm);

                // שתי מילים
                if (i < words.Length - 1)
                {
                    var combinedOriginal = $"{original} {words[i + 1]}".Trim();
                    if (_citiesMap.TryGetValue(combinedOriginal, out var cityPlain2))
                        return NormalizeCityName(cityPlain2);

                    var combinedNorm = $"{RemovePrefix(original)} {words[i + 1]}".Trim();
                    if (_citiesMap.TryGetValue(combinedNorm, out var city2))
                        return NormalizeCityName(city2);

                    // ננסה את המילה השנייה לבדה
                    var second = words[i + 1].Trim();
                    if (_citiesMap.TryGetValue(second, out var citySecond))
                        return NormalizeCityName(citySecond);
                }

                // שלוש מילים
                if (i < words.Length - 2)
                {
                    var combined3 = $"{words[i]} {words[i + 1]} {words[i + 2]}".Trim();
                    if (_citiesMap.TryGetValue(combined3, out var cityPlain3))
                        return NormalizeCityName(cityPlain3);

                    var combined3Norm = $"{RemovePrefix(words[i])} {words[i + 1]} {words[i + 2]}".Trim();
                    if (_citiesMap.TryGetValue(combined3Norm, out var city3))
                        return NormalizeCityName(city3);
                }
            }

            return null;
        }

        private string RemovePrefix(string word)
        {
            foreach (var prefix in Prefixes)
            {
                if (word.StartsWith(prefix) && word.Length > prefix.Length + 1)
                    return word.Substring(prefix.Length);
            }
            return word;
        }

        private string NormalizeCityName(string city)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);
        }
    }
}

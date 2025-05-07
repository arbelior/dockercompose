
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SimiltaryWorlds.Helpers
{
    public class CityHelper
    {
        private readonly Dictionary<string, string> _cityMap;

        public CityHelper(Dictionary<string, string> cityMap)
        {
            _cityMap = cityMap;
        }

        public string? GetCityFromSentence(string sentence)
        {
            var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var word in words)
            {
                var normalized = NormalizeWord(word);
                if (_cityMap.TryGetValue(normalized, out var city))
                    return city;
            }

            return null;
        }

        public string NormalizeWord(string word)
        {
            var normalized = word
                .Replace("\"", "")
                .Replace("'", "")
                .Replace("־", "-")
                .ToLower();

            normalized = Regex.Replace(normalized, @"[^א-תa-zA-Z0-9\-]", "");

            if (normalized.Length > 3 &&
                (normalized.StartsWith("ב") ||
                 normalized.StartsWith("ל") ||
                 normalized.StartsWith("כ") ||
                 normalized.StartsWith("מ")))
            {
                normalized = normalized.Substring(1);
            }

            return normalized.Trim();
        }

        public string NormalizeSentence(string sentence)
        {
            var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var normalizedWords = words.Select(NormalizeWord).ToList();
            return string.Join(" ", normalizedWords);
        }

        public List<string> GetAllCitiesFromSentence(string sentence)
        {
            var foundCities = new List<string>();
            var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var word in words)
            {
                var normalized = NormalizeWord(word);
                if (_cityMap.TryGetValue(normalized, out var city) && !foundCities.Contains(city))
                {
                    foundCities.Add(city);
                }
            }

            return foundCities;
        }
    }
}

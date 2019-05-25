using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Namrly.Services 
{
    public class WordnikClient : IRandomWordService
    {
        private readonly string _wordnikApiKey;
        private readonly string _wordnikBaseAddress = "https://api.wordnik.com";
        private readonly string _wordnikApiVersion  = "v4";
        private readonly string _WordsEntityPath = "words.json";
        private readonly string _WordEntityPath = "word.json";
        private readonly int _minWordLength = 3;
        private readonly int _maxWordLength = 8;

        private string WordsPath => $"{_wordnikBaseAddress}/{_wordnikApiVersion}/{_WordsEntityPath}";
        private string WordPath => $"{_wordnikBaseAddress}/{_wordnikApiVersion}/{_WordEntityPath}";

        
        public WordnikClient() {
            this._wordnikApiKey = this.GetApiKey();
        }

        public async Task<IEnumerable<string>> GetRandomWords(int numWords) 
        {
            using (var client = new HttpClient())
            {
                var request = this.WordsPath 
                + "/randomWords?hasDictionaryDef=true&limit=" + numWords 
                + "&api_key=" + this._wordnikApiKey
                + "&minLength=" + _minWordLength
                + "&maxLength=" + _maxWordLength;
                var response = await client.GetAsync(request);
                var jsonString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<WordnikSingleResponse>>(jsonString).Select(word => word.word).ToList();
            }
            
        }

        public async Task<IEnumerable<string>> GetSynonyms(string baseWord) 
        {
            var synonyms = new List<string>();
            if (baseWord == null) return synonyms;

            using (var client = new HttpClient())
            {
                var request =
                    this.WordPath + "/" + baseWord 
                    + "/relatedWords?useCanonical=false&relationshipTypes=synonym&limitPerRelationshipType=" + 99 
                    + "&api_key="+ this._wordnikApiKey
                    + "&minLength=" + _minWordLength
                    + "&maxLength=" + _maxWordLength;
                var rawResponse = await client.GetAsync(request);
                var jsonString = await rawResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<WordnikResponse[]>(jsonString);

                if (response != null && response.Length > 0 && response[0].words.Length > 0) {
                    synonyms = response[0].words.ToList();
                }
                
                return synonyms;
            }
        }

        // TODO: Load the crednentials from a config of some sort. I will want to use a Heroku env variable, etc.
        private string GetApiKey() {
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "wordNikCred.txt");
            return System.IO.File.ReadAllText(path);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Namrly.WordnikApi;
using Newtonsoft.Json;

public class WordnikClient {
    private readonly string _wordnikApiKey;
    private readonly string _wordnikBaseAddress = "https://api.wordnik.com";
    private readonly string _wordnikApiVersion  = "v4";
    private readonly string _entityPath = "words.json";

    private string WordnikApiPath => $"{_wordnikBaseAddress}/{_wordnikApiVersion}/{_entityPath}";

    
    public WordnikClient() {
        this._wordnikApiKey = this.GetApiKey();
    }

    public async Task<List<string>> GetRandomWords(int numWords = 1) {
        using (var client = new HttpClient())
        {
            var request = this.WordnikApiPath + "/randomWords?hasDictionaryDef=true&limit = " + numWords + "&api_key="+ this._wordnikApiKey;
            var response = await client.GetAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<WordnikSingleResponse>>(jsonString).Select(word => word.word).ToList();
        }
        
    }

    public async Task<List<string>> GetSynonyms(string baseWord, int numWords = 0) {
        using (var client = new HttpClient())
        {
            var request =
                this.WordnikApiPath + "/" + baseWord 
                + "/relatedWords?useCanonical=false&relationshipTypes=synonym&limitPerRelationshipType=" + numWords + " api_key="+ this._wordnikApiKey;
            var response = await client.GetAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<WordnikSingleResponse>>(jsonString).Select(word => word.word).ToList();
        }
    }

    // TODO: Load the crednentials from a config of some sort. I will want to use a Heroku env variable, etc.
    private string GetApiKey() {
        var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "wordNikCred.txt");
        return System.IO.File.ReadAllText(path);
    }
}
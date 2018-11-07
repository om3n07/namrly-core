using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Namrly.WordnikApi;
using Newtonsoft.Json;

public class RandomWordProxy {
    private readonly string _wordnikApiKey;

    public RandomWordProxy() {
        this._wordnikApiKey = this.GetApiKey();
    }

    public async Task<string> GetRandomWord(int len = 0) {
         using (var client = new HttpClient())
            {
                // var request = "https://api.wordnik.com/v4/words.json/randomWord?hasDictionaryDef=true";
                var request =
                    "https://api.wordnik.com/v4/words.json/randomWord?hasDictionaryDef=true&api_key="+ this._wordnikApiKey;
                var response = await client.GetAsync(request);
                var jsonString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<WordnikSingleResponse>(jsonString).word;
            }
    }

    private string GetApiKey() {
        // var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "wordNikCred.txt");
        
        // return System.IO.File.ReadAllText(path);

        return "";
    }
}
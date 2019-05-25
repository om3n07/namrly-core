using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Namrly.Services
{

    public class NamrlyService : INamrlyService
    {
        private IRandomWordService _randomWordService = null;
        private static readonly Random R = new Random();
        private readonly string[] _vowels = { "a", "e", "i", "o", "u" };

        protected IRandomWordService RandomWordService => _randomWordService;

        public NamrlyService(IRandomWordService randomWordService)
        {
            this._randomWordService = randomWordService;
        }

        public async Task<string> GetRandomName(bool includeAdditionalSuffixes = false)
        {
            var name = string.Empty;
            var results = (await this.GetRandomNames(1, includeAdditionalSuffixes)).ToList();

            // Get a random name from the list of returned results.
            if (results != null && results.Count > 0)
            {
                name = results[0];
            }

            return name;
        }

        public async Task<string> GetRandomName(string baseWord, bool includeAdditionalSuffixes = false)
        {
            var name = string.Empty;
            var results = (await this.GetRandomNames(baseWord, 1, includeAdditionalSuffixes)).ToList();

            // Get a random name from the list of returned results.
            if (results != null && results.Count > 0)
            {
                name = results[0];
            }

            return name;
        }

        public async Task<ICollection<string>> GetRandomNames(int numResults = 1)
        {
            return await this.GetRandomNames(null, numResults);
        }

        public async Task<ICollection<string>> GetRandomNames(int numResults = 1, bool includeAdditionalSuffixes = false)
        {
            var results = new List<string>();
            var words = await this.RandomWordService.GetRandomWords(numResults);

            if (words != null && words.ToList().Count > 0)
            {
                foreach (var word in words)
                {
                    var newWord = word.Clone().ToString();

                    if (this.ShouldDropVowel())
                    {
                        this.DropVowel(ref newWord);
                    }

                    newWord += GetRandomSuffix(includeAdditionalSuffixes);

                    results.Add(newWord);
                }
            }

            return results;
        }

        public async Task<ICollection<string>> GetRandomNames(string baseWord, int numResults = 1, bool includeAdditionalSuffixes = false)
        {
            var results = new List<string>();

            // gets the list of synonyms, then shuffles
            var synonyms = (await this.RandomWordService.GetSynonyms(baseWord));
            if (synonyms != null && synonyms.Count() > 0)
            {
                synonyms.ToList()
                .OrderBy(a => Guid.NewGuid()).ToList();

                foreach (var synonym in synonyms)
                {
                    var newWord = synonym.Clone().ToString();

                    if (this.ShouldDropVowel()) this.DropVowel(ref newWord);
                    newWord += GetRandomSuffix(includeAdditionalSuffixes);

                    results.Add(newWord);

                    // don't want to return more than the requested number of results.
                    if (--numResults == 0) break;
                }
            }

            return results;
        }

        private static string GetRandomSuffix(bool includeAdditionalSuffixes)
        {
            if (includeAdditionalSuffixes && R.Next(2) == 0)
            {
                return ((AdditionalSuffixes)R.Next(0, Enum.GetNames(typeof(AdditionalSuffixes)).Length)).ToString();
            }

            return ((Suffixes)R.Next(0, Enum.GetNames(typeof(Suffixes)).Length)).ToString();
        }

        private bool ShouldDropVowel()
        {
            // 25%
            var s = R.Next(0, 3);
            return s == 1;
        }

        // Drops either the last letter or second to last letter if one of them is a vowel
        private bool DropVowel(ref string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            var wasSuccessful = false;

            foreach (var v in this._vowels)
            {
                if (word.EndsWith(v))
                {
                    word = word.Substring(0, word.Length - 1);
                    break;
                }

                if (word[word.Length - 2].ToString() == v)
                {
                    var e = word[word.Length - 1];
                    word = word.Substring(0, word.Length - 2);
                    word += e;
                    break;
                }
            }

            return wasSuccessful;
        }

        private enum Suffixes
        {
            ly,
            r,
            rly,
            bits,
            ify,
        }

        private enum AdditionalSuffixes
        {
            bon,
            a
        }
    }
}
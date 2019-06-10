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

        public async Task<ICollection<string>> GetRandomStartupNames(int numResults = 1)
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

                    newWord += GetRandomSuffix();

                    results.Add(newWord);
                }
            }

            return results;
        }

        public async Task<ICollection<string>> GetRelatedStartupNames(string baseWord, int maxNumResults = 1)
        {
            var results = new List<string>();

            // gets the list of synonyms, then shuffles
            var synonyms = (await this.RandomWordService.GetSynonyms(baseWord));
            if (synonyms != null && synonyms.Count() > 0)
            {
                synonyms = synonyms
                    .ToList()
                    .OrderBy(a => Guid.NewGuid())
                    .ToList();

                foreach (var synonym in synonyms)
                {
                    var newWord = synonym.Clone().ToString();

                    if (this.ShouldDropVowel()) this.DropVowel(ref newWord);
                    newWord += GetRandomSuffix();

                    results.Add(newWord);

                    // don't want to return more than the requested number of results.
                    if (--maxNumResults == 0) break;
                }
            }

            return results;
        }

        private static string GetRandomSuffix()
        {
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
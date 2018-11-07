using System;
using System.Threading.Tasks;

public class NamrlyService : INamrlyService
{
    private RandomWordProxy _randomWordProxy = null;
    private static readonly Random R = new Random();
    private readonly string[] _vowels = { "a", "e", "i", "o", "u" };

    private bool ShouldDropVowel
        {
            get
            {
                // 25%
                var s = R.Next(0, 3);
                return s == 1;
            }
        }

    protected RandomWordProxy RandomWordProxy => _randomWordProxy != null ? _randomWordProxy : _randomWordProxy = new RandomWordProxy();
    
    public NamrlyService()
    {
        
    }

    public async Task<string> GetRandomName(bool includeAdditionalSuffixes = false)
    {
        var result = await this.RandomWordProxy.GetRandomWord(R.Next(0, 5));
            if (this.ShouldDropVowel) this.DropVowel(ref result);
            result += GetRandomSuffix(includeAdditionalSuffixes);
            return (result);
    }

    public async Task<string> GetRandomName(string baseName, bool includeAdditionalSuffixes = false)
    {
        throw new System.NotImplementedException();
    }

    private static string GetRandomSuffix(bool includeAdditionalSuffixes)
    {
        if (includeAdditionalSuffixes && R.Next(2) == 0) return ((AdditionalSuffixes)R.Next(0, Enum.GetNames(typeof(AdditionalSuffixes)).Length)).ToString();
        return ((Suffixes)R.Next(0, Enum.GetNames(typeof(Suffixes)).Length)).ToString();
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
}

 enum Suffixes
    {
        ly,
        r,
        rly,
        bits,
        ify,
    }

    enum AdditionalSuffixes
    {
       bon
    }
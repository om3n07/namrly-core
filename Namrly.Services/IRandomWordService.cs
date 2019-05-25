using System.Collections.Generic;
using System.Threading.Tasks;

namespace Namrly.Services
{
    public interface IRandomWordService
    {
        Task<IEnumerable<string>> GetRandomWords(int numWords);
        Task<IEnumerable<string>> GetSynonyms(string baseWord);
    }
}
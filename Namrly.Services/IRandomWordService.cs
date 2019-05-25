using System.Collections.Generic;
using System.Threading.Tasks;

namespace Namrly.Services
{
    public interface IRandomWordService
    {
        Task<ICollection<string>> GetRandomWords(int numWords);
        Task<ICollection<string>> GetSynonyms(string baseWord);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Namrly.Services
{
    public interface INamrlyService
    {
        Task<ICollection<string>> GetRandomStartupNames(int numResults = 1);
        Task<ICollection<string>> GetRelatedStartupNames(string baseWord, int numResults = 1);
    }
}
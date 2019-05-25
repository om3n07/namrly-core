using System.Collections.Generic;
using System.Threading.Tasks;

namespace Namrly.Services
{
    public interface INamrlyService
    {
        Task<string> GetRandomName(bool includeAdditionalSuffixes = false);
        Task<string> GetRandomName(string baseName, bool includeAdditionalSuffixes = false);
        Task<ICollection<string>> GetRandomNames(int numResults = 0, bool includeAdditionalSuffixes = false);
        Task<ICollection<string>> GetRandomNames(string baseName, int numResults = 0, bool includeAdditionalSuffixes = false);
    }
}
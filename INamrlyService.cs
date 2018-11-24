using System.Collections.Generic;
using System.Threading.Tasks;

public interface INamrlyService {
    Task<string> GetRandomName(string baseName = null, bool includeAdditionalSuffixes = false);    
    Task<IEnumerable<string>> GetRandomNames(string baseName = null, int numResults = 0, bool includeAdditionalSuffixes = false);
}
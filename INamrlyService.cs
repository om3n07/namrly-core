using System.Collections.Generic;
using System.Threading.Tasks;

public interface INamrlyService {
    Task<IEnumerable<string>> GetRandomNames(bool includeAdditionalSuffixes = false, int numResults = 0);
    
    Task<IEnumerable<string>> GetRandomNames(string baseName, bool includeAdditionalSuffixes = false, int numResults = 0);
}
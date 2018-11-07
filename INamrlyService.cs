using System.Threading.Tasks;

public interface INamrlyService {
    Task<string> GetRandomName(bool includeAdditionalSuffixes = false);
    
    Task<string> GetRandomName(string baseName, bool includeAdditionalSuffixes = false);
}
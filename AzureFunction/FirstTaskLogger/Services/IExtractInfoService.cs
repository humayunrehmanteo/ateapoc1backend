using System.Threading.Tasks;

namespace FirstTaskLogger.Services
{
    public interface IExtractInfoService
    {
        Task<bool> GetStoreLogDataAsync();
    }
}

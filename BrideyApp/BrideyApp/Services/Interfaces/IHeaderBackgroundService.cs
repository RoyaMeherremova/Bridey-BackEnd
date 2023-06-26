using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IHeaderBackgroundService
    {
        Task<List<HeaderBackground>> GetHeaderBackgroundsAsync();
        Task<HeaderBackground> GetHeaderBackgroundByIdAsync(int? id);
        Dictionary<string, string> GetHeaderBackgroundDatas();

    }
}

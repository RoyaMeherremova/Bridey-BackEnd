using BrideyApp.Models;
using NuGet.Configuration;

namespace BrideyApp.Services.Interfaces
{
    public interface ISettingService
    {
        Task<List<Setting>> GetSettingsAsync();
        Task<Setting> GetSettingByIdAsync(int? id);
    }
}

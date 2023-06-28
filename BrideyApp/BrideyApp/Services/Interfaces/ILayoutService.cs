using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ILayoutService
    {
        Dictionary<string, string> GetSettingDatas();
        Task<List<Social>> GetAll();
        Dictionary<string, string> GetSectionBackgroundImages();

    }
}

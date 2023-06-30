using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IColorService
    {
        Task<List<Color>> GetAll();
        Task<Color> GetColorById(int? id);

    }
}

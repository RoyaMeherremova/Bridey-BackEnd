using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAll();
        Task<Slider> GetSliderById(int? id);
    }
}

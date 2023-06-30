using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();
        Task<Brand> GetBrandById(int? id);
    }
}

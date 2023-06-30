using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetCategoryById(int? id);
    }
}

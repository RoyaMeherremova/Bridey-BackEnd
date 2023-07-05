using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAll();
        Task<int> GetCountAsync();
        Task<List<Blog>> GetPaginatedDatas(int page, int take);
        Task<Blog> GetById(int? id);
        List<Blog> GetRelatedBlogs();

    }
}

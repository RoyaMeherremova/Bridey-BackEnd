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
        Task<List<BlogComment>> GetComments();
        Task<BlogComment> GetCommentById(int? id);
        Task<BlogComment> GetCommentByIdWithBlog(int? id);

    }
}

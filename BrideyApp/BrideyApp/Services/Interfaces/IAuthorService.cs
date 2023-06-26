using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAll();
        Task<Author> GetById(int? id);
    }
}

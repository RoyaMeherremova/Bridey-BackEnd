using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface IPositionService
    {
        Task<List<Position>> GetAll();
        Task<Position> GetPositionById(int? id);
    }
}

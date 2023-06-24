using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> GetAll();
        Task<Team> GetTeamById(int? id);
    }
}

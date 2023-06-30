using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAll();
        Task<Sale> GetSaleById(int? id);
    }
}

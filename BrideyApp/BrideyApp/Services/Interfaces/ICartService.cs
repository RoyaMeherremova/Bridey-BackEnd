using BrideyApp.Models;
using BrideyApp.ViewModels;

namespace BrideyApp.Services.Interfaces
{
    public interface ICartService
    {
        List<CartVM> GetDatasFromCookie();
        void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct);
        void DeleteData(int? id);
        Task<Cart> GetByUserIdAsync(string userId);
        Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId);
    }
}

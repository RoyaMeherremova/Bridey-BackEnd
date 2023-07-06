using BrideyApp.Models;
using BrideyApp.ViewModels.Wishlist;

namespace BrideyApp.Services.Interfaces
{
    public interface IWishlistService
    {
        List<WishlistVM> GetDatasFromCookie();
        void SetDatasToCookie(List<WishlistVM> wishlists, Product dbProduct, WishlistVM existProduct);
        void DeleteData(int? id);
        Task<Wishlist> GetByUserIdAsync(string userId);
        Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? wishlistId);
    }
}

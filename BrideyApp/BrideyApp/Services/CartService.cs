using BrideyApp.Data;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Cart;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BrideyApp.Services
{
    public class CartService:ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public CartService(IHttpContextAccessor httpContextAccessor,
                            AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public List<CartVM> GetDatasFromCookie()
        {
            List<CartVM> carts;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                carts = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                carts = new List<CartVM>();
            }
            return carts;
        }

        public void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct)
        {
            if (existProduct == null)
            {
                carts.Add(new CartVM
                {
                    ProductId = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _httpContextAccessor.HttpContext.Response.Cookies
                .Append("basket", JsonConvert.SerializeObject(carts));
        }

        public void DeleteData(int? id)
        {
            var baskets = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            var deletedProduct = baskets.FirstOrDefault(b => b.ProductId == id);
            baskets.Remove(deletedProduct);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));
        }

        public async Task<Cart> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.AppUserId == userId);
        }

        public async Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId)
        {
            return await _context.CartProducts.Where(c => c.CartId == cartId).ToListAsync();
        }
    }
}

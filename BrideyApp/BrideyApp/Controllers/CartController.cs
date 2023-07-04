using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BrideyApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<CartVM> carts = _cartService.GetDatasFromCookie();
            List<CartDetailVM> cartDetailVMs = new();

            foreach (var item in carts)
            {
                Product dbProduct = await _productService.GetById((int)item.ProductId);

                cartDetailVMs.Add(new CartDetailVM()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    Image = dbProduct.Images.FirstOrDefault(i => i.IsMain).Image,
                    Count = item.Count,
                    Total = dbProduct.Price * item.Count
                });
            }
            return View(cartDetailVMs);
        }

        [HttpPost]
        public IActionResult DeleteDataFromBasket(int? id)
        {
            if (id is null) return BadRequest();

            _cartService.DeleteData((int)id);
            List<CartVM> baskets = _cartService.GetDatasFromCookie();

            return Ok();
        }
        [HttpPost]
        public IActionResult IncrementProductCount(int? id)
        {
            if (id is null) return BadRequest();
            var baskets = JsonConvert.DeserializeObject<List<CartVM>>(Request.Cookies["basket"]);
            var count = baskets.FirstOrDefault(b => b.ProductId == id).Count++;

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));

            return Ok(count);
        }

        [HttpPost]
        public IActionResult DecrementProductCount(int? id)
        {
            if (id is null) return BadRequest();
            var baskets = JsonConvert.DeserializeObject<List<CartVM>>(Request.Cookies["basket"]);
            var product = (baskets.FirstOrDefault(b => b.ProductId == id));
            if (product.Count == 1)
            {
                return Ok();
            }
            var count = product.Count--;
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));

            return Ok(count);
        }
    }
}
    
using BrideyApp.Data;
using BrideyApp.Helpers.Enums;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Account;
using BrideyApp.ViewModels.Cart;
using BrideyApp.ViewModels.Wishlist;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BrideyApp.Controllers
{
    //SuperAdmin - Cavid_Bashirov  email: royaam@code.edu.az / parol: Cavid123_
    //Admin - Roya_Meherremova  email: ggigarli@gmail.com / parol: Cavid123_
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ICartService _cartService;
        private readonly IWishlistService _wishlistService;




        public AccountController(AppDbContext context,
                                 UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 ICartService cartService,
                                 IEmailService emailService,
                                 IWishlistService wishlistService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _cartService = cartService;
            _emailService = emailService;
            _context = context;
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AppUser newUser = new()
                {
                    UserName = string.Join("_", model.FirstName, model.LastName),
                    Email = model.Email,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    TempData["errors"] = model.ErrorMessages;
                    return View(model);
                }

                await _userManager.AddToRoleAsync(newUser, Roles.Member.ToString());

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme, Request.Host.ToString());

                string subject = "Register confirmation";

                string html = string.Empty;

                using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
                {
                    html = reader.ReadToEnd();
                }

                html = html.Replace("{{link}}", link);
                html = html.Replace("{{headerText}}", "Welcome");

                _emailService.Send(newUser.Email, subject, html);

                return RedirectToAction(nameof(VerifyEmail));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View(model);
            }
        }





        //public async Task CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //}


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            List<CartVM> cartVMs = new();
            List<WishlistVM> wishlistVMs = new();
            Cart dbCart = await _cartService.GetByUserIdAsync(userId);
            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(userId);

            if (dbCart is not null)
            {
                List<CartProduct> cartProducts = await _cartService.GetAllByCartIdAsync(dbCart.Id);
                foreach (var cartProduct in cartProducts)
                {
                    cartVMs.Add(new CartVM
                    {
                        ProductId = cartProduct.ProductId,
                        Count = cartProduct.Count
                    });
                }

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
            }

            if (dbWishlist is not null)
            {
                List<WishlistProduct> wishlistProducts = await _wishlistService.GetAllByWishlistIdAsync(dbWishlist.Id);
                foreach (var wishlistProduct in wishlistProducts)
                {
                    wishlistVMs.Add(new WishlistVM
                    {
                        ProductId = wishlistProduct.ProductId,
                    });
                }

                Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistVMs));
            }


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
            Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistVMs));

            return RedirectToAction("Index", "Home");
        }



        public IActionResult VerifyEmail()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                AppUser user = await _userManager.FindByEmailAsync(model.EmailOrUsername);

                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(model.EmailOrUsername);
                }
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    return View(model);
                }
                var res = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false);

                if (!res.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Email or password is wrong");
                    return View(model);
                }

                List<CartVM> cartVMs = new();
                List<WishlistVM> wishlistVMs = new();


                Cart dbCart = await _cartService.GetByUserIdAsync(user.Id);
                Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(user.Id);


                if (dbCart is not null)
                {
                    List<CartProduct> cartProducts = await _cartService.GetAllByCartIdAsync(dbCart.Id);
                    foreach (var cartProduct in cartProducts)
                    {
                        cartVMs.Add(new CartVM
                        {
                            ProductId = cartProduct.ProductId,
                            Count = cartProduct.Count
                        });
                    }

                    Response.Cookies.Append("basket", JsonConvert.SerializeObject(cartVMs));
                }
                if (dbWishlist is not null)
                {
                    List<WishlistProduct> wishlistProducts = await _wishlistService.GetAllByWishlistIdAsync(dbCart.Id);
                    foreach (var wishlistProduct in wishlistProducts)
                    {
                        wishlistVMs.Add(new WishlistVM
                        {
                            ProductId = wishlistProduct.ProductId,
                        });
                    }

                    Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistVMs));
                }


                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string userId)
        {
            await _signInManager.SignOutAsync();

            List<CartVM> carts = _cartService.GetDatasFromCookie();
            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();

            Cart dbCart = await _cartService.GetByUserIdAsync(userId);
            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(userId);
            if (carts.Count != null)
            {

                if (dbCart == null)
                {
                    dbCart = new()
                    {
                        AppUserId = userId,
                        CartProducts = new List<CartProduct>()
                    };
                    foreach (var cart in carts)
                    {
                        dbCart.CartProducts.Add(new CartProduct()
                        {
                            ProductId = cart.ProductId,
                            CartId = dbCart.Id,
                            Count = cart.Count
                        });
                    }
                    await _context.Carts.AddAsync(dbCart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<CartProduct> cartProducts = new List<CartProduct>();
                    foreach (var cart in carts)
                    {
                        cartProducts.Add(new CartProduct()
                        {
                            ProductId = cart.ProductId,
                            CartId = dbCart.Id,
                            Count = cart.Count
                        });
                    }
                    dbCart.CartProducts = cartProducts;

                    //await _context.Carts.AddAsync(dbCart);
                    _context.SaveChanges();

                }
                Response.Cookies.Delete("basket");
            }
            else
            {
                _context.Carts.Remove(dbCart);
            }


            if (wishlists.Count != null)
            {
               
                if (dbWishlist == null)
                {
                    dbWishlist = new()
                    {
                        AppUserId = userId,
                        WishlistProducts = new List<WishlistProduct>()
                    };
                    foreach (var wishlist in wishlists)
                    {
                        dbWishlist.WishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = wishlist.ProductId,
                            WishlistId = dbWishlist.Id,
                        });
                    }
                    await _context.Wishlists.AddAsync(dbWishlist);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<WishlistProduct> wishlistProducts = new List<WishlistProduct>();
                    foreach (var wishlist in wishlists)
                    {
                        wishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = wishlist.ProductId,
                            WishlistId = dbWishlist.Id,
                        });
                    }
                    dbWishlist.WishlistProducts = wishlistProducts;
                    //await _context.Wishlists.AddAsync(dbWishlist);
                    _context.SaveChanges();

                }
                Response.Cookies.Delete("wishlist");
            }
            else
            {
                _context.Wishlists.Remove(dbWishlist);

            }


            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            if (!ModelState.IsValid) return View();

            AppUser existUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (existUser is null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);

            string link = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme, Request.Host.ToString());


            string subject = "Verify password reset email";

            string html = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);
            html = html.Replace("{{headerText}}", "Welcome");

            _emailService.Send(existUser.Email, subject, html);
            return RedirectToAction(nameof(VerifyEmail));
        }


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            return View(new ResetPasswordVM { Token = token, UserId = userId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);
            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);
            if (existUser == null) return NotFound();
            if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
            {
                ModelState.AddModelError("", "New password cant be same with old password");
                return View(resetPassword);
            }
            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);
            return RedirectToAction(nameof(Login));
        }


    }
}

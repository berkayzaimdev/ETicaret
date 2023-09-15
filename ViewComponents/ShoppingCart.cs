using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ETicaret.ViewComponents
{
    [ViewComponent]
    public class ShoppingCart : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;

        public ShoppingCart(AppDbContext context,SignInManager<AppUser> signInManager,ICartItemRepository cartItemRepository ,IProductRepository productRepository)
        {
            _context = context;
            _signInManager = signInManager;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            List<CartItem> cartItems;
            if (_signInManager.IsSignedIn((System.Security.Claims.ClaimsPrincipal)User))
            {
                cartItems = _cartItemRepository.GetByCartId(Int32.Parse(User.Identity.GetUserId())).ToList();
            }
            else 
            {
                if(HttpContext.Session.GetString("cart")==null)
                {
                    HttpContext.Session.SetString("cart", JsonSerializer.Serialize(new List<CartItem>()));
                }   
                cartItems = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("cart"));
            }
            foreach (CartItem ci in cartItems)
            {
                ci.Product = _productRepository.GetById(ci.ProductId);
            }
            return View("Default", cartItems);
        }
    }
}

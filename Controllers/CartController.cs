using ETicaret.Models;
using ETicaret.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ETicaret.Controllers
{
    public class CartController : BaseController
    {
        protected readonly ICartRepository _cartRepository;
        protected readonly ICartItemRepository _cartItemRepository;
        protected readonly IProductRepository _productRepository;
        protected readonly ICouponRepository _couponRepository;
        private readonly SignInManager<AppUser> _signInManager;

        public CartController(ICartRepository cartRepository, ICartItemRepository cartItemRepository,IProductRepository productRepository, ICouponRepository couponRepository, SignInManager<AppUser> signInManager)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _couponRepository = couponRepository;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            List<CartItem> cartItems;
            if (_signInManager.IsSignedIn(User))
            {
                cartItems = _cartItemRepository.GetByCartId(Int32.Parse(User.Identity.GetUserId())).ToList();
            }
            else
            {
                if (HttpContext.Session.GetString("cart") == null)
                {
                    HttpContext.Session.SetString("cart", JsonSerializer.Serialize(new List<CartItem>()));
                }
                cartItems = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("cart"));
            }
            foreach (CartItem ci in cartItems)
            {
                ci.Product = _productRepository.GetById(ci.ProductId);
            }
            if (HttpContext.Session.GetInt32("coupon") == null) HttpContext.Session.SetInt32("coupon", 0);
            return View(cartItems);
        }

        public IActionResult Add(int productId) 
        {
            int? userId;
            if(_signInManager.IsSignedIn(User))
            {
                userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            else
            {
                userId = null;
            }

            CartItem newCartItem = new()
            {
                CartId = userId!=null?_cartRepository.GetByUserId((int)userId).Id:null,
                ProductId = productId,
                Quantity = 1
            };

            if(newCartItem.CartId == null)
            {
                List<CartItem> cartitems = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("cart"));
                if(cartitems.SingleOrDefault(ci => ci.ProductId == newCartItem.ProductId && ci.Id == newCartItem.Id) == null)
                    cartitems.Add(newCartItem);
                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cartitems));
            }

            else
            {
                CartItem p = _cartItemRepository.GetAll().SingleOrDefault(ci => ci.ProductId == newCartItem.ProductId && ci.CartId == newCartItem.CartId);
                if (p==null)
                    _cartItemRepository.Add(newCartItem);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartItemId,int productId)
        {
            if (_signInManager.IsSignedIn(User))
            {
                _cartItemRepository.Remove(_cartItemRepository.GetById(cartItemId));
            }
            else
            {
                List<CartItem> cartItems = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("cart"));
                cartItems.Remove((CartItem)cartItems.SingleOrDefault(ci => ci.ProductId == productId));
                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cartItems));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult UpdateQuantity(int cartItemId,int productId, int newQuantity)
        {
            if (cartItemId == 0)
            {
                List<CartItem> cartitems = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("cart"));
                CartItem ci = (CartItem) cartitems.SingleOrDefault(ci => ci.ProductId == productId);
                ci.Quantity = newQuantity;
                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(cartitems));
            }

            else
            {
                CartItem ci = _cartItemRepository.GetById(cartItemId);
                ci.Quantity = newQuantity;
                _cartItemRepository.Update(ci);
                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(_cartItemRepository.GetAll().ToList()));
            }
            return Json(new {});
        }
    }
}

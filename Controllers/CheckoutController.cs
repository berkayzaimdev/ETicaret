using ETicaret.Models;
using ETicaret.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ETicaret.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class CheckoutController : BaseController
    {
        protected readonly ICartRepository _cartRepository;
        protected readonly ICartItemRepository _cartItemRepository;
        protected readonly IProductRepository _productRepository;
        protected readonly ICouponRepository _couponRepository;
        protected readonly IAddressRepository _addressRepository;
        protected readonly IOrderRepository _orderRepository;
        protected readonly IOrderItemRepository _orderItemRepository;
        protected readonly IShipperRepository _shipperRepository;
        protected readonly ICardRepository _cardRepository;
        private readonly SignInManager<AppUser> _signInManager;

        public CheckoutController(ICartRepository cartRepository, ICartItemRepository cartItemRepository, IProductRepository productRepository, ICouponRepository couponRepository, IAddressRepository addressRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IShipperRepository shipperRepository, ICardRepository cardRepository, SignInManager<AppUser> signInManager)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _couponRepository = couponRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _shipperRepository = shipperRepository;
            _cardRepository = cardRepository;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            int initializeCoupon = (int)(HttpContext.Session.GetInt32("coupon")??0);
            HttpContext.Session.SetInt32("coupon", initializeCoupon);
            ViewBag.CouponPercentage = TempData["CouponPercentage"] ?? 0;
            int currentUserId = Int32.Parse(User.Identity.GetUserId());
            CheckoutViewModel cvm = new();
            LoadModel(cvm,currentUserId);
            return View(cvm);
        }

        [HttpPost]
        public IActionResult Index(CheckoutViewModel cvm)
        {
            int currentUserId = Int32.Parse(User.Identity.GetUserId());
            LoadModel(cvm,currentUserId);
            ModelState.Remove(nameof(Address));
            if (ModelState.IsValid)
            {         
                if (cvm.SelectedAddress == 0)
                {
                    cvm.Address.UserId = currentUserId;
                    _addressRepository.Add(cvm.Address);
                }
                else
                    cvm.Address = _addressRepository.GetById(cvm.SelectedAddress);
                _cardRepository.Add(cvm.Card);
                Order order = new()
                {
                    Amount = cvm.Amount,
                    UserId = currentUserId,
                    ShipperId = cvm.ShipperId,
                    CardId = cvm.Card.Id,
                    AddressId = cvm.Address.Id
                };
                _orderRepository.Add(order);

                OrderItem orderItem;
                foreach (CartItem cartItem in cvm.CartItems)
                {
                    orderItem = new()
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity
                    };
                    Product product = _productRepository.GetById(orderItem.ProductId);
                    product.Stock -= orderItem.Quantity;
                    _productRepository.Update(product);
                    _orderItemRepository.Add(orderItem);
                    _cartItemRepository.Remove(cartItem);
                }
                HttpContext.Session.SetInt32("coupon", 0);
                return RedirectToAction("Index", "Home");
            }
            return View(cvm);
        }

        public void LoadModel(CheckoutViewModel cvm,int currentUserId)
        {
            List<CartItem> cartItems = _cartItemRepository.GetByCartId(currentUserId).ToList();
            foreach (CartItem ci in cartItems)
            {
                ci.Product = _productRepository.GetById(ci.ProductId);
            }
            cvm.CartItems = cartItems;
            cvm.Shippers = _shipperRepository.GetAll();
            cvm.Addresses = _addressRepository.GetAll().Where(a => a.UserId == currentUserId).ToList();
        }


        [HttpPost]
        public IActionResult ApplyCoupon(string code)
        {
            Coupon coupon = _couponRepository.GetByCode(code.ToUpper());
            if(coupon!=null)
                HttpContext.Session.SetInt32("coupon", coupon.Percentage);
            return Json(new { });
        }

        [HttpPost]
        public IActionResult FindAddressById(int addressId)
        {
            Address address = _addressRepository.GetById(addressId);
            return Json(JsonSerializer.Serialize(address));
        }
    }
}
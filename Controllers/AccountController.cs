using ETicaret.Models;
using ETicaret.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ETicaret.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IShipperRepository _shipperRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(ICartRepository cartRepository, ICartItemRepository cartItemRepository, IOrderRepository orderRepository, IAddressRepository addressRepository, IShipperRepository shipperRepository, ICardRepository cardRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
            _shipperRepository = shipperRepository;
            _cardRepository = cardRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            var result = await _signInManager.PasswordSignInAsync(Username, Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(Username);
                List<CartItem> cartItems = JsonSerializer.Deserialize<List<CartItem>>(HttpContext.Session.GetString("cart"));
                List<CartItem> newCartItems = new();
                var existingProductIds = _cartItemRepository.GetByCartId(user.Id)
                                                           .Select(ci => ci.ProductId)
                                                           .ToList();
                foreach (CartItem ci in cartItems)
                {
                    if (!existingProductIds.Contains(ci.ProductId))
                    {
                        newCartItems.Add(new CartItem
                        {
                            CartId = user.Id,
                            ProductId = ci.ProductId,
                            Quantity = ci.Quantity
                        });
                    }
                }

                foreach (CartItem ci in newCartItems)
                {
                    _cartItemRepository.Add(ci);
                }
                return RedirectToAction("Index", "Home");
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string Name, string Surname, string Email, string Gender, string Username, string Password)
        {
            AppUser newUser = new()
            {
                Name = Name,
                Surname = Surname,
                Email = Email,
                Gender = Enum.Parse<Gender>(Gender),
                UserName = Username,
                ImageUrl = "/img/defaultpp.png"
            };
            var result1 = await _userManager.CreateAsync(newUser, Password);
            if (result1.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                _cartRepository.Add(new Cart
                {
                    Id = newUser.Id,
                    UserId = newUser.Id
                });
                await _signInManager.PasswordSignInAsync(newUser, Password, false, true);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.SetString("cart", JsonSerializer.Serialize<List<CartItem>>(new List<CartItem>()));
            HttpContext.Session.SetInt32("coupon", 0);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            EditViewModel evm = new()
            {
                username = user.UserName,
                name = user.Name,
                surname = user.Surname,
                Orders = _orderRepository.GetAll().Where(o => o.UserId == user.Id)
            };
            foreach(Order o in evm.Orders)
            {
                o.Address = _addressRepository.GetById(o.AddressId);
                o.Shipper = _shipperRepository.GetById(o.ShipperId);
                o.Card = _cardRepository.GetById(o.CardId);
                o.OrderItems = _orderItemRepository.GetAll().Where(oi => oi.OrderId == o.Id).ToList();
                foreach (var orderItem in o.OrderItems)
                {
                    orderItem.Product = _productRepository.GetById(orderItem.ProductId);
                }
            }
            return View(evm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel evm)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            user.UserName = evm.username;
            user.Name = evm.name;
            user.Surname = evm.surname;
            user.NormalizedUserName = evm.username.ToUpper();
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Edit");
        }
    }
}

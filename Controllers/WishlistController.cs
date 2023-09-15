using ETicaret.Models;
using ETicaret.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.Controllers
{
    public class WishlistController : BaseController
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IProductRepository _productRepository;
        public WishlistController(IWishlistRepository wishlistRepository,IProductRepository productRepository) 
        {
            _wishlistRepository = wishlistRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            List<int> productIds = _wishlistRepository.GetByUserId(Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).Select(w => w.ProductId).ToList();
            List<Product> products = _productRepository.GetAll().Where(p => productIds.Contains(p.Id)).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add(int userId, int productId)
        {
            _wishlistRepository.Add(new()
            {
                UserId = userId,
                ProductId = productId
            });
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int userId,int productId) 
        {
            _wishlistRepository.Remove(_wishlistRepository.GetByUserIdAndProductId(userId,productId));
            return RedirectToAction("Index");
        }
    }
}

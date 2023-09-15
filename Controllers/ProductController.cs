using Microsoft.AspNetCore;
using ETicaret.Data;
using ETicaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using System.Text.Json;
using ETicaret.Repositories.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ETicaret.Controllers
{
    public class ProductController : BaseController
    {
        protected readonly IProductRepository _productRepository;
        protected readonly ICommentRepository _commentRepository;
        protected readonly IWishlistRepository _wishlistRepository;
        protected readonly ICategoryRepository _categoryRepository;
        protected readonly ISubcategoryRepository _subcategoryRepository;
        protected readonly IRecentlyViewedProductRepository _recentlyViewedProductRepository;
        private readonly SignInManager<AppUser> _signInManager;
        public ProductController(IProductRepository productRepository, ICommentRepository commentRepository, IWishlistRepository wishlistRepository, ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository, IRecentlyViewedProductRepository recentlyViewedProductRepository, SignInManager<AppUser> signInManager)
        {
            _productRepository = productRepository;
            _commentRepository = commentRepository;
            _wishlistRepository = wishlistRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _recentlyViewedProductRepository = recentlyViewedProductRepository;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Search(string? searchTerm,int categoryId, int? subcategoryId, int sortOrder, int sortNumber, int targetPage = 1)
        {
            List<Product> recentlyViewedProducts;
            SearchViewModel svm = new()
            {
                SearchTerm = searchTerm,
                CategoryId = categoryId,
                SubcategoryId = subcategoryId
            };
            sortNumber = sortNumber == 0 ? 12 : sortNumber;
            if(searchTerm == "" || searchTerm == null || searchTerm == "null")
            {
                if(categoryId == 0) 
                {
                    svm.Products = _productRepository.GetAll().ToList();
                    svm.MenuName = "Tüm Ürünler";
                }
                else 
                {
                    if (!subcategoryId.HasValue)
                    {
                        svm.Products = _productRepository.GetAll().Where(p => p.CategoryId == categoryId).ToList();
                        svm.MenuName = _categoryRepository.GetById(categoryId).Name;
                    }

                    else
                    {
                        svm.Products = _productRepository.GetAll().Where(p => p.CategoryId == categoryId && p.SubcategoryId == subcategoryId).ToList();
                        svm.MenuName = _subcategoryRepository.GetById((int)subcategoryId).Name;
                    }
                }
            }

            else 
            {
                svm.Products = _productRepository.GetAll().Where(p => p.Name.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                svm.MenuName = "Search";
            }

            if (_signInManager.IsSignedIn(User))
            {
                var userId = int.Parse(User.Identity.GetUserId());
                var recentlyViewedProductIds = _recentlyViewedProductRepository.GetUserRVPsByUserId(userId).Select(rvp => rvp.ProductId);
                recentlyViewedProducts = _productRepository
                    .GetAll()
                    .Where(p => recentlyViewedProductIds.Contains(p.Id))
                    .ToList();
            }

            else
            {
                recentlyViewedProducts = (HttpContext.Session.GetString("guestrvp") != null) ? JsonSerializer.Deserialize<List<Product>>(HttpContext.Session.GetString("guestrvp")) : new List<Product>();
                recentlyViewedProducts.Reverse();
            }
            svm.RecentlyViewedProducts = recentlyViewedProducts;

            foreach (Product product in svm.Products)
                product.Comments = _commentRepository.GetByProductId(product.Id);
            switch (sortOrder)
            {
                case 1:
                    svm.Products = svm.Products.OrderBy(p => p.Name).ToList();
                    break;
                case 2:
                    svm.Products = svm.Products.OrderByDescending(p => p.Comments.Any() ? p.Comments.Average(c => c.Rating) : 0).ToList();
                    break;
                case 3:
                    svm.Products = svm.Products.OrderByDescending(p => p.Comments.Count).ToList();
                    break;
                case 4:
                    svm.Products = svm.Products.OrderBy(p => p.Id).ToList();
                    break;
                case 5:
                    svm.Products = svm.Products.OrderBy(p => p.Price).ToList();
                    break;
                case 6:
                    svm.Products = svm.Products.OrderByDescending(p => p.Price).ToList();
                    break;
            }
            int maxpage = (int)Math.Ceiling((double)svm.Products.Count / sortNumber);
            ViewBag.MaxPage = maxpage;
            ViewBag.CurrentPage = targetPage;
            ViewBag.selectedSortOrder = sortOrder;
            ViewBag.selectedSortNumber = sortNumber;
            svm.Wishlists = _signInManager.IsSignedIn(User) ? _wishlistRepository.GetByUserId(int.Parse(User.Identity.GetUserId())) : null;
            svm.Products = svm.Products.Skip((targetPage - 1) * sortNumber).Take(sortNumber).ToList();
            svm.Categories = _categoryRepository.GetAll().ToList();
            HttpContext.Session.SetString("compared1", HttpContext.Session.GetString("compared1") ?? "");
            HttpContext.Session.SetString("compared2", HttpContext.Session.GetString("compared2") ?? "");
            if (HttpContext.Session.GetString("compared2") != "")
                return RedirectToAction(nameof(Compare), new { productId1 = HttpContext.Session.GetString("compared1"), productId2 = HttpContext.Session.GetString("compared2") });
            return View(svm);
        }

        [HttpPost]
        public IActionResult SearchFilter(string? searchTerm, int? categoryId, int? subcategoryId, int sortOrder, int sortNumber, int targetPage)
        {
            return Json(new { searchTerm, categoryId, subcategoryId, sortOrder, sortNumber, targetPage });
        }

        public IActionResult Detail(int productId)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (_recentlyViewedProductRepository.GetUserRVPsByUserId(int.Parse(User.Identity.GetUserId())).Count == 3)
                {
                    _recentlyViewedProductRepository.Remove(_recentlyViewedProductRepository.GetAll().OrderBy(rvp => rvp.Id).FirstOrDefault());
                }
                _recentlyViewedProductRepository.Add(new()
                {
                    ProductId = productId,
                    UserId = int.Parse(User.Identity.GetUserId())
                });
            }

            else
            {
                HttpContext.Session.SetString("guestrvp", HttpContext.Session.GetString("guestrvp") ?? JsonSerializer.Serialize<List<Product>>(new()));
                List<Product> guestRvps = JsonSerializer.Deserialize<List<Product>>(HttpContext.Session.GetString("guestrvp"));
                Product p = _productRepository.GetById(productId);
                if (guestRvps.Count == 3)
                {
                    if (!guestRvps.Contains(p))
                    {
                        guestRvps[0] = guestRvps[1];
                        guestRvps[1] = guestRvps[2];
                        guestRvps[2] = p;
                    }
                }
                else
                    guestRvps.Add(p);
                HttpContext.Session.SetString("guestrvp", JsonSerializer.Serialize<List<Product>>(guestRvps));
            }

            Product product = _productRepository.GetById(productId);
            product.Comments = _commentRepository.GetByProductId(productId);
            product.Category = _categoryRepository.GetById(product.CategoryId);
            product.Subcategory = product.SubcategoryId != null ? _subcategoryRepository.GetById((int)product.SubcategoryId) : null;
            return View(product);
        }

        [HttpPost]
        public IActionResult AddComment(int rating, string detail, int userId, int productId)
        {
            Comment newComment = new()
            {
                Detail = detail,
                Rating = rating,
                UserId = userId,
                ProductId = productId,
                CommentTime = DateTime.Now
            };
            _commentRepository.Add(newComment);
            return RedirectToAction("Detail", new { productId });
        }

        [HttpPost]
        public JsonResult GetById(int productId)
        {
            Product p = _productRepository.GetById(productId);
            p.Price = (int)(p.Price * float.Parse(HttpContext.Session.GetString("multiplier")));
            return Json(JsonSerializer.Serialize(p));
        }

        [HttpPost]
        public JsonResult SetCompare(int productId)
        {
            if (HttpContext.Session.GetString("compared1") == "")
                HttpContext.Session.SetString("compared1", productId.ToString());
            else
            {
                if (HttpContext.Session.GetString("compared1") == productId.ToString())
                    return Json(new { });
                HttpContext.Session.SetString("compared2", productId.ToString());
            }
            return Json(new { });
        }

        public IActionResult Compare(int productId1, int productId2)
        {
            List<Product> products = new() { _productRepository.GetById(productId1), _productRepository.GetById(productId2) };
            HttpContext.Session.SetString("compared1", "");
            HttpContext.Session.SetString("compared2", "");
            return View(products);
        }
    }
}

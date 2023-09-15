using ETicaret.Models;
using ETicaret.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IShipperRepository _shipperRepository;
        public AdminController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository,ICouponRepository couponRepository, IShipperRepository shipperRepository) 
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _couponRepository = couponRepository;
            _shipperRepository = shipperRepository;
        }
        public IActionResult Index()
        {
            AdminPanelViewModel apvm = new()
            {
                Products = _productRepository.GetAll(),
                Categories = _categoryRepository.GetAll(),
                Subcategories = _subcategoryRepository.GetAll(),
                Coupons = _couponRepository.GetAll(),
                Shippers = _shipperRepository.GetAll()
            };
            return View(apvm);
        }

        [HttpPost]
        public IActionResult CreateProduct(string Name, int Price, int Stock, int categoryId, int? subcategoryId, string Description, string Image)
        {
            var newProduct = new Product
            {
                Name = Name,
                Price = Price,
                Stock = Stock,
                CategoryId = categoryId,
                SubcategoryId = subcategoryId,
                Description = Description,
                ImageUrl = "250x300.png"
            };
           _productRepository.Add(newProduct);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveProduct(int Id)
        {
            _productRepository.Remove(_productRepository.GetById(Id));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateProduct(int Id)
        {
            AdminPanelViewModel apvm = new()
            {
                Product = _productRepository.GetById(Id),
                Products = _productRepository.GetAll(),
                Categories = _categoryRepository.GetAll(),
                Subcategories = _subcategoryRepository.GetAll(),
                Coupons = _couponRepository.GetAll(),
                Shippers = _shipperRepository.GetAll()
            };
            return View(apvm);
        }

        [HttpPost]
        public IActionResult UpdateProduct(int Id, string Name, int Price, int Stock, int categoryId, int? subcategoryId, string Description, string Image)
        {
            var newProduct = new Product
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Stock = Stock,
                CategoryId = categoryId,
                SubcategoryId = subcategoryId,
                Description = Description,
                ImageUrl = "250x300.png"
            };
            _productRepository.Update(newProduct);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateCategory(string Name)
        {
            var newCategory = new Category
            {
                Name = Name
            };
            _categoryRepository.Add(newCategory);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveCategory(int Id)
        {
            _categoryRepository.Remove(_categoryRepository.GetById(Id));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int Id)
        {
            AdminPanelViewModel apvm = new()
            {
                Category = _categoryRepository.GetById(Id),
                Products = _productRepository.GetAll(),
                Categories = _categoryRepository.GetAll(),
                Subcategories = _subcategoryRepository.GetAll(),
                Coupons = _couponRepository.GetAll(),
                Shippers = _shipperRepository.GetAll()
            };
            return View(apvm);
        }

        [HttpPost]
        public IActionResult UpdateCategory(int Id, string Name)
        {
            var newCategory = new Category
            {
                Id = Id,
                Name = Name
            };
            _categoryRepository.Update(newCategory);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateSubcategory(string Name,int CategoryId)
        {
            var newSubcategory = new Subcategory
            {
                Name = Name,
                CategoryId = CategoryId
            };
            _subcategoryRepository.Add(newSubcategory);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveSubcategory(int Id)
        {
            _subcategoryRepository.Remove(_subcategoryRepository.GetById(Id));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateSubcategory(int Id)
        {
            AdminPanelViewModel apvm = new()
            {
                Subcategory = _subcategoryRepository.GetById(Id),
                Products = _productRepository.GetAll(),
                Categories = _categoryRepository.GetAll(),
                Subcategories = _subcategoryRepository.GetAll(),
                Coupons = _couponRepository.GetAll(),
                Shippers = _shipperRepository.GetAll()
            };
            return View(apvm);
        }

        [HttpPost]
        public IActionResult UpdateSubcategory(int Id,string Name)
        {
            var newSubcategory = new Subcategory
            {
                Id = Id,
                Name = Name
            };
            _subcategoryRepository.Update(newSubcategory);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateCoupon(string code,string description,int percentage)
        {
            var newCoupon = new Coupon
            {
                Code = code,
                Description = description,
                Percentage = percentage
            };
            _couponRepository.Add(newCoupon);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveCoupon(int Id)
        {
            _couponRepository.Remove(_couponRepository.GetById(Id));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCoupon(int Id)
        {
            AdminPanelViewModel apvm = new()
            {
                Subcategory = _subcategoryRepository.GetById(Id),
                Products = _productRepository.GetAll(),
                Categories = _categoryRepository.GetAll(),
                Subcategories = _subcategoryRepository.GetAll(),
                Coupons = _couponRepository.GetAll(),
                Shippers = _shipperRepository.GetAll()
            };
            return View(apvm);
        }

        [HttpPost]
        public IActionResult CreateShipper(string name, string description)
        {
            var newShipper = new Shipper
            {
                Name = name,
                Description = description
            };
            _shipperRepository.Add(newShipper);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveShipper(int Id)
        {
            //_shipperRepository.Remove(_shipperRepository.GetById(Id));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateShipper(int Id)
        {
            AdminPanelViewModel apvm = new()
            {
                Subcategory = _subcategoryRepository.GetById(Id),
                Products = _productRepository.GetAll(),
                Categories = _categoryRepository.GetAll(),
                Subcategories = _subcategoryRepository.GetAll(),
                Coupons = _couponRepository.GetAll(),
                Shippers = _shipperRepository.GetAll()
            };
            return View(apvm);
        }

        [HttpPost]
        public JsonResult GetSubcategories(int categoryId)
        {
            var subcategories = _subcategoryRepository.GetByCategoryId(categoryId);
            return Json(subcategories);
        }
    }
}

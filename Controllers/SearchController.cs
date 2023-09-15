using ETicaret.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Controllers
{
    public class SearchController : BaseController
    {
        private readonly IProductRepository _productRepository;
        public SearchController(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Get(string searchTerm)
        {
            return ViewComponent("SearchBar", new { searchTerm });
        }

        public IActionResult GetResults(string term) 
        {
            return RedirectToAction("Search", "Product", new
            {
                searchTerm = term
            });
        }
    }
}

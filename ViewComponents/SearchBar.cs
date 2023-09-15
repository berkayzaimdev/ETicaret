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
    public class SearchBar : ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public SearchBar(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke(string searchTerm)
        {
            List<Product> results = new();
            if(searchTerm!=""&&searchTerm!=null)
                results = _productRepository.GetAll().Where(p => p.Name.ToLower().StartsWith(searchTerm.ToLower())).ToList();
            return View(results);
        }
    }
}

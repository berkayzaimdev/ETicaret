using ETicaret.Data;
using ETicaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.ViewComponents
{
    [ViewComponent]
    public class MenuBar : ViewComponent
    {
        private readonly AppDbContext _context;

        public MenuBar(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Category> categories = _context.Categories.ToList();
            List<Subcategory> subcategories = _context.Subcategories.ToList();

            MenuViewModel mvm = new MenuViewModel
            {
                Categories = _context.Categories.Include(x=>x.Subcategories).ToList()
            };

            return View("Default", mvm);
        }
    }
}

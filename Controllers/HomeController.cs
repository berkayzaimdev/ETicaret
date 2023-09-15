using ETicaret.Data;
using ETicaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using System.Globalization;

namespace ETicaret.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        protected readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger,AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult MenuBarPartial()
        {
            MenuViewModel mvm = new();
            mvm.Categories = _context.Categories.ToList();
            mvm.Subcategories = _context.Subcategories.Include("Category").ToList();
            return PartialView("_MenuBarPartial",mvm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult ChangeLanguage(string lang)
        {
            HttpContext.Session.SetString("lang", lang);
            return Json(new { });
        }

        public JsonResult ChangeCurrency(string symbol,string multiplier)
        {
            HttpContext.Session.SetString("symbol", symbol);
            HttpContext.Session.SetString("multiplier", multiplier);
            return Json(new { });
        }
    }
}
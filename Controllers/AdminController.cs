using Microsoft.AspNetCore.Mvc;
using odshop.Data;
using odshop.Models;
using Microsoft.AspNetCore.Http;

namespace odshop.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 🔐 ADMIN KONTROLÜ
            if (HttpContext.Session.GetString("isAdmin") != "True")
                return RedirectToAction("Login", "Account");

            var products = _context.Products.ToList();
            return View(products);
        }
    }
}


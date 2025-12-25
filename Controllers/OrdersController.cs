using Microsoft.AspNetCore.Mvc;
using odshop.Data;
using odshop.Models;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.EntityFrameworkCore;

namespace odshop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // ÖDEME SAYFASI
        public IActionResult Pay(int id)
        {
            var username = HttpContext.Session.GetString("username");
            if (username == null)
                return RedirectToAction("Login", "Account");

            ViewBag.ProductId = id;
            return View();
        }
        public IActionResult MyOrders()
        {
            var username = HttpContext.Session.GetString("username");

            if (username == null)
                return RedirectToAction("Login", "Account");

            var orders = _context.Orders
                .Include(o => o.Product)
                .Where(o => o.Username == username)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }


        // ÖDEME TAMAMLAMA
        [HttpPost]
        public IActionResult Complete(int productId, string paymentType, string paymentInfo)
        {
            var username = HttpContext.Session.GetString("username");
            if (username == null)
                return RedirectToAction("Login", "Account");

            var order = new Order
            {
                ProductId = productId,
                Username = username,
                PaymentType = paymentType,
                PaymentInfo = paymentInfo,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }


        public IActionResult Success()
        {
            return View();
        }
    }
}


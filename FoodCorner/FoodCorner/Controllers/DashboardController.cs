using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FoodCorner.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddRestaurant()
        {
            return RedirectToAction("Add", "Restaurant");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AllOrders()
        {
            var allOrders =
                (from orders in _context.Orders
                 join users in _context.Users on orders.UserID equals users.Id
                 join foods in _context.Foods on orders.FoodID equals foods.FoodID
                 join rest in _context.Restaurants on foods.RestaurantID equals rest.RestaurantID
                 select new
                 {
                     orderedItem = foods.Name,
                     restaurant = rest.Name,
                     customer = users.FirstName + " " + users.LastName,
                     price = foods.Price + " ₺"
                 }).ToList();

            ViewBag.allOrders = allOrders;

            return View();
        }
    }
}

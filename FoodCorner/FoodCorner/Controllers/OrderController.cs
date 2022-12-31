using FoodCorner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodCorner.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string _userID = (string)User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userOrders = 
                ( from o in _context.Orders
                  join u in _context.Users on o.UserID equals u.Id
                  join f in _context.Foods on o.FoodID equals f.FoodID
                  where u.Id == _userID
                  select new
                  {
                      foodID = f.FoodID,
                      foodImage = f.ImageUrl,
                      foodName = f.Name,
                      price = f.Price,
                  }).ToList();

            ViewBag.cartItems = userOrders;
            return View();
        }
    }
}

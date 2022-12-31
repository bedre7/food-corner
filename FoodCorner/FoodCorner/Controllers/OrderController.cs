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
            var userOrders = _context.Orders
                                    .Include(o => o.FoodID)
                                    .Include(u => u.UserID)
                                    .FirstOrDefault(u => u.UserID == _userID);
            //ViewBag.orderedItem = 
            return View(_context.Orders.ToList());
        }
    }
}

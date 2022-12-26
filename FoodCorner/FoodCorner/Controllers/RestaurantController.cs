using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorner.Controllers
{
    public class RestaurantController : Controller
    {
        private ApplicationDbContext _db;

        public RestaurantController(ApplicationDbContext _db)
        {
            this._db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                _db.Restaurants.Add(restaurant);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }
        public IActionResult List()
        {
            return View(_db.Restaurants);
        }
    }
}

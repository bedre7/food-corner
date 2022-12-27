using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            var restaurant = _db.Restaurants.FirstOrDefault(r => r.RestaurantID == id);
            if (restaurant == null)
            {
                ViewBag.error = "No such restaurant was found";
                return View("Error");
            }
            var restaurantFoods = this._db.Foods
                                    .Include(f => f.Restaurant)
                                    .Where(f => f.RestaurantID == restaurant.RestaurantID)
                                    .ToList();

            TempData["RestaurantFoods"] = restaurantFoods;

            return View(restaurant);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int? id)
        {
            //check validity
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
        [Authorize(Roles = "Admin")]
        public IActionResult AddFood(int? id)
        {
            if (id == null)
            {
                ViewBag.error = "Page is of unknown resource!";
                return RedirectToAction(nameof(Error));
            }
            Restaurant restaurant = this._db.Restaurants.FirstOrDefault(r => r.RestaurantID == id);
            if (restaurant == null)
            {
                ViewBag.error = "No such restaurant was found";
                return View("Error");
            }

            return RedirectToAction("Add", "Food", new { id });
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}

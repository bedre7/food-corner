using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            return View(_db.Restaurants);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            var restaurant = _db.Restaurants.
                                FirstOrDefault(r => r.RestaurantID == id);
            if (restaurant == null)
            {
                ViewBag.error = "No such restaurant was found";
                return View(nameof(Error));
            }
            var restaurantMenu = _db.Foods
                                   .Include(f => f.Restaurant)
                                   .Where(f => f.RestaurantID == restaurant.RestaurantID)
                                   .ToList();
            
            var restaurantReviews =
                (from reviews in _db.Reviews
                 join rest in _db.Restaurants on reviews.RestaurantID equals rest.RestaurantID
                 where rest.RestaurantID == reviews.RestaurantID
                 select reviews).ToList();
                 

            TempData["RestaurantMenu"] = restaurantMenu;
            TempData["Reviews"] = restaurantReviews;
            ViewBag.reviews = _db.Reviews
                                 .Include(r => r.Restaurant)
                                 .Where(m => m.RestaurantID == restaurant.RestaurantID)
                                 .Count();

            return View(restaurant);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _db.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Restaurant restaurant)
        {
            if (id != restaurant.RestaurantID)
            {
                return NotFound();
            }

            _db.Update(restaurant);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
        [Authorize(Roles = "Admin")]
        public IActionResult AddFood(int? id)
        {
            if (id == null)
            {
                ViewBag.error = "Page is of unknown resource!";
                return RedirectToAction(nameof(Error));
            }
            var restaurant = this._db.Restaurants.FirstOrDefault(r => r.RestaurantID == id);
            if (restaurant == null)
            {
                ViewBag.error = "No such restaurant was found";
                return View("Error");
            }

            return RedirectToAction("Add", "Food", new { id });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Restaurants == null)
            {
                return NotFound();
            }

            var restaurant = await _db.Restaurants
                .Include(f => f.Foods)
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _db.Restaurants
                                   .Include(r => r.Foods)
                                   .FirstOrDefaultAsync(m => m.RestaurantID == id);

            if (restaurant == null)
            {
                ViewBag.error = "No such restaurant was found";
                return View(nameof(Error));
            }
            
            bool hasFoods = restaurant?.Foods.Count > 0;
            if (hasFoods)
            {
                ViewBag.error = "Can not delete restaurant since it has dependencies associated with it";
                return View(nameof(Error));
            }

            _db.Restaurants.Remove(restaurant);
            await _db.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Customer")]
        public IActionResult Order(int id)
        {
            if (id == null || _db.Restaurants == null)
            {
                return NotFound();
            }

            return RedirectToAction("Order", "Food", new { id });
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}

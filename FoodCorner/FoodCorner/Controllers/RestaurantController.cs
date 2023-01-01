using AspNetCoreHero.ToastNotification.Abstractions;
using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace FoodCorner.Controllers
{
    public class RestaurantController : Controller
    {
        private ApplicationDbContext _db;
        private readonly IStringLocalizer<RestaurantController> _localizer;
        private readonly INotyfService _toastNotification;
        public RestaurantController(ApplicationDbContext _db, IStringLocalizer<RestaurantController> localizer, INotyfService toastNotification)
        {
            this._db = _db;
            _localizer = localizer;
            _toastNotification = toastNotification;
        }
        public void OnSuccess(string message)
        {
            _toastNotification.Success(message, 10);
        }
        public void OnFailure(string message)
        {
            _toastNotification.Error(message, 10);
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
                 where reviews.RestaurantID == id
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
            ViewBag.Rating = new List<SelectListItem>
            {
                new SelectListItem{ Value = "5", Text = "Excellent"},
                new SelectListItem{ Value = "4", Text = "Very Good"},
                new SelectListItem{ Value = "3", Text = "Good"},
                new SelectListItem{ Value = "1", Text = "Not bad"},
                new SelectListItem{ Value = "2", Text = "Very bad"},
            };

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
            ViewBag.Rating = new List<SelectListItem>
            {
                new SelectListItem{ Value = "5", Text = "Excellent"},
                new SelectListItem{ Value = "4", Text = "Very Good"},
                new SelectListItem{ Value = "3", Text = "Good"},
                new SelectListItem{ Value = "1", Text = "Not bad"},
                new SelectListItem{ Value = "2", Text = "Very bad"},
            };

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
            OnSuccess("Edited Restaurant details successfully");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Add(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                _db.Restaurants.Add(restaurant);
                _db.SaveChanges();
                OnSuccess("Added new restaurant successfully");
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddMenu(int? id)
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
                OnFailure("Can not delete restaurant since it has dependencies associated with it");
                return View(nameof(Error));
            }

            _db.Restaurants.Remove(restaurant);
            await _db.SaveChangesAsync();
            OnSuccess("Deleted resaurant successully");
            
            return RedirectToAction(nameof(Index));
        }
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

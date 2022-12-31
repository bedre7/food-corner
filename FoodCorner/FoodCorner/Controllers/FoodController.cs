using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace FoodCorner.Controllers
{
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;
        private INotyfService _toastNotification;
        public FoodController(ApplicationDbContext context, INotyfService toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        public void OnOrder()
        {
            _toastNotification.Success("Placed your order!", 10);
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Foods.Include(f => f.Restaurant);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.Restaurant)
                .FirstOrDefaultAsync(m => m.FoodID == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add(int? id)
        {
            TempData["RestaurantID"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Food food)
        {
            if (TempData["RestaurantID"] != null)
            {
                int restaurantID = (int)TempData["RestaurantID"];
                food.RestaurantID = restaurantID;
                this._context.Add(food);
                this._context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["RestaurantID"] = new SelectList(_context.Restaurants, "RestaurantID", "Name", food.RestaurantID);

            return View(food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodID,Name,ImageUrl,Price,RestaurantID")] Food food)
        {
            if (id != food.FoodID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(food);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(food.FoodID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.Restaurant)
                .FirstOrDefaultAsync(m => m.FoodID == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Foods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
            }
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.FoodID == id);
        }
        [Authorize(Roles = "Customer")]
        public IActionResult Order(int id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var orderedFood = _context.Foods.Find(id);

            if (orderedFood == null)
            {
                return NotFound();
            }

            string _userID = (string)User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.FirstOrDefault(u => u.Id == _userID);

            Order newOrder = new Order { FoodID = id, UserID = _userID, OrderedBy = user, OrderedItem = orderedFood };
            _context.Add(newOrder);
            _context.SaveChanges();
            OnOrder();

            return RedirectToAction("Index", "Order");
        }
    }
}

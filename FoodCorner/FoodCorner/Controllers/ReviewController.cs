using AspNetCoreHero.ToastNotification.Abstractions;
using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodCorner.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private INotyfService _toastNotification;
        public ReviewController(ApplicationDbContext context, INotyfService toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        public void OnSuccess()
        {
            _toastNotification.Success("Thank you for the review!");
        }
        public IActionResult Index()
        {
            return View(_context.Reviews);
        }
        public IActionResult Add(int id)
        {
            ViewBag.Rating = new List<SelectListItem>
            {
                new SelectListItem{ Value = "5", Text = "Excellent"},
                new SelectListItem{ Value = "4", Text = "Very Good"},
                new SelectListItem{ Value = "3", Text = "Good"},
                new SelectListItem{ Value = "1", Text = "Not bad"},
                new SelectListItem{ Value = "2", Text = "Very bad"},
            };

            TempData["RestaurantID"] = id;
            return Index();
        }
        [HttpPost]
        public IActionResult Add(Review review)
        {
            int restaurantID = (int)TempData["RestaurantID"];
            if (review == null || restaurantID == null)
            {
                return NotFound();
            }

            review.RestaurantID = restaurantID;
            _context.Add(review);
            _context.SaveChanges();
            OnSuccess();

            return RedirectToAction("Details", "Restaurant", new {id = restaurantID});
        }
    }
}

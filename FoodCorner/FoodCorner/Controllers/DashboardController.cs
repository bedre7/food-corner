using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorner.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return RedirectToAction("List", "Restaurant");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddRestaurant()
        {
            return RedirectToAction("Add", "Restaurant");
        }
    }
}

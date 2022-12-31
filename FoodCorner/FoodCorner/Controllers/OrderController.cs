using AspNetCoreHero.ToastNotification.Abstractions;
using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FoodCorner.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _toastNotification;
        private string AppUrl;

        public OrderController(ApplicationDbContext context, INotyfService toastNotification)
        {
            _context = context;
           AppUrl = "https://localhost:7043/api/OrderAPI";
            _toastNotification = toastNotification;
        }
        public void OnGet()
        {
            _toastNotification.Success("Removed order succesfully", 10);
        }
        public async Task<IActionResult> Index()
        {
            string _userID = (string)User.FindFirstValue(ClaimTypes.NameIdentifier);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{this.AppUrl}/{_userID}");
            string JSONResponse = await response.Content.ReadAsStringAsync();
            List<OrderItem> userOrders = JsonConvert.DeserializeObject<List<OrderItem>>(JSONResponse);

            int? totalPrice = userOrders?.Sum(o => o.price);

            ViewBag.totalPrice = totalPrice;
            ViewBag.cartItems = userOrders;

            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = new HttpClient();
            await httpClient.DeleteAsync($"{this.AppUrl}/{id}");
            OnGet();

            return RedirectToAction("Index");
        }
    }
}

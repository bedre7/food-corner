using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cartItem = _context.Orders.FirstOrDefault(o => o.OrderID == id);
            if (cartItem is null)
            {
                return NotFound();
            }

            _context.Remove(cartItem);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<OrderItem>>> Get(string id)
        {
            var userOrders =
               (from orders in _context.Orders
                join users in _context.Users on orders.UserID equals users.Id
                join foods in _context.Foods on orders.FoodID equals foods.FoodID
                where users.Id == id
                select new OrderItem
                {
                    orderID = orders.OrderID,
                    imageUrl = foods.ImageUrl,
                    orderName = foods.Name,
                    price = foods.Price,
                }).ToList();

            return userOrders;
        }
    }
}

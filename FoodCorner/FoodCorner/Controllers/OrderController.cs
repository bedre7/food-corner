﻿using FoodCorner.Data;
using Microsoft.AspNetCore.Mvc;

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
            return View(_context.Orders.ToList());
        }
    }
}

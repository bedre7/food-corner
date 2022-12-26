using FoodCorner.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodCorner.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Food> Foods;
        public DbSet<Order> Orders;
        public DbSet<Restaurant> Restaurants;
        public DbSet<Review> Reviews;
        public DbSet<User> Users;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
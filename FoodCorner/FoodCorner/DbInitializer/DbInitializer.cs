using FoodCorner.Data;
using FoodCorner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodCorner.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;
        private ApplicationDbContext _context;

        public DbInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        private static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (await roleManager.FindByIdAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public void Initialize()
        {
            try
            {
                _context.Database.EnsureCreated();
                var migrations = _context.Database.GetPendingMigrations();
                if (migrations.Any())
                    _context.Database.Migrate();
            }
            catch(Exception){ }

            if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole("Customer")).GetAwaiter().GetResult();

                string username = "b191210557@sakarya.edu.tr";
                string password = "sau";

                //await InitializeRoles(roleManager);

                if (userManager.FindByNameAsync(username) == null)
                {
                    var admin = new User
                    {
                        UserName = "b191210557@sakarya.edu.tr",
                        Email = "b191210557@sakarya.edu.tr",
                        FirstName = "Bedru Umer",
                        LastName = "Mohammed",
                    };
                    
                    userManager.CreateAsync(admin, password).GetAwaiter().GetResult();
                    userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();

                    _context.SaveChanges();
                }
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FoodCorner.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
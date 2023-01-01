using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodCorner.Models
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Logo")]
        public string LogoUrl { get; set; }
        public double Rating { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}

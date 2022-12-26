using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Models
{
    public class Food
    {
        public int FoodID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter numbers only")]
        public int Price { get; set; } 
        public int RestaurantID { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
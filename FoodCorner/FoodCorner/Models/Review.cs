using System.ComponentModel.DataAnnotations;

namespace FoodCorner.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        [Required]
        [Display(Name = "Text")]
        public string ReviewText { get; set; }
        public int RestaurantID { get; set; }
        public int Rating { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
